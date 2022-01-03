﻿using UnityEngine;

namespace Mistaken.UnityPrefabs
{
    public class AssetMeta : MonoBehaviour
    {
        public AssetType Type = AssetType.UNKNOWN;

        public SpawnRule[] Rules => this.gameObject.GetComponents<SpawnRule>();
    
        public enum AssetType
        {
            UNKNOWN = -1,
            SURFACE_GATEB_BRIDGE_FORWARD,
            SURFACE_GATEB_BRIDGE_LEFT,
            SURFACE_GATEB_BRIDGE_LEFT_BUNKER,
            SURFACE_GATEB_BRIDGE_CONNECTOR,
            SURFACE_GATEA_STAIRS_LOCK,
            SURFACE_GATEA_TUNNEL_CI_DOOR,
            SURFACE_GATEA_TUNNEL_CI_DOOR_LOCKED,
            SURFACE_GATEA_TUNNEL_ELEVATOR_DOOR,
            SURFACE_GATEA_TOWER_ELEVATOR,
            SURFACE_GATEA_TOWER_SCP1499_CHAMBER,
            SURFACE_GATEA_TOWER_ARMORY,
            SURFACE_GATEA_TOWER_ARMORY_BIG,
            SURFACE_CICAR,
            SURFACE_GATEA_MIDDLE_TOWER,
            SURFACE_HELIPAD,
            SURFACE_HELICOPTER,

            EZ_CURVE_ROOM,
            EZ_VENT_MEDICALROOM,

            WARHEAD_TIMER,
        }
    }
}