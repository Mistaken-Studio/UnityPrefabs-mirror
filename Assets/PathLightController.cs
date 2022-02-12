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
            SetStatus(light, false);

        foreach (var light in PlusZ_Lights)
            SetStatus(light, false);

        foreach (var light in MinusX_Lights)
            SetStatus(light, false);

        foreach (var light in MinusZ_Lights)
            SetStatus(light, false);


        foreach (var light in PlusX_MinusZ_Lights)
            SetStatus(light, false);

        foreach (var light in PlusX_PlusZ_Lights)
            SetStatus(light, false);

        foreach (var light in PlusX_MinusX_Lights)
            SetStatus(light, false);

        foreach (var light in MinusX_PlusZ_Lights)
            SetStatus(light, false);

        foreach (var light in MinusX_MinusZ_Lights)
            SetStatus(light, false);

        foreach (var light in PlusZ_MinusZ_Lights)
            SetStatus(light, false);
    }

    private void SetStatus(GameObject light, bool value)
    {
        // light.SetActive(value);
        light.GetComponentInChildren<Light>().enabled = value;
    }

    public void SetTargetSide(Side targetSide)
    {
        Side rawSide = Side.PLUS_X;
        if (this.transform.rotation.y == 90)
            rawSide += 1;
        else if (this.transform.rotation.y == 180)
            rawSide += 2;
        else if (this.transform.rotation.y == 270)
            rawSide += 3;

        targetSide += (int)rawSide;
        if ((int)targetSide > 3)
            targetSide -= 3;

        TargetSide = targetSide;
    }

    public IEnumerator DoAnimation()
    {
        GameObject[][] plusX = PlusX.ToArray();
        GameObject[][] minusX = MinusX.ToArray();
        GameObject[][] plusZ = PlusZ.ToArray();
        GameObject[][] minusZ = MinusZ.ToArray();

        while (true)
        {
            yield return new WaitForSeconds(.2f);
            Debug.Log($"{this.name}: {TargetSide}");
            GameObject[][] list;
            switch (TargetSide)
            {
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

            
            foreach (var item in list[State == 0 ? 9 : State - 1])
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
        MINUS_Z,
        MINUS_X,
        PLUS_Z,
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
}
