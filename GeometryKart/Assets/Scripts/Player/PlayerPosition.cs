using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{

    private int currentCheckpoint;

    private int currentLap;

    private void Awake()
    {
        currentCheckpoint = Track.Instance.StartCheckpoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Checkpoint"))
        {
            Checkpoint();
        }
    }

    private void Checkpoint()
    {
        currentCheckpoint = (currentCheckpoint + 1) % Track.Instance.NumberCheckpoints;

        if (currentCheckpoint == Track.Instance.StartCheckpoint)
        {
            currentLap++;
        }
        
        Debug.Log("Checkpoint:" + currentCheckpoint + ", Lap: " + currentLap);
    }
}
