﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour, IButtonInit {

    [SerializeField]
    ButtonAction[] _MainMenuBtn;

    // Use this for initialization
    void Awake()
    {
        InitOnAwake();
    }

    public void InitOnAwake()
    {
        foreach (ButtonAction btn in _MainMenuBtn)
        {
            btn.ButtonObject.AddComponent<Button>().onClick.AddListener(delegate
            {
                EventManager.TriggerEvent(new MainMenuButtonEvent(btn.Type));
            });
        }
    }
}
