using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceGameManager : NetworkBehaviour
{
    public static RaceGameManager Instance { get; private set; }


    [SerializeField] private Transform playerPrefab;

    [SerializeField] private GameObject playerSpawnParent;
    
    private List<Transform> playerSpawnList;

    private void Awake()
    {
        Instance = this;

        playerSpawnList = new List<Transform>();

        foreach (Transform playerSpawn in playerSpawnParent.transform)
        {
            playerSpawnList.Add(playerSpawn);
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
        }
            
    }

    private void SceneManager_OnLoadEventCompleted(string sceneName,
        UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, List<ulong> a, List<ulong> b)
    {
        foreach (var clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            Transform playerTransform = Instantiate(playerPrefab, playerSpawnList[RaceGameMultiplayer.Instance.GetPlayerDataFromClientId(clientId).colorId]);
            
            playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
        }
    }
}
