using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistaken.UnityPrefabs
{
    public class Item : MonoBehaviour
    {
        public ItemType Type = ItemType.None;
        public ushort Ammo = 0;
        public bool DestroyAfterUse = false;
        public bool Locked = false;
        public bool HasRb = false;

        public enum ItemType
        {
            None = -1,
            KeycardJanitor,
            KeycardScientist,
            KeycardResearchCoordinator,
            KeycardZoneManager,
            KeycardGuard,
            KeycardNTFOfficer,
            KeycardContainmentEngineer,
            KeycardNTFLieutenant,
            KeycardNTFCommander,
            KeycardFacilityManager,
            KeycardChaosInsurgency,
            KeycardO5,
            Radio,
            GunCOM15,
            Medkit,
            Flashlight,
            MicroHID,
            SCP500,
            SCP207,
            Ammo12gauge,
            GunE11SR,
            GunCrossvec,
            Ammo556x45,
            GunFSP9,
            GunLogicer,
            GrenadeHE,
            GrenadeFlash,
            Ammo44cal,
            Ammo762x39,
            Ammo9x19,
            GunCOM18,
            SCP018,
            SCP268,
            Adrenaline,
            Painkillers,
            Coin,
            ArmorLight,
            ArmorCombat,
            ArmorHeavy,
            GunRevolver,
            GunAK,
            GunShotgun,
            SCP330,
            SCP2176
        }
    }
}
