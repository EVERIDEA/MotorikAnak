using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameplayPopUp {
    public string Id;
    public GameObject ObjectUI;
}

public class GameplayUIManager : MonoBehaviour {
    [SerializeField]
    GameplayPopUp[] _FailPopUp;
    Dictionary<string, GameObject> _FailPopUpData = new Dictionary<string, GameObject>();

    private void Awake()
    {
        EventManager.AddListener<FailPopUpEvents>(FailPopUpListener);

        for (int i = 0; i < _FailPopUp.Length; i++) {
            _FailPopUpData.Add(_FailPopUp[i].Id, _FailPopUp[i].ObjectUI);
        }
    }

    private void FailPopUpListener(FailPopUpEvents e)
    {
        //DO SOME ACTION OF UI OBJECTS
        _FailPopUpData[e.Id].SetActive(e.IsActive);
    }
}
