using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectUI : MonoBehaviour
{
    [SerializeField] private Button testTrackButton;
    
    [SerializeField] private Button crossTrackButton;


    private void Start()
    {
        AreButtonsActive(NetworkManager.Singleton.IsServer);
    }

    private void Awake()
    {
        testTrackButton.onClick.AddListener((() =>
        {
            Loader.LoadNetwork(Loader.Scene.TestScene);
        }));
        
        crossTrackButton.onClick.AddListener((() =>
        {
            Loader.LoadNetwork(Loader.Scene.GameScene);
        }));
    }

    private void AreButtonsActive(bool active)
    {
        testTrackButton.gameObject.SetActive(active);
        crossTrackButton.gameObject.SetActive(active);
    }
}
