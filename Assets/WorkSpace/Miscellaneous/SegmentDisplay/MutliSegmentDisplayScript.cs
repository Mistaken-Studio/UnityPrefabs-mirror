using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistaken.UnityPrefabs.SegmentDisplay
{
    public class MutliSegmentDisplayScript : MonoBehaviour
    {
        public List<SegmentDisplayScript> segments = new List<SegmentDisplayScript>();

        public MeshRenderer Background;

        public string DebugText;

        public string CurrentText;
        void Start()
        {
            SetText("");
        }
        // Update is called once per frame
        void Update()
        {
            if (CurrentText != DebugText)
                SetText(DebugText);
        }
        [Obsolete]
        public void SetNumber(ushort num)
        {
            SetText(num.ToString());
            return;
            /*
            if (num >= Mathf.Pow(10, segments.Count))
                throw new ArgumentOutOfRangeException(nameof(num), $"Segment display can only display up to {Mathf.Pow(10, segments.Count) - 1}");
            if (CurrentNumber == num)
                return;
            CurrentNumber = num;
            bool skip = false;
            for (int i = segments.Count - 1; i >= 0; i--)
            {
                if(skip)
                    segments[i].SetNumber(0);
                segments[i].SetNumber((byte)(num % 10));
                Debug.Log($"[{i}] {num % 10}");
                num -= (ushort)(num % 10);
                num /= 10;
                if (num <= 0)
                {
                    skip = true;
                }
            }
            */
        }
        public void SetText(string text)
        {
            if(text.Length > segments.Count)
                throw new ArgumentOutOfRangeException(nameof(text), $"Segment display can only display up to {segments.Count} letters");
            text = new String(' ', segments.Count - text.Length) + text;
            for(int i = 0; i < text.Length; i++)
            {
                segments[i].SetChar(text[i]);
            }
        }
    }
}
