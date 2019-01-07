using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameplayLevel {
	public int Id;
	public GameObject Level;
}


public class LevelSelectMenuButton : MonoBehaviour
{
	[SerializeField]
	GameplayLevel[] _GameplayLevel;
	Dictionary<int, GameObject> _GameplayLevelData = new Dictionary<int, GameObject>();

    [SerializeField]
    ButtonAction[] _LevelSelectButton;

    // Use this for initialization
    void Awake()
    {
		EventManager.AddListener<GameplayLevelEvents>(LevelHandler);
		for (int i = 0; i < _GameplayLevel.Length; i++) 
		{
			_GameplayLevelData.Add(_GameplayLevel[i].Id, _GameplayLevel[i].Level);
		}
		
        InitOnAwake();
    }

    public void InitOnAwake()
    {
        foreach (ButtonAction btn in _LevelSelectButton)
        {
            btn.ButtonObject.AddComponent<Button>().onClick.AddListener(delegate
            {
                EventManager.TriggerEvent(new MainMenuButtonEvent(btn.Type, btn.IsObjectActive));
            });
        }
    }

	private void LevelHandler(GameplayLevelEvents e)
	{
		_GameplayLevelData [e.Id].SetActive (e.IsActive);
	}
}
