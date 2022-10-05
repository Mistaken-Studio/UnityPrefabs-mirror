using System;
using System.Collections;
using JetBrains.Annotations;
using Mistaken.UnityPrefabs.Misc.Light;
using Mistaken.UnityPrefabs.SegmentDisplay;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Mistaken.UnityPrefabs.WaitingForPlayersRoom
{

    [PublicAPI]
    public class WaitingForPlayersRoomScript : MonoBehaviour
    {
        [SerializeField] private LightObjectScript statusLightGreen;
        [SerializeField] private LightObjectScript statusLightYellow;
        [SerializeField] private LightObjectScript statusLightRed;

        [SerializeField] private MultiSegmentDisplayScript timeHourMinuteDisplay;
        [SerializeField] private MultiSegmentDisplayScript timeSecondDisplay;

        [SerializeField] private MultiSegmentDisplayScript playerCountDisplay;
        [SerializeField] private MultiSegmentDisplayScript maxPlayerCountDisplay;

        [SerializeField] private MultiSegmentDisplayScript timeLeftDisplay;

        public bool LobbyLocked
        {
            set
            {
                statusLightRed.Enabled = value;
                statusLightGreen.Enabled = !value;
            }
        }

        public int PlayerCount
        {
            set
            {
                playerCountDisplay.SetText(value.ToString());
                statusLightYellow.Enabled = value < 2;
            }
        }

        public int MaxPlayerCount
        {
            set => maxPlayerCountDisplay.SetText(value.ToString());
        }

        public int TimeLeft
        {
            set => timeLeftDisplay.SetText(value.ToString());
        }

        private void Start()
        {
            this.StartCoroutine(DateTimeTimerUpdate());
        }

        private IEnumerator DateTimeTimerUpdate()
        {
            var date = DateTime.Now;
            while (this.enabled)
            {
                yield return new WaitForSeconds(1);
                date = date.AddSeconds(1);
                timeHourMinuteDisplay.SetText($"{date.Hour:00}{date.Minute:00}");
                timeSecondDisplay.SetText(date.Second.ToString("00"));
            }
        }
    }
}