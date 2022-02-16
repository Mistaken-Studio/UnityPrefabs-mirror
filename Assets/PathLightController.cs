using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mistaken.UnityPrefabs.PathLights
{
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

        // Start is called before the first frame update
        void Start()
        {
            DisableAll(true);

/*#if UNITY_EDITOR
            this.StartCoroutine(this.DoAnimation());
#endif*/
        }

        private void DisableAll(bool force)
        {
            State = 0;

            foreach (var light in PlusX_Lights)
                SetStatus(light, false, force);

            foreach (var light in PlusZ_Lights)
                SetStatus(light, false, force);

            foreach (var light in MinusX_Lights)
                SetStatus(light, false, force);

            foreach (var light in MinusZ_Lights)
                SetStatus(light, false, force);


            foreach (var light in PlusX_MinusZ_Lights)
                SetStatus(light, false, force);

            foreach (var light in PlusX_PlusZ_Lights)
                SetStatus(light, false, force);

            foreach (var light in PlusX_MinusX_Lights)
                SetStatus(light, false, force);

            foreach (var light in MinusX_PlusZ_Lights)
                SetStatus(light, false, force);

            foreach (var light in MinusX_MinusZ_Lights)
                SetStatus(light, false, force);

            foreach (var light in PlusZ_MinusZ_Lights)
                SetStatus(light, false, force);
        }

        private void SetStatus(GameObject light, bool value, bool force = false)
        {
            this.StartCoroutine(_setStatus(light, value, force));
        }

        private const int FadoutTicks = 15;

        private readonly Dictionary<GameObject, Light[]> Lights = new Dictionary<GameObject, Light[]>();
        private IEnumerator _setStatus(GameObject light, bool value, bool force = false)
        {
            if (!Lights.TryGetValue(light, out var lightC))
            {
                lightC = light.GetComponentsInChildren<Light>();
                Lights[light] = lightC;
            }

            foreach (var item in lightC)
            {
                if (value)
                    item.enabled = true;
                else if (force)
                    item.enabled = false;
            }

            if (!value && !value)
            {
                for (float i = 0; i < 1; i += 1f / FadoutTicks)
                {
                    foreach (var item in lightC)
                        item.intensity -= 1f / FadoutTicks;

                    yield return new WaitForSeconds(1f / FadoutTicks);
                }

                foreach (var item in lightC)
                {
                    item.enabled = false;
                    item.intensity = 1;
                }
            }
        }

        public void SetTargetSide(Side targetSide)
        {
            if (targetSide == Side.NONE)
            {
                TargetSide = Side.NONE;
                DisableAll(false);
                return;
            }
            else if (targetSide == Side.SPECIAL)
            {
                TargetSide = SpecialState;
                return;
            }

            (float, Side) max = (float.MaxValue, Side.PLUS_X);

            var test = new GameObject();
            test.transform.parent = this.transform;
            test.transform.localPosition = Vector3.zero;
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

            GameObject[][] plusX = PlusX.ToArray();
            GameObject[][] minusX = MinusX.ToArray();
            GameObject[][] plusZ = PlusZ.ToArray();
            GameObject[][] minusZ = MinusZ.ToArray();

            while (true)
            {
                GameObject[][] list;
                switch (TargetSide)
                {
                    case Side.NONE:
                        yield return new WaitForSeconds(2f);
                        continue;

                    case Side.PLUS_X:
                        if (plusX.Length == 0)
                            continue;
                        list = plusX;
                        break;

                    case Side.MINUS_X:
                        if (minusX.Length == 0)
                            continue;
                        list = minusX;
                        break;

                    case Side.PLUS_Z:
                        if (plusZ.Length == 0)
                            continue;
                        list = plusZ;
                        break;

                    case Side.MINUS_Z:
                        if (minusZ.Length == 0)
                            continue;
                        list = minusZ;
                        break;

                    default:
                        yield return new WaitForSeconds(2f);
                        continue;
                }


                foreach (var item in list[State - 2 < 0 ? State - 2 + list.Length : State - 2])
                    SetStatus(item, false);

                foreach (var item in list[State])
                    SetStatus(item, true);

                State++;
                if (State >= list.Length)
                    State = 0;

                yield return new WaitForSeconds(2f / list.Length);
            }
        }

        public enum Side
        {
            PLUS_X,
            PLUS_Z,
            MINUS_X,
            MINUS_Z,

            [System.Obsolete("Use NONE")]
            CENTER = -1,
            NONE = -1,
            SPECIAL = -2,
        }

        public Side TargetSide;
        public Side SpecialState = Side.NONE;
        public int State = 0;

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
            var endListLength = endList.Count();

            if (endListLength == 0)
                yield break;

            var startList1Length = startList1.Count();
            var startList2Length = startList2.Count();
            var startList3Length = startList3.Count();

            var middleList1Length = middleList1.Count();
            var middleList2Length = middleList2.Count();
            var middleList3Length = middleList3.Count();

            List<GameObject> tmp = new List<GameObject>();

            for (int i = 0; i < Mathf.Max(Mathf.Max(startList1Length, startList2Length), startList3Length); i++)
            {
                if (startList1Length > i)
                    tmp.Add(startList1.ElementAt(i));
                if (startList2Length > i)
                    tmp.Add(startList2.ElementAt(i));
                if (startList3Length > i)
                    tmp.Add(startList3.ElementAt(i));
                yield return tmp.ToArray();
                tmp.Clear();
            }

            for (int i = 0; i < Mathf.Max(Mathf.Max(middleList1Length, middleList2Length), middleList3Length); i++)
            {
                if (middleList1Length > i)
                    tmp.Add(middleList1.ElementAt(i));
                if (middleList2Length > i)
                    tmp.Add(middleList2.ElementAt(i));
                if (middleList3Length > i)
                    tmp.Add(middleList3.ElementAt(i));
                yield return tmp.ToArray();
                tmp.Clear();
            }

            for (int i = endListLength - 1; i >= 0; i--)
                yield return new GameObject[] { endList.ElementAt(i) };
        }

        public IEnumerable<GameObject[]> PlusX => GenerateList(
            PlusZ_Lights,
            MinusZ_Lights,
            MinusX_Lights,

            PlusX_PlusZ_Lights,
            PlusX_MinusZ_Lights,
            PlusX_MinusX_Lights,

            PlusX_Lights
            );

        public IEnumerable<GameObject[]> MinusX => GenerateList(
            PlusZ_Lights,
            MinusZ_Lights,
            PlusX_Lights,

            MinusX_PlusZ_Lights,
            MinusX_MinusZ_Lights,
            PlusX_MinusX_Lights.Reverse<GameObject>(),

            MinusX_Lights
            );

        public IEnumerable<GameObject[]> PlusZ => GenerateList(
            MinusX_Lights,
            MinusZ_Lights,
            PlusX_Lights,

            PlusX_PlusZ_Lights.Reverse<GameObject>(),
            MinusX_PlusZ_Lights.Reverse<GameObject>(),
            PlusZ_MinusZ_Lights,

            PlusZ_Lights
            );

        public IEnumerable<GameObject[]> MinusZ => GenerateList(
            MinusX_Lights,
            PlusZ_Lights,
            PlusX_Lights,

            PlusX_MinusZ_Lights.Reverse<GameObject>(),
            MinusX_MinusZ_Lights.Reverse<GameObject>(),
            PlusZ_MinusZ_Lights.Reverse<GameObject>(),

            MinusZ_Lights
            );
    }
}