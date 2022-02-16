using UnityEngine;

namespace Mistaken.UnityPrefabs
{
    public class AssetMeta : MonoBehaviour
    {
        public AssetType Type = AssetType.UNKNOWN;

        public SpawnRule[] Rules => this.gameObject.GetComponents<SpawnRule>();

        public GameObject[] MovableObjects = new GameObject[0];
        public GameObject[] ColorChangableObjects = new GameObject[0];

        public enum AssetType
        {
            UNKNOWN = -1,
            SURFACE_GATEB_BRIDGE_FORWARD = 0,
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
            SURFACE_GATEA_TUNNEL_DOOR,
            SURFACE_ESCAPE_WORKSTATION,
            SURFACE_SHOOTING_TARGETS,
            SURFACE_ADDITIONAL_LIGHTS,

            EZ_CURVE_ROOM = 1000,
            EZ_VENT_MEDICALROOM,
            EZ_ELECTRICALROOM,

            LCZ_AIRLOCK_MEDICAL_ROOM = 2000,
            LCZ_ARMORY_ADDICTIONAL_MEDKITS,
            LCZ_ARMORY_SHOOTING_TARGET,
         
            HCZ_ARMORY_SHOOTING_TARGET = 3000,

            WARHEAD_TIMER = 9000,

            LIGHT_TEST,
        }
    }
}