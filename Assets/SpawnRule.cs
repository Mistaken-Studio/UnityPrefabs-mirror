using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Mistaken.UnityPrefabs
{
    [PublicAPI]
    [RequireComponent(typeof(AssetMeta))]
    public class SpawnRule : MonoBehaviour
    {
        public bool Spawn;
        public RoomType Room;
        public sbyte MaxAmount;
        public sbyte MinAmount;
        public float Chance;
        public List<AssetMeta.AssetType> ColidingAssetTypes; 
        
        public enum RoomType
        {
            Unknown,
            LczArmory,
            LczCurve,
            LczStraight,
            Lcz012,
            Lcz914,
            LczCrossing,
            LczTCross,
            LczCafe,
            LczPlants,
            LczToilets,
            LczAirlock,
            Lcz173,
            LczClassDSpawn,
            LczChkpB,
            LczGlassBox,
            LczChkpA,
            Hcz079,
            HczEzCheckpoint,
            HczArmory,
            Hcz939,
            HczHid,
            Hcz049,
            HczChkpA,
            HczCrossing,
            Hcz106,
            HczNuke,
            HczTesla,
            HczServers,
            HczChkpB,
            HczTCross,
            HczCurve,
            Hcz096,
            EzVent,
            EzIntercom,
            EzGateA,
            EzDownstairsPcs,
            EzCurve,
            EzPcs,
            EzCrossing,
            EzCollapsedTunnel,
            EzConference,
            EzStraight,
            EzCafeteria,
            EzUpstairsPcs,
            EzGateB,
            EzShelter,
            Pocket,
            Surface,
            HczStraight,
            EzTCross
        }
    }
}