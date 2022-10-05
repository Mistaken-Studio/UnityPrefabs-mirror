using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Mistaken.UnityPrefabs
{
    [PublicAPI]
    public class AnimatorTrigger : MonoBehaviour
    {
        public Animator Animator;
        public string Name = "IsOpen";
        public bool Toggle = true;
        public bool Value = true;
    }
}
