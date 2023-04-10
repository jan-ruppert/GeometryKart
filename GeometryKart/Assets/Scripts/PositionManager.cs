using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PositionManager : NetworkBehaviour
{
    public static PositionManager Instance { get; private set; }

    private Dictionary<ulong, Position> clientPositions;

    private void Awake()
    {
        Instance = this;

        clientPositions = new Dictionary<ulong, Position>();
    }

    private void Update()
    {

        var ordered = clientPositions.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        int i = 1;

        foreach (var clientId in ordered.Keys)
        {
            RaceGameMultiplayer.Instance.SetPlayerPosition(i, clientId);

            i++;
        }
    }

    public void Add(ulong clientId, Position position)
    {
        clientPositions.Add(clientId, position);
    }
    
    
}
