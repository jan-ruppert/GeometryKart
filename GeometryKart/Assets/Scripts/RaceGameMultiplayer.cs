using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceGameMultiplayer : NetworkBehaviour
{
    private const int MAX_PLAYER_COUNT = 4;
    public static RaceGameMultiplayer Instance { get; private set; }

    public event EventHandler OnTryingToJoinGame;
    public event EventHandler OnFailedToJoinGame;
    public event EventHandler OnPlayerDataNetworkListChanged;


    private NetworkList<PlayerData> playerDataNetworkList;

    private ushort colorID;

    private void Awake()
    {
        Instance = this;
        
        DontDestroyOnLoad(gameObject);

        playerDataNetworkList = new NetworkList<PlayerData>();
        playerDataNetworkList.OnListChanged += PlayerDataNetworkList_OnOnListChanged;
    }

    private void PlayerDataNetworkList_OnOnListChanged(NetworkListEvent<PlayerData> changeevent)
    {
        OnPlayerDataNetworkListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void StartHost()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += NetworkManager_ConnectionApprovalCallback;
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        NetworkManager.Singleton.StartHost();
    }

    private void NetworkManager_OnClientConnectedCallback(ulong clientId)
    {
        playerDataNetworkList.Add(new PlayerData
        {
            clientId = clientId,
            colorId = colorID
        });
        
        Debug.Log("client "+ clientId + ", " + colorID);

        colorID++;
    }

    public void StartClient()
    {
        OnTryingToJoinGame?.Invoke(this, EventArgs.Empty);

        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;

        NetworkManager.Singleton.StartClient();
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong obj)
    {
        colorID--;
        
        OnFailedToJoinGame?.Invoke(this, EventArgs.Empty);
    }

    private void NetworkManager_ConnectionApprovalCallback(
        NetworkManager.ConnectionApprovalRequest connectionApprovalRequest, NetworkManager.ConnectionApprovalResponse connectionApprovalResponse)
    {
        
        if (SceneManager.GetActiveScene().name != Loader.Scene.CharacterSelectScene.ToString())
        {
            connectionApprovalResponse.Approved = false;
            connectionApprovalResponse.Reason = "Game has already started";
            return;
        }

        if (NetworkManager.Singleton.ConnectedClientsIds.Count >= MAX_PLAYER_COUNT)
        {
            connectionApprovalResponse.Approved = false;
            connectionApprovalResponse.Reason = "Game is full";
            return;
        }
        connectionApprovalResponse.Approved = true;
    }

    public bool IsPlayerIndexConnected(int playerIndex)
    {
        return playerIndex < playerDataNetworkList.Count;
    }

    public PlayerData GetPlayerDataFromPlayerIndex(int playerIndex)
    {
        return playerDataNetworkList[playerIndex];
    }

    public PlayerData GetPlayerDataFromClientId(ulong clientId)
    {
        foreach (var playerData in playerDataNetworkList)
        {
            if (playerData.clientId == clientId)
            {
                return playerData;
            }
        }

        return default;
    }

    public PlayerData GetPlayerData()
    {
        return GetPlayerDataFromClientId(NetworkManager.Singleton.LocalClientId);
    }
}
