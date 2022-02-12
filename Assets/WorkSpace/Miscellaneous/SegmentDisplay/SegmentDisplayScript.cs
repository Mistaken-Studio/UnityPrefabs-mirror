﻿using System;
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
        static Dictionary<DisplaySegmentChars, bool[]> Chars = new Dictionary<DisplaySegmentChars, bool[]>()
        {
            {
                DisplaySegmentChars.A, new bool[]
                {
                    true,
                    true, true,
                    true,
                    true,true,
                    false
                }
            },
            {
                DisplaySegmentChars.B, new bool[]
                {
                    false,
                    true, false,
                    true,
                    true,true,
                    true
                }
            },
            {
                DisplaySegmentChars.C, new bool[]
                {
                    true,
                    true, false,
                    false,
                    true,false,
                    true
                }
            },
            {
                DisplaySegmentChars.D, new bool[]
                {
                    false,
                    false, true,
                    true,
                    true,true,
                    true
                }
            },
            {
                DisplaySegmentChars.E, new bool[]
                {
                    true,
                    true, false,
                    true,
                    true,false,
                    true
                }
            },
            {
                DisplaySegmentChars.F, new bool[]
                {
                    true,
                    true, false,
                    true,
                    true, false,
                    false
                }
            },
            {
                DisplaySegmentChars.H, new bool[]
                {
                    false,
                    true, true,
                    true,
                    true, true,
                    false
                }
            },
            {
                DisplaySegmentChars.I, new bool[]
                {
                    false,
                    true, false,
                    false,
                    true, false,
                    false
                }
            },
            {
                DisplaySegmentChars.J, new bool[]
                {
                    false,
                    false, true,
                    false,
                    true, true,
                    true
                }
            },
            {
                DisplaySegmentChars.L, new bool[]
                {
                    false,
                    true, false,
                    false,
                    true, false,
                    true
                }
            },
            {
                DisplaySegmentChars.N, new bool[]
                {
                    false,
                    false, false,
                    true,
                    true, true,
                    false
                }
            },
            {
                DisplaySegmentChars.O, new bool[]
                {
                    false,
                    false, false,
                    true,
                    true, true,
                    true
                }
            },
            {
                DisplaySegmentChars.P, new bool[]
                {
                    true,
                    true, true,
                    true,
                    true, false,
                    false
                }
            },
            {
                DisplaySegmentChars.Q, new bool[]
                {
                    true,
                    true, true,
                    true,
                    false, true,
                    false
                }
            },
            {
                DisplaySegmentChars.R, new bool[]
                {
                    false,
                    false, false,
                    true,
                    true, false,
                    false
                }
            },
            {
                DisplaySegmentChars.S, new bool[]
                {
                    true,
                    true, false,
                    true,
                    false, true,
                    true
                }
            },
            {
                DisplaySegmentChars.T, new bool[]
                {
                    false,
                    true, false,
                    true,
                    true, false,
                    true
                }
            },
            {
                DisplaySegmentChars.U, new bool[]
                {
                    false,
                    true, true,
                    false,
                    true, true,
                    true
                }
            },
            {
                DisplaySegmentChars.Y, new bool[]
                {
                    false,
                    true, true,
                    true,
                    false, true,
                    true
                }
            },
            {
                DisplaySegmentChars.Z, new bool[]
                {
                    true,
                    false, true,
                    true,
                    true, false,
                    true
                }
            },
            {
                DisplaySegmentChars.ZERO, new bool[]
                {
                    true,
                    true, true,
                    false,
                    true, true,
                    true
                }
            },
            {
                DisplaySegmentChars.ONE, new bool[]
                {
                    false,
                    false, true,
                    false,
                    false, true,
                    false
                }
            },
            {
                DisplaySegmentChars.TWO, new bool[]
                {
                    true,
                    false, true,
                    true,
                    true, false,
                    true
                }
            },
            {
                DisplaySegmentChars.THREE, new bool[]
                {
                    true,
                    false, true,
                    true,
                    false, true,
                    true
                }
            },
            {
                DisplaySegmentChars.FOUR, new bool[]
                {
                    false,
                    true, true,
                    true,
                    false, true,
                    false
                }
            },
            {
                DisplaySegmentChars.FIVE, new bool[]
                {
                    true,
                    true, false,
                    true,
                    false, true,
                    true
                }
            },
            {
                DisplaySegmentChars.SIX, new bool[]
                {
                    true,
                    true, false,
                    true,
                    true, true,
                    true
                }
            },
            {
                DisplaySegmentChars.SEVEN, new bool[]
                {
                    true,
                    false, true,
                    false,
                    false, true,
                    false
                }
            },
            {
                DisplaySegmentChars.EIGHT, new bool[]
                {
                    true,
                    true, true,
                    true,
                    true, true,
                    true
                }
            },
            {
                DisplaySegmentChars.NINE, new bool[]
                {
                    true,
                    true, true,
                    true,
                    false, true,
                    true
                }
            },
            {
                DisplaySegmentChars.DASH, new bool[]
                {
                    false,
                    false, false,
                    true,
                    false, false,
                    false
                }
            },
            {
                DisplaySegmentChars.SPACE, new bool[]
                {
                    false,
                    false, false,
                    false,
                    false, false,
                    false
                }
            }
        };
        [Obsolete]
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
        public void SetChar(DisplaySegmentChars character)
        {
            var colors = Chars[character];
            SetColor(TopSegment, colors[0] ? EnabledColor : DisabledColor);
            SetColor(LeftTopSegment, colors[1] ? EnabledColor : DisabledColor);
            SetColor(RightTopSegment, colors[2] ? EnabledColor : DisabledColor);
            SetColor(MiddleSegment, colors[3] ? EnabledColor : DisabledColor);
            SetColor(LeftBottomSegment, colors[4] ? EnabledColor : DisabledColor);
            SetColor(RightBottomSegment, colors[5] ? EnabledColor : DisabledColor);
            SetColor(BottomSegment, colors[6] ? EnabledColor : DisabledColor);
            colors = null;
        }
        public bool TrySetChar(char character)
        {
            switch (character)
            {
                case '0':
                    SetChar(DisplaySegmentChars.ZERO);
                    return true;
                case '1':
                    SetChar(DisplaySegmentChars.ONE);
                    return true;
                case '2':
                    SetChar(DisplaySegmentChars.TWO);
                    return true;
                case '3':
                    SetChar(DisplaySegmentChars.THREE);
                    return true;
                case '4':
                    SetChar(DisplaySegmentChars.FOUR);
                    return true;
                case '5':
                    SetChar(DisplaySegmentChars.FIVE);
                    return true;
                case '6':
                    SetChar(DisplaySegmentChars.SIX);
                    return true;
                case '7':
                    SetChar(DisplaySegmentChars.SEVEN);
                    return true;
                case '8':
                    SetChar(DisplaySegmentChars.EIGHT);
                    return true;
                case '9':
                    SetChar(DisplaySegmentChars.NINE);
                    return true;
                case ' ':
                    SetChar(DisplaySegmentChars.SPACE);
                    return true;
                case '-':
                    SetChar(DisplaySegmentChars.DASH);
                    return true;
            }
            if(Enum.TryParse<DisplaySegmentChars>(character.ToString(), true, out var chara))
            {
                SetChar(chara);
                return true;
            }
            return false;
        }
        public void SetChar(char character)
        {
            if(!TrySetChar(character))
                throw new ArgumentOutOfRangeException(nameof(character),"Cannot display this char on 7-segment display");
        }
        public void SetColor(MeshRenderer obj, Color color)
        {
            obj.material.color = color;
        }
    }
    public enum DisplaySegmentChars
    {
        A,
        B,
        C,
        D,
        E,
        F,
        H,
        I,
        J,
        L,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        Y,
        Z,
        ZERO,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        DASH,
        SPACE
    }
}