using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerModel : NetworkBehaviour
{
    [SerializeField] private GameObject redCar;
    [SerializeField] private GameObject orangeCar;
    [SerializeField] private GameObject greenCar;
    [SerializeField] private GameObject whiteCar;

    private void Start()
    {
        var playerData = RaceGameMultiplayer.Instance.GetPlayerDataFromClientId(OwnerClientId);

        switch (playerData.colorId)
        {
            case 0:
                Instantiate(redCar,gameObject.transform.position, Quaternion.identity, gameObject.transform);
                break;
            case 1:
                Instantiate(greenCar,gameObject.transform.position, Quaternion.identity, gameObject.transform);
                break;
            case 2:
                Instantiate(orangeCar,gameObject.transform.position, Quaternion.identity, gameObject.transform);
                break;
            case 3:
                Instantiate(whiteCar,gameObject.transform.position, Quaternion.identity, gameObject.transform);
                break;
        }
    }
}
