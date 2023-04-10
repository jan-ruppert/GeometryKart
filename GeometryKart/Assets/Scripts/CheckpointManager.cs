using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    private List<Checkpoint> checkpointList;

    public List<Checkpoint> CheckpointList => checkpointList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitCheckpointList();
    }

    private void InitCheckpointList()
    {
        checkpointList = new List<Checkpoint>();
        
        foreach (Transform checkpointObject in gameObject.transform)
        {
            checkpointList.Add(checkpointObject.GetComponent<Checkpoint>());
        }
    }

    public Vector3 GetCheckpointPositionFromId(int id)
    {
        foreach (var checkpoint in checkpointList)
        {
            if (checkpoint.CheckpointId == id)
            {
                return checkpoint.transform.position;
            }
        }

        return Vector3.zero;
    }
}
