using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistaken.UnityPrefabs.SegmentDisplay
{
    public class TimerSegmentScript : MultiSegmentDisplayScript
    {
        public int Counter;

        public List<MeshRenderer> CenterLights = new List<MeshRenderer>();
        private readonly Dictionary<MeshRenderer, Light> Lights = new Dictionary<MeshRenderer, Light>();

        public Action OnFinishCounting;

        void Start()
        {
            foreach (var item in CenterLights)
            {
                Lights[item] = item.GetComponentInChildren<Light>();
            }

            this.StartCoroutine(CentralLoop());
        }

        private IEnumerator CentralLoop()
        {
            bool enable = true;
            while (true)
            {
                foreach (var item in CenterLights)
                {
                    Lights[item].intensity = enable ? 1 : 0;
                    item.material.color = enable ? this.segments[0].EnabledColor : this.segments[0].DisabledColor;
                }

                enable = !enable;
                yield return new WaitForSeconds(0.5f);
            }
        }

        private IEnumerator Loop()
        {
            while (Counter > 0)
            {
                yield return new WaitForSeconds(1);
                Counter -= 1;
                SetDisplayTime(Counter);
            }

            this.SetText("0000");

            OnFinishCounting?.Invoke();

            yield return new WaitForSeconds(10);
            this.SetText("----");
        }

        public void SetTime(int seconds)
        {
            Counter = seconds;
            this.StopCoroutine(nameof(Loop));
            this.StartCoroutine(Loop());
        }

        public void SetDisplayTime(int seconds)
        {
            int second = seconds % 60;
            int minute = (seconds - second) / 60;
            this.SetText(minute.ToString("00") + second.ToString("00"));
        }
    }
}
