using JetBrains.Annotations;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Mistaken.UnityPrefabs.Misc.Light
{

    [PublicAPI]
    [RequireComponent(typeof(MeshRenderer))]
    public class LightObjectScript : MonoBehaviour
    {
        [SerializeField] private new UnityEngine.Light light;
        private new MeshRenderer renderer;

        private float intensity;
        private new bool enabled = true;

        public bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled == value)
                    return;

                enabled = value;
                this.light.intensity = value ? intensity : 0;
                if (value)
                    this.renderer.material.color *= 2;
                else
                    this.renderer.material.color /= 2;
            }
        }

        [UsedImplicitly]
        private void Awake()
        {
            renderer = this.GetComponent<MeshRenderer>();
            intensity = light.intensity;
            if (intensity == 0)
                intensity = 1;
        }
    }

}