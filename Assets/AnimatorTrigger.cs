using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistaken.UnityPrefabs
{
    public class AnimatorTrigger : MonoBehaviour
    {
        public Animator Animator;
        public string Name = "IsOpen";
        public bool Toggle = true;
        public bool Value = true;
    }
}
