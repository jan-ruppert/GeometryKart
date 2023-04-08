using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private byte checkpointId;

    public byte CheckpointId => checkpointId;
}
