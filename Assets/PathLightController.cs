using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        foreach (var light in PlusX_Lights)
            SetStatus(light, false, true);

        foreach (var light in PlusZ_Lights)
            SetStatus(light, false, true);

        foreach (var light in MinusX_Lights)
            SetStatus(light, false, true);

        foreach (var light in MinusZ_Lights)
            SetStatus(light, false, true);


        foreach (var light in PlusX_MinusZ_Lights)
            SetStatus(light, false, true);

        foreach (var light in PlusX_PlusZ_Lights)
            SetStatus(light, false, true);

        foreach (var light in PlusX_MinusX_Lights)
            SetStatus(light, false, true);

        foreach (var light in MinusX_PlusZ_Lights)
            SetStatus(light, false, true);

        foreach (var light in MinusX_MinusZ_Lights)
            SetStatus(light, false, true);

        foreach (var light in PlusZ_MinusZ_Lights)
            SetStatus(light, false, true);
    }

    private void SetStatus(GameObject light, bool value, bool force = false)
    {
        this.StartCoroutine(_setStatus(light, value, force));
    }

    private readonly Dictionary<GameObject, Light> Lights = new Dictionary<GameObject, Light>();
    private IEnumerator _setStatus(GameObject light, bool value, bool force = false)
    {
        if(!Lights.TryGetValue(light, out var lightC))
        {
            lightC = light.GetComponentInChildren<Light>();
            Lights[light] = lightC;
        }

        if (value)
            lightC.enabled = true;
        else if (force)
            lightC.enabled = false;
        else
        {
            var original = 1; // lightC.intensity;
            while (lightC.intensity > 0)
            {
                lightC.intensity -= original * 0.033f;
                yield return new WaitForSeconds(0.033f);
            }
            lightC.enabled = false;
            lightC.intensity = original;
        }
    }

    public void SetTargetSide(Side targetSide)
    {
        if (targetSide == Side.CENTER)
        {
            TargetSide = Side.CENTER;
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

        if(PlusX_Lights.Count > 0)
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
        GameObject[][] center = Center.ToArray();

        while (true)
        {
            yield return new WaitForSeconds(.2f);
            GameObject[][] list;
            switch (TargetSide)
            {
                case Side.CENTER:
                    Debug.Log("Center :/");
                    continue;
                    if (center.Length == 0)
                        continue;
                    list = center;
                    break;

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
                    continue;
            }

            
            foreach (var item in list[State - 2 < 0 ? State - 2 + 10 : State - 2])
                SetStatus(item, false);

            foreach (var item in list[State])
                SetStatus(item, true);

            State++;
            if (State > 9)
                State = 0;
        }
    }

    public enum Side
    {
        PLUS_X,
        PLUS_Z,
        MINUS_X,
        MINUS_Z,

        CENTER = -1,
    }

    public Side TargetSide;
    public int State = 0;

    public IEnumerable<GameObject[]> PlusX
    {
        get
        {
            if (PlusX_Lights.Count == 0)
                yield break;

            List<GameObject> tmp = new List<GameObject>();
            for (int i = 0; i < 3; i++)
            {
                if (PlusZ_Lights.Count > 0)
                    tmp.Add(PlusZ_Lights[i]);
                if (MinusZ_Lights.Count > 0)
                    tmp.Add(MinusZ_Lights[i]);
                if (MinusX_Lights.Count > 0)
                    tmp.Add(MinusX_Lights[i]);
                yield return tmp.ToArray();
                tmp.Clear();
            }

            for (int i = 0; i < 4; i++)
            {
                if (PlusX_PlusZ_Lights.Count > 0)
                    tmp.Add(PlusX_PlusZ_Lights[i]);
                if (PlusX_MinusZ_Lights.Count > 0)
                    tmp.Add(PlusX_MinusZ_Lights[i]);
                if (PlusX_MinusX_Lights.Count > 0)
                    tmp.Add(PlusX_MinusX_Lights[i]);
                yield return tmp.ToArray();
                tmp.Clear();
            }

            yield return new GameObject[] { PlusX_Lights[2] };
            yield return new GameObject[] { PlusX_Lights[1] };
            yield return new GameObject[] { PlusX_Lights[0] };
        }
    }

    public IEnumerable<GameObject[]> MinusX
    {
        get
        {
            if (MinusX_Lights.Count == 0)
                yield break;

            List<GameObject> tmp = new List<GameObject>();
            for (int i = 0; i < 3; i++)
            {
                if (PlusZ_Lights.Count > 0)
                    tmp.Add(PlusZ_Lights[i]);
                if (MinusZ_Lights.Count > 0)
                    tmp.Add(MinusZ_Lights[i]);
                if (PlusX_Lights.Count > 0)
                    tmp.Add(PlusX_Lights[i]);
                yield return tmp.ToArray();
                tmp.Clear();
            }

            for (int i = 0; i < 4; i++)
            {
                if (MinusX_PlusZ_Lights.Count > 0)
                    tmp.Add(MinusX_PlusZ_Lights[i]);
                if (MinusX_MinusZ_Lights.Count > 0)
                    tmp.Add(MinusX_MinusZ_Lights[i]);
                if (PlusX_MinusX_Lights.Count > 0)
                    tmp.Add(PlusX_MinusX_Lights[3 - i]);
                yield return tmp.ToArray();
                tmp.Clear();
            }

            yield return new GameObject[] { MinusX_Lights[2] };
            yield return new GameObject[] { MinusX_Lights[1] };
            yield return new GameObject[] { MinusX_Lights[0] };
        }
    }

    public IEnumerable<GameObject[]> PlusZ
    {
        get
        {
            if (PlusZ_Lights.Count == 0)
                yield break;

            List<GameObject> tmp = new List<GameObject>();
            for (int i = 0; i < 3; i++)
            {
                if (MinusX_Lights.Count > 0)
                    tmp.Add(MinusX_Lights[i]);
                if (MinusZ_Lights.Count > 0)
                    tmp.Add(MinusZ_Lights[i]);
                if (PlusX_Lights.Count > 0)
                    tmp.Add(PlusX_Lights[i]);
                yield return tmp.ToArray();
                tmp.Clear();
            }

            for (int i = 0; i < 4; i++)
            {
                if (PlusX_PlusZ_Lights.Count > 0)
                    tmp.Add(PlusX_PlusZ_Lights[3 - i]);
                if (MinusX_PlusZ_Lights.Count > 0)
                    tmp.Add(MinusX_PlusZ_Lights[3 - i]);
                if (PlusZ_MinusZ_Lights.Count > 0)
                    tmp.Add(PlusZ_MinusZ_Lights[i]);
                yield return tmp.ToArray();
                tmp.Clear();
            }

            yield return new GameObject[] { PlusZ_Lights[2] };
            yield return new GameObject[] { PlusZ_Lights[1] };
            yield return new GameObject[] { PlusZ_Lights[0] };
        }
    }

    public IEnumerable<GameObject[]> MinusZ
    {
        get
        {
            if (MinusZ_Lights.Count == 0)
                yield break;

            List<GameObject> tmp = new List<GameObject>();
            for (int i = 0; i < 3; i++)
            {
                if (MinusX_Lights.Count > 0)
                    tmp.Add(MinusX_Lights[i]);
                if (PlusZ_Lights.Count > 0)
                    tmp.Add(PlusZ_Lights[i]);
                if (PlusX_Lights.Count > 0)
                    tmp.Add(PlusX_Lights[i]);
                yield return tmp.ToArray();
                tmp.Clear();
            }

            for (int i = 0; i < 4; i++)
            {
                if (PlusX_MinusZ_Lights.Count > 0)
                    tmp.Add(PlusX_MinusZ_Lights[3 - i]);
                if (MinusX_MinusZ_Lights.Count > 0)
                    tmp.Add(MinusX_MinusZ_Lights[3 - i]);
                if (PlusZ_MinusZ_Lights.Count > 0)
                    tmp.Add(PlusZ_MinusZ_Lights[3 - i]);
                yield return tmp.ToArray();
                tmp.Clear();
            }

            yield return new GameObject[] { MinusZ_Lights[2] };
            yield return new GameObject[] { MinusZ_Lights[1] };
            yield return new GameObject[] { MinusZ_Lights[0] };
        }
    }

    public IEnumerable<GameObject[]> Center
    {
        get
        {
            List<GameObject> tmp = new List<GameObject>();
            for (int i = 0; i < 3; i++)
            {
                if (MinusX_Lights.Count > 0)
                    tmp.Add(MinusX_Lights[i]);
                if (PlusZ_Lights.Count > 0)
                    tmp.Add(PlusZ_Lights[i]);
                if (PlusX_Lights.Count > 0)
                    tmp.Add(PlusX_Lights[i]);
                if (MinusZ_Lights.Count > 0)
                    tmp.Add(MinusZ_Lights[i]);
                yield return tmp.ToArray();
                tmp.Clear();
            }

            yield return new GameObject[0];
            yield return new GameObject[0];
            yield return new GameObject[0];
            yield return new GameObject[0];
            yield return new GameObject[0];
            yield return new GameObject[0];
            yield return new GameObject[0];
        }
    }
}
