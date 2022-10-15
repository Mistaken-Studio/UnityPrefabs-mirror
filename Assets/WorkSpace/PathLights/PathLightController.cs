using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

// ReSharper disable ParameterHidesMember
// ReSharper disable LocalVariableHidesMember
// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Mistaken.UnityPrefabs.PathLights
{
    [PublicAPI]
    public class PathLightController : MonoBehaviour
    {
        public List<GameObject> PlusX_Lights = new List<GameObject>();
        public List<GameObject> PlusZ_Lights = new List<GameObject>();
        public List<GameObject> MinusX_Lights = new List<GameObject>();
        public List<GameObject> MinusZ_Lights = new List<GameObject>();

        public List<GameObject> PlusX_MinusZ_Lights = new List<GameObject>();
        public List<GameObject> PlusX_PlusZ_Lights = new List<GameObject>();
        public List<GameObject> PlusX_MinusX_Lights = new List<GameObject>();
        public List<GameObject> MinusX_PlusZ_Lights = new List<GameObject>();
        public List<GameObject> MinusX_MinusZ_Lights = new List<GameObject>();
        public List<GameObject> PlusZ_MinusZ_Lights = new List<GameObject>();

        private void Start()
        {
            DisableAll(true);

/*#if UNITY_EDITOR
            this.StartCoroutine(this.DoAnimation());
#endif*/
        }

        private bool destroyed;
        private void OnDestroy()
        {
            destroyed = true;
        }

        private void DisableAll(bool force)
        {
            State = 0;

            foreach (var light in GetAllLights)
                SetStatus(light, false, force);
        }

        private IEnumerable<GameObject> GetAllLights
        {
            get
            {
                foreach (var light in PlusX_Lights)
                    yield return light;

                foreach (var light in PlusZ_Lights)
                    yield return light;

                foreach (var light in MinusX_Lights)
                    yield return light;

                foreach (var light in MinusZ_Lights)
                    yield return light;


                foreach (var light in PlusX_MinusZ_Lights)
                    yield return light;

                foreach (var light in PlusX_PlusZ_Lights)
                    yield return light;

                foreach (var light in PlusX_MinusX_Lights)
                    yield return light;

                foreach (var light in MinusX_PlusZ_Lights)
                    yield return light;

                foreach (var light in MinusX_MinusZ_Lights)
                    yield return light;

                foreach (var light in PlusZ_MinusZ_Lights)
                    yield return light;
            }
        }

        private void SetStatus(GameObject light, bool value, bool force = false)
        {
            if (destroyed)
                return;

            if (this.gameObject == null)
            {
                Debug.LogWarning("Can's set Status for light for null master game object aka PathLightController's gameObject");
                return;
            }

            this.StartCoroutine(_setStatus(light, value, force));
        }

        private const int FadoutTicks = 15;

        private readonly Dictionary<GameObject, Light[]> Lights = new Dictionary<GameObject, Light[]>();

        private Light[] GetLightCache(GameObject obj)
        {
            if (Lights.TryGetValue(obj, out var light))
                return light;

            light = obj.GetComponentsInChildren<Light>();
            Lights[obj] = light;

            return light;
        }

        private IEnumerator _setStatus(GameObject light, bool value, bool force = false)
        {
            var lightC = GetLightCache(light);

            if ((lightC?.Length ?? 0) == 0)
                yield break;

            foreach (var item in lightC)
            {
                if (value)
                    item.intensity = 1;
                else if (force)
                    item.intensity = 0;
            }

            if (value)
                yield break;

            for (float i = 0; i < 1; i += 1f / FadoutTicks)
            {
                foreach (var item in lightC)
                    item.intensity -= 1f / FadoutTicks;

                yield return new WaitForSeconds(1f / FadoutTicks);
            }

            foreach (var item in lightC)
                item.intensity = 0;
        }

        public void SetColor(Color color)
        {
            foreach (var light in GetAllLights.SelectMany(GetLightCache))
                light.color = color;
        }

        public void SetTargetSide(Side targetSide)
        {
            switch (targetSide)
            {
                case Side.NONE:
                    TargetSide = Side.NONE;
                    DisableAll(false);
                    return;
                case Side.SPECIAL:
                    TargetSide = SpecialState;
                    return;
            }

            (float, Side) max = (float.MaxValue, Side.PLUS_X);

            var test = new GameObject
            {
                transform =
                {
                    parent = this.transform,
                    localPosition = Vector3.zero
                }
            };

            switch (targetSide)
            {
                case Side.PLUS_X:
                    test.transform.position += new Vector3(5, 0, 0);
                    break;
                case Side.PLUS_Z:
                    test.transform.position += new Vector3(0, 0, 5);
                    break;
                case Side.MINUS_X:
                    test.transform.position += new Vector3(-5, 0, 0);
                    break;
                case Side.MINUS_Z:
                    test.transform.position += new Vector3(0, 0, -5);
                    break;
            }

            if (PlusX_Lights.Count > 0)
            {
                var distance = Vector3.Distance(PlusX_Lights[0].transform.position, test.transform.position);
                if (max.Item1 > distance)
                    max = (distance, Side.PLUS_X);
            }

            if (PlusZ_Lights.Count > 0)
            {
                var distance = Vector3.Distance(PlusZ_Lights[0].transform.position, test.transform.position);
                if (max.Item1 > distance)
                    max = (distance, Side.PLUS_Z);
            }

            if (MinusX_Lights.Count > 0)
            {
                var distance = Vector3.Distance(MinusX_Lights[0].transform.position, test.transform.position);
                if (max.Item1 > distance)
                    max = (distance, Side.MINUS_X);
            }

            if (MinusZ_Lights.Count > 0)
            {
                var distance = Vector3.Distance(MinusZ_Lights[0].transform.position, test.transform.position);
                if (max.Item1 > distance)
                    max = (distance, Side.MINUS_Z);
            }

            TargetSide = max.Item2;
        }

        public IEnumerator DoAnimation()
        {
            yield return new WaitForSeconds(1f);

            while (true)
                yield return new WaitForSeconds(DoAnimationSingleCycle());
        }

        public float DoAnimationSingleCycle()
        {
            GameObject[][] list;
            switch (TargetSide)
            {
                case Side.PLUS_X:
                    var plusX = PlusX.ToArray();
                    if (plusX.Length == 0)
                        return 0;
                    list = plusX;
                    break;

                case Side.MINUS_X:
                    var minusX = MinusX.ToArray();
                    if (minusX.Length == 0)
                        return 0;
                    list = minusX;
                    break;

                case Side.PLUS_Z:
                    var plusZ = PlusZ.ToArray();
                    if (plusZ.Length == 0)
                        return 0;
                    list = plusZ;
                    break;

                case Side.MINUS_Z:
                    var minusZ = MinusZ.ToArray();
                    if (minusZ.Length == 0)
                        return 0;
                    list = minusZ;
                    break;

                case Side.NONE:
                case Side.SPECIAL:
                default:
                    return 2;
            }


            foreach (var item in list[State - 2 < 0 ? State - 2 + list.Length : State - 2])
                SetStatus(item, false);

            foreach (var item in list[State])
                SetStatus(item, true);

            State++;
            if (State >= list.Length)
                State = 0;

            return 2f / list.Length;
        }

        public enum Side
        {
            PLUS_X,
            PLUS_Z,
            MINUS_X,
            MINUS_Z,

            NONE = -1,
            SPECIAL = -2,
        }

        public Side TargetSide;
        public Side SpecialState = Side.NONE;
        public int State;

        public IEnumerable<GameObject[]> GenerateList(
            IEnumerable<GameObject> startList1,
            IEnumerable<GameObject> startList2,
            IEnumerable<GameObject> startList3,

            IEnumerable<GameObject> middleList1,
            IEnumerable<GameObject> middleList2,
            IEnumerable<GameObject> middleList3,

            IEnumerable<GameObject> endList
            )
        {
            var endListArray = endList.ToArray();
            var endListLength = endListArray.Length;

            if (endListLength == 0)
                yield break;

            var startList1Array = startList1.ToArray();
            var startList1Length = startList1Array.Length;
            var startList2Array = startList2.ToArray();
            var startList2Length = startList2Array.Length;
            var startList3Array = startList3.ToArray();
            var startList3Length = startList3Array.Length;

            var middleList1Array = middleList1.ToArray();
            var middleList1Length = middleList1Array.Length;
            var middleList2Array = middleList2.ToArray();
            var middleList2Length = middleList2Array.Length;
            var middleList3Array = middleList3.ToArray();
            var middleList3Length = middleList3Array.Length;

            List<GameObject> tmp = new List<GameObject>();

            for (var i = 0; i < Mathf.Max(Mathf.Max(startList1Length, startList2Length), startList3Length); i++)
            {
                if (startList1Length > i)
                    tmp.Add(startList1Array[i]);
                if (startList2Length > i)
                    tmp.Add(startList2Array[i]);
                if (startList3Length > i)
                    tmp.Add(startList3Array[i]);
                yield return tmp.ToArray();
                tmp.Clear();
            }

            for (var i = 0; i < Mathf.Max(Mathf.Max(middleList1Length, middleList2Length), middleList3Length); i++)
            {
                if (middleList1Length > i)
                    tmp.Add(middleList1Array[i]);
                if (middleList2Length > i)
                    tmp.Add(middleList2Array[i]);
                if (middleList3Length > i)
                    tmp.Add(middleList3Array[i]);
                yield return tmp.ToArray();
                tmp.Clear();
            }

            foreach (var item in endListArray.Reverse())
                yield return new[] { item };
        }

        private GameObject[][] plusX;
        public GameObject[][] PlusX => plusX ?? (plusX = GenerateList(
            PlusZ_Lights,
            MinusZ_Lights,
            MinusX_Lights,

            PlusX_PlusZ_Lights,
            PlusX_MinusZ_Lights,
            PlusX_MinusX_Lights,

            PlusX_Lights
            ).ToArray());

        private GameObject[][] minusX;
        public GameObject[][] MinusX => minusX ?? (minusX = GenerateList(
            PlusZ_Lights,
            MinusZ_Lights,
            PlusX_Lights,

            MinusX_PlusZ_Lights,
            MinusX_MinusZ_Lights,
            PlusX_MinusX_Lights.Reverse<GameObject>(),

            MinusX_Lights
            ).ToArray());

        private GameObject[][] plusZ;
        public GameObject[][] PlusZ => plusZ ?? (plusZ = GenerateList(
            MinusX_Lights,
            MinusZ_Lights,
            PlusX_Lights,

            PlusX_PlusZ_Lights.Reverse<GameObject>(),
            MinusX_PlusZ_Lights.Reverse<GameObject>(),
            PlusZ_MinusZ_Lights,

            PlusZ_Lights
            ).ToArray());

        private GameObject[][] minusZ;

        public GameObject[][] MinusZ => minusZ ?? (minusZ = GenerateList(
            MinusX_Lights,
            PlusZ_Lights,
            PlusX_Lights,

            PlusX_MinusZ_Lights.Reverse<GameObject>(),
            MinusX_MinusZ_Lights.Reverse<GameObject>(),
            PlusZ_MinusZ_Lights.Reverse<GameObject>(),

            MinusZ_Lights
        ).ToArray());
    }
}