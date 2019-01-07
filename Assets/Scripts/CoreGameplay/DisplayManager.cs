using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DisplayObjectClass
{
    public GameObject DisplayObjectHUD;
    public string NameTag;
}

public class DisplayManager : MonoBehaviour
{
    [SerializeField]
    bool _IsTest;

    [SerializeField]
    DisplayObjectClass[] _DisplayList;

    Dictionary<string, GameObject> _ListOfDisplay = new Dictionary<string, GameObject>();

    
    // Use this for initialization
    void Awake ()
    {
        EventManager.AddListener<MainMenuButtonEvent>(MainMenuButtonListener);

        foreach(DisplayObjectClass x in _DisplayList)
        {
            _ListOfDisplay.Add(x.NameTag, x.DisplayObjectHUD);
        }
	}

    private void Start()
    {
        if (!_IsTest)
            MainMenuButtonListener(new MainMenuButtonEvent(EMainMenuButton.RESTART));
    }

    void MainMenuButtonListener(MainMenuButtonEvent e)
    {
        switch(e.Type)
        {
            case EMainMenuButton.MAIN_MENU_PLAY:
                _ListOfDisplay["MainMenu"].SetActive(false);
                _ListOfDisplay["LevelSelect"].SetActive(true);
                break;
            case EMainMenuButton.HELP:
                _ListOfDisplay["HelpInstruction"].SetActive(e.IsActive);
                break;
            case EMainMenuButton.EXIT:
                Debug.Log("EXIT BUTTON");
                break;
            case EMainMenuButton.PAUSE:
                _ListOfDisplay["PauseMenu"].SetActive(e.IsActive);
                if(e.IsActive)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
                break;
            case EMainMenuButton.RESTART:
                foreach(var x in _ListOfDisplay)
                {
                    if(x.Key == "MainMenu")
                        x.Value.SetActive(true);
                    else
                        x.Value.SetActive(false);

                    EventManager.TriggerEvent(new EndGameplayEvent());
                }
                break;
			/*
            case EMainMenuButton.START_GAME:
                _ListOfDisplay["LevelSelect"].SetActive(false);
                if(e.IsActive)
                {
                    _ListOfDisplay["Gameplay"].SetActive(true);
                    EventManager.TriggerEvent(new InitGameplayEvent());
                }
                else
                {
                    _ListOfDisplay["MainMenu"].SetActive(true);
                }
                break;
            */
			case EMainMenuButton.LEVEL1:
				_ListOfDisplay["LevelSelect"].SetActive(false);
				if(e.IsActive)
				{
					_ListOfDisplay["Gameplay"].SetActive(true);
					EventManager.TriggerEvent (new GameplayLevelEvents (1,true));
					EventManager.TriggerEvent(new InitGameplayEvent());
				}
				else
				{
				_ListOfDisplay["MainMenu"].SetActive(true);
				}
				break;
			case EMainMenuButton.LEVEL2:
				_ListOfDisplay["LevelSelect"].SetActive(false);
				if(e.IsActive)
				{
					_ListOfDisplay["Gameplay"].SetActive(true);
					EventManager.TriggerEvent (new GameplayLevelEvents (2,true));
					EventManager.TriggerEvent(new InitGameplayEvent());
				}
				else
				{
					_ListOfDisplay["MainMenu"].SetActive(true);
				}
				break;
			case EMainMenuButton.LEVEL3:
				_ListOfDisplay["LevelSelect"].SetActive(false);
				if(e.IsActive)
				{
					_ListOfDisplay["Gameplay"].SetActive(true);
					EventManager.TriggerEvent (new GameplayLevelEvents (3,true));
					EventManager.TriggerEvent(new InitGameplayEvent());
				}
				else
				{
					_ListOfDisplay["MainMenu"].SetActive(true);
				}
				break;
			case EMainMenuButton.LEVEL4:
				_ListOfDisplay["LevelSelect"].SetActive(false);
				if(e.IsActive)
				{
					_ListOfDisplay["Gameplay"].SetActive(true);
					EventManager.TriggerEvent (new GameplayLevelEvents (4,true));
					EventManager.TriggerEvent(new InitGameplayEvent());
				}
				else
				{
					_ListOfDisplay["MainMenu"].SetActive(true);
				}
				break;
			case EMainMenuButton.LEVEL5:
				_ListOfDisplay["LevelSelect"].SetActive(false);
				if(e.IsActive)
				{
					_ListOfDisplay["Gameplay"].SetActive(true);
					EventManager.TriggerEvent (new GameplayLevelEvents (5,true));
					EventManager.TriggerEvent(new InitGameplayEvent());
				}
				else
				{
					_ListOfDisplay["MainMenu"].SetActive(true);
				}
				break;
			case EMainMenuButton.LEVEL6:
				_ListOfDisplay["LevelSelect"].SetActive(false);
				if(e.IsActive)
				{
					_ListOfDisplay["Gameplay"].SetActive(true);
					EventManager.TriggerEvent (new GameplayLevelEvents (6,true));
					EventManager.TriggerEvent(new InitGameplayEvent());
				}
				else
				{
					_ListOfDisplay["MainMenu"].SetActive(true);
				}
				break;
			case EMainMenuButton.LEVEL7:
				_ListOfDisplay["LevelSelect"].SetActive(false);
				if(e.IsActive)
				{
					_ListOfDisplay["Gameplay"].SetActive(true);
					EventManager.TriggerEvent (new GameplayLevelEvents (7,true));
					EventManager.TriggerEvent(new InitGameplayEvent());
				}
				else
				{
					_ListOfDisplay["MainMenu"].SetActive(true);
				}
				break;
			case EMainMenuButton.LEVEL8:
				_ListOfDisplay["LevelSelect"].SetActive(false);
				if(e.IsActive)
				{
					_ListOfDisplay["Gameplay"].SetActive(true);
					EventManager.TriggerEvent (new GameplayLevelEvents (8,true));
					EventManager.TriggerEvent(new InitGameplayEvent());
				}
				else
				{
					_ListOfDisplay["MainMenu"].SetActive(true);
				}
				break;
			case EMainMenuButton.LEVEL9:
				_ListOfDisplay["LevelSelect"].SetActive(false);
				if(e.IsActive)
				{
					_ListOfDisplay["Gameplay"].SetActive(true);
					EventManager.TriggerEvent (new GameplayLevelEvents (9,true));
					EventManager.TriggerEvent(new InitGameplayEvent());
				}
				else
				{
					_ListOfDisplay["MainMenu"].SetActive(true);
				}
				break;
			case EMainMenuButton.LEVEL10:
				_ListOfDisplay["LevelSelect"].SetActive(false);
				if(e.IsActive)
				{
					_ListOfDisplay["Gameplay"].SetActive(true);
					EventManager.TriggerEvent (new GameplayLevelEvents (10,true));
					EventManager.TriggerEvent(new InitGameplayEvent());
				}
				else
				{
					_ListOfDisplay["MainMenu"].SetActive(true);
				}
				break;
            case EMainMenuButton.WIN:
                _ListOfDisplay["WinUI"].SetActive(true);
                break;
            case EMainMenuButton.FAIL:
                _ListOfDisplay["FailUI"].SetActive(true);
                break;
			case EMainMenuButton.NEXT:
				EventManager.TriggerEvent (new NextLevelEvent ());
				_ListOfDisplay["WinUI"].SetActive(false);
				break;
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
            MainMenuButtonListener(new MainMenuButtonEvent(EMainMenuButton.RESTART));
    }
}