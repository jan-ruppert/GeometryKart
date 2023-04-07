using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public enum CarColor
    {
        RED,
        GREEN,
        ORANGE,
        WHITE
    }

    [SerializeField] private CarColor carColor;

    [SerializeField] private GameObject redCar;
    [SerializeField] private GameObject orangeCar;
    [SerializeField] private GameObject greenCar;
    [SerializeField] private GameObject whiteCar;

    private void Awake()
    {
        switch (carColor)
        {
            case CarColor.RED:
                Instantiate(redCar,gameObject.transform.position, Quaternion.identity, gameObject.transform);
                break;
            case CarColor.GREEN:
                Instantiate(greenCar,gameObject.transform.position, Quaternion.identity, gameObject.transform);
                break;
            case CarColor.ORANGE:
                Instantiate(orangeCar,gameObject.transform.position, Quaternion.identity, gameObject.transform);
                break;
            case CarColor.WHITE:
                Instantiate(whiteCar,gameObject.transform.position, Quaternion.identity, gameObject.transform);
                break;
        }
    }
}
