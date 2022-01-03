using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistaken.UnityPrefabs.SegmentDisplay
{
    public class SegmentDisplayScript : MonoBehaviour
    {
        public Color EnabledColor;
        public Color DisabledColor;

        public MeshRenderer TopSegment;
        public MeshRenderer LeftTopSegment;
        public MeshRenderer RightTopSegment;
        public MeshRenderer MiddleSegment;
        public MeshRenderer LeftBottomSegment;
        public MeshRenderer RightBottomSegment;
        public MeshRenderer BottomSegment;

        public void SetNumber(byte? num)
        {
            if(num == null)
            {
                SetColor(TopSegment, DisabledColor);
                SetColor(LeftTopSegment, DisabledColor);
                SetColor(RightTopSegment, DisabledColor);
                SetColor(MiddleSegment, DisabledColor);
                SetColor(LeftBottomSegment, DisabledColor);
                SetColor(RightBottomSegment, DisabledColor);
                SetColor(BottomSegment, DisabledColor);
                return;
            }

            if (num > 9)
                throw new ArgumentOutOfRangeException(nameof(num), "Segment display can only display numbers from 0 to 9");

            switch (num)
            {
                case 0:
                    SetColor(TopSegment, EnabledColor);
                    SetColor(LeftTopSegment, EnabledColor);
                    SetColor(RightTopSegment, EnabledColor);
                    SetColor(MiddleSegment, DisabledColor);
                    SetColor(LeftBottomSegment, EnabledColor);
                    SetColor(RightBottomSegment, EnabledColor);
                    SetColor(BottomSegment, EnabledColor);
                    break;
                case 1:
                    SetColor(TopSegment, DisabledColor);
                    SetColor(LeftTopSegment, DisabledColor);
                    SetColor(RightTopSegment, EnabledColor);
                    SetColor(MiddleSegment, DisabledColor);
                    SetColor(LeftBottomSegment, DisabledColor);
                    SetColor(RightBottomSegment, EnabledColor);
                    SetColor(BottomSegment, DisabledColor);
                    break;
                case 2:
                    SetColor(TopSegment, EnabledColor);
                    SetColor(LeftTopSegment, DisabledColor);
                    SetColor(RightTopSegment, EnabledColor);
                    SetColor(MiddleSegment, EnabledColor);
                    SetColor(LeftBottomSegment, EnabledColor);
                    SetColor(RightBottomSegment, DisabledColor);
                    SetColor(BottomSegment, EnabledColor);
                    break;
                case 3:
                    SetColor(TopSegment, EnabledColor);
                    SetColor(LeftTopSegment, DisabledColor);
                    SetColor(RightTopSegment, EnabledColor);
                    SetColor(MiddleSegment, EnabledColor);
                    SetColor(LeftBottomSegment, DisabledColor);
                    SetColor(RightBottomSegment, EnabledColor);
                    SetColor(BottomSegment, EnabledColor);
                    break;
                case 4:
                    SetColor(TopSegment, DisabledColor);
                    SetColor(LeftTopSegment, EnabledColor);
                    SetColor(RightTopSegment, EnabledColor);
                    SetColor(MiddleSegment, EnabledColor);
                    SetColor(LeftBottomSegment, DisabledColor);
                    SetColor(RightBottomSegment, EnabledColor);
                    SetColor(BottomSegment, DisabledColor);
                    break;
                case 5:
                    SetColor(TopSegment, EnabledColor);
                    SetColor(LeftTopSegment, EnabledColor);
                    SetColor(RightTopSegment, DisabledColor);
                    SetColor(MiddleSegment, EnabledColor);
                    SetColor(LeftBottomSegment, DisabledColor);
                    SetColor(RightBottomSegment, EnabledColor);
                    SetColor(BottomSegment, EnabledColor);
                    break;
                case 6:
                    SetColor(TopSegment, EnabledColor);
                    SetColor(LeftTopSegment, EnabledColor);
                    SetColor(RightTopSegment, DisabledColor);
                    SetColor(MiddleSegment, EnabledColor);
                    SetColor(LeftBottomSegment, EnabledColor);
                    SetColor(RightBottomSegment, EnabledColor);
                    SetColor(BottomSegment, EnabledColor);
                    break;
                case 7:
                    SetColor(TopSegment, EnabledColor);
                    SetColor(LeftTopSegment, DisabledColor);
                    SetColor(RightTopSegment, EnabledColor);
                    SetColor(MiddleSegment, DisabledColor);
                    SetColor(LeftBottomSegment, DisabledColor);
                    SetColor(RightBottomSegment, EnabledColor);
                    SetColor(BottomSegment, DisabledColor);
                    break;
                case 8:
                    SetColor(TopSegment, EnabledColor);
                    SetColor(LeftTopSegment, EnabledColor);
                    SetColor(RightTopSegment, EnabledColor);
                    SetColor(MiddleSegment, EnabledColor);
                    SetColor(LeftBottomSegment, EnabledColor);
                    SetColor(RightBottomSegment, EnabledColor);
                    SetColor(BottomSegment, EnabledColor);
                    break;
                case 9:
                    SetColor(TopSegment, EnabledColor);
                    SetColor(LeftTopSegment, EnabledColor);
                    SetColor(RightTopSegment, EnabledColor);
                    SetColor(MiddleSegment, EnabledColor);
                    SetColor(LeftBottomSegment, DisabledColor);
                    SetColor(RightBottomSegment, EnabledColor);
                    SetColor(BottomSegment, EnabledColor);
                    break;
            }
        }

        public void SetColor(MeshRenderer obj, Color color)
        {
            obj.material.color = color;
        }
    }
}