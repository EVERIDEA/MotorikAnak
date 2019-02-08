using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		EventManager.AddListener<DisableAllPopupEvents>(DisableAllPopUp);

        for (int i = 0; i < _FailPopUp.Length; i++) 
		{
            _FailPopUpData.Add(_FailPopUp[i].Id, _FailPopUp[i].ObjectUI);
        }

		foreach (GameplayPopUp popup in _FailPopUp) 
		{
			popup.ObjectUI.AddComponent<Button>().onClick.AddListener(delegate
				{
					Disabler ();
					
                    if (popup.Id=="9")
                    {
                        Debug.Log("Reset The Time And Resume Game");
                        EventManager.TriggerEvent(new ResumeHandlerEvent());
                        EventManager.TriggerEvent(new TimerHandlerEvent(true, 10f));

                    }
                    else
                    {
                        EventManager.TriggerEvent(new RestartLevelEvent());
                    }
				});
		}
    }

    private void FailPopUpListener(FailPopUpEvents e)
    {
        //DO SOME ACTION OF UI OBJECTS
        _FailPopUpData[e.Id].SetActive(e.IsActive);
    }

	private void DisableAllPopUp(DisableAllPopupEvents e)
	{
		Disabler ();
	}

	private void Disabler()
	{
		for (int i = 0; i < _FailPopUp.Length; i++) 
		{
			_FailPopUp[i].ObjectUI.SetActive (false);
		}
	}
}
