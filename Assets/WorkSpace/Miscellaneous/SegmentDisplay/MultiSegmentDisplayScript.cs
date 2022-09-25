using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistaken.UnityPrefabs.SegmentDisplay
{
    public class MultiSegmentDisplayScript : MonoBehaviour
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
