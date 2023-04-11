using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class LapUI : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI lapText;
    
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            gameObject.SetActive(false);
        }
    }
    
    private void Awake()
    {
        RaceGameMultiplayer.Instance.OnPlayerDataNetworkListChanged += RaceGameMultiplayer_OnPlayerDataNetworkListChanged;
    }

    private void RaceGameMultiplayer_OnPlayerDataNetworkListChanged(object sender, EventArgs e)
    {
        var playerData = RaceGameMultiplayer.Instance.GetPlayerDataFromClientId(OwnerClientId);

        UpdateLapText(playerData.currentLap, Track.Instance.NumberLaps);
    }

    private void UpdateLapText(int currentLap, int lastLap)
    {
        lapText.text = "Lap " + currentLap + "/" + lastLap;
    }
}
