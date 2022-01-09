﻿using System;
using UnityEngine;

namespace Mistaken.UnityPrefabs
{
    public class Door : MonoBehaviour
    {
        public bool DefaultState;
        public bool Locked;
        public bool Breakable;
        public KeycardPermissions Permissions;
        public bool LockAfterUse;
        public bool DestroyAfterUse;

        [Flags]
        public enum KeycardPermissions : ushort
        {
            None = 0,
            Checkpoints = 1,
            ExitGates = 2,
            Intercom = 4,
            AlphaWarhead = 8,
            ContainmentLevelOne = 16,
            ContainmentLevelTwo = 32,
            ContainmentLevelThree = 64,
            ArmoryLevelOne = 128,
            ArmoryLevelTwo = 256,
            ArmoryLevelThree = 512,
            ScpOverride = 1024
        }
    }
}