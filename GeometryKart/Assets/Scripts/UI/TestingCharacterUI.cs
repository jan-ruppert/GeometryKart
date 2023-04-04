using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingCharacterUI : MonoBehaviour
{
    [SerializeField] private Button readyButton;

    private void Awake()
    {
        readyButton.onClick.AddListener((() =>
        {
            CharacterSelectReady.Instance.SetPlayerReady();
        }));
    }
}
