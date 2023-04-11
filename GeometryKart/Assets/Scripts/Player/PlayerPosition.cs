using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerPosition : NetworkBehaviour
{

    private Position position;
    
    public Position Position => position;

    private void Awake()
    {
        position = new Position(Track.Instance.StartCheckpoint, 0, 0);
    }

    public override void OnNetworkSpawn()
    {
        PositionManager.Instance.Add(OwnerClientId, position);
    }

    private void Update()
    {
        position.DistanceToNextCheckpoint = Vector3.Distance(gameObject.transform.position,
            CheckpointManager.Instance.GetCheckpointPositionFromId(NextCheckpoint()));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Checkpoint") && other.gameObject.GetComponent<Checkpoint>().CheckpointId == NextCheckpoint())
        {
            Checkpoint();
        }
    }

    private void Checkpoint()
    {
        position.CurrentCheckpoint = NextCheckpoint();

        if (position.CurrentCheckpoint == Track.Instance.StartCheckpoint)
        {
            position.CurrentLap++;
            
            RaceGameMultiplayer.Instance.SetPlayerLap(position.CurrentLap);
        }
        
        Debug.Log("Checkpoint:" + position.CurrentCheckpoint + ", Lap: " + position.CurrentLap);
    }

    private int NextCheckpoint()
    {
        return (position.CurrentCheckpoint + 1) % Track.Instance.NumberCheckpoints;
    }
}
