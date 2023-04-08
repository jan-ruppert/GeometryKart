using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Track : MonoBehaviour
{
    public static Track Instance { get; private set; }
    
    [SerializeField] private byte numberLaps;

    public int NumberLaps => numberLaps;
    
    [SerializeField] private int numberCheckpoints;

    public int NumberCheckpoints => numberCheckpoints;
    
    [SerializeField] private int startCheckpoint;

    public int StartCheckpoint => startCheckpoint;

    private void Awake()
    {
        Instance = this;
    }
}
