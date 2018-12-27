using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayButton : MonoBehaviour
{
    [SerializeField]
    ButtonAction[] _ResultUIButton;

    // Use this for initialization
    void Awake()
    {
        InitOnAwake();
    }

    public void InitOnAwake()
    {
        foreach (ButtonAction btn in _ResultUIButton)
        {
            btn.ButtonObject.AddComponent<Button>().onClick.AddListener(delegate
            {
                EventManager.TriggerEvent(new MainMenuButtonEvent(btn.Type, btn.IsObjectActive));
            });
        }
    }
}
