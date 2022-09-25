using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistaken.UnityPrefabs.Misc
{
    public class ElectricalBoxScript : MonoBehaviour
    {
        public MeshRenderer Light1;
        public MeshRenderer Light2;
        public MeshRenderer Light3;
        public MeshRenderer Light4;

        private readonly Dictionary<MeshRenderer, Light> lights = new Dictionary<MeshRenderer, Light>();

        // Start is called before the first frame update
        void Start()
        {
            lights[Light1] = Light1.GetComponentInChildren<Light>();
            lights[Light2] = Light2.GetComponentInChildren<Light>();
            lights[Light3] = Light3.GetComponentInChildren<Light>();
            lights[Light4] = Light4.GetComponentInChildren<Light>();

            SetStatus(1, new Color(0, 0, 0, 0));
            SetStatus(2, new Color(0, 0, 0, 0));
            SetStatus(3, new Color(0, 0, 0, 0));
            SetStatus(4, new Color(0, 0, 0, 0));
        }

        public void SetStatus(int id, Color color)
        {
            bool enable = color.a != 0;
            switch (id)
            {
                case 1:
                    lights[Light1].intensity = enable ? 1 : 0;
                    lights[Light1].color = color;
                    break;

                case 2:
                    lights[Light2].intensity = enable ? 1 : 0;
                    lights[Light2].color = color;
                    break;

                case 3:
                    lights[Light3].intensity = enable ? 1 : 0;
                    lights[Light3].color = color;
                    break;

                case 4:
                    lights[Light4].intensity = enable ? 1 : 0;
                    lights[Light4].color = color;
                    break;
            }
        }
    }
}
