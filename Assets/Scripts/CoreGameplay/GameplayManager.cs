using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject linePrefab;
    Line activeLine;

    bool IsStart = false;
	bool CheckLine = false;

    List<GameObject> _LineDrawer = new List<GameObject>();

	public List <Level> _Level;

	[SerializeField]
	int _ThisLevel;
	[SerializeField]
	int _NextLevel;


    private void Awake()
    {
        EventManager.AddListener<InitGameplayEvent>(InitListener);
        EventManager.AddListener<EndGameplayEvent>(EndGameListener);
        EventManager.AddListener<FailHandlerEvent>(FailListener);
        EventManager.AddListener<ResultGameplayEvent>(ResultHandler);
		EventManager.AddListener<NextLevelEvent>(NextLevel);
    }

	private void Start()
	{
		LevelHandler ();
	}


    public void InitListener(InitGameplayEvent e) {
        IsStart = true;
    }
		
	void Update ()
    {
        if (IsStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                Debug.Log(mousePos);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.transform == null)
                    return;
				if ((hit.transform.gameObject.name == "A")||(hit.transform.gameObject.name == "Line(Clone)"))
                {
                    Debug.Log("A");
					CheckLine = true;
                }
                else
                {
					EventManager.TriggerEvent (new FailPopUpEvents ("1", true));
                    return; //Fail
                }
                //if (_LineDrawer.Count >= 1)
                //    return;


                GameObject lineGo = Instantiate(linePrefab);
                _LineDrawer.Add(lineGo);

                activeLine = lineGo.GetComponent<Line>();
                lineGo.transform.parent = this.gameObject.transform;
            }

            if (Input.GetMouseButtonUp(0))
            {
                activeLine = null;
				if (CheckLine==true) 
				{
					EventManager.TriggerEvent (new FailPopUpEvents ("3", true));
					CheckLine = false;
				}
            }

            /*
             * Take mouse position and feed it to updateline
             */
            if (activeLine != null)
            {
                Vector2 x = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(x);
            }
        }
    }

    private void EndGameListener(EndGameplayEvent e)
    {
        for (int i = 0; i < _LineDrawer.Count; i++) {
            Destroy(_LineDrawer[i]);
        }

        _LineDrawer = new List<GameObject>();
    }

    private void ResultHandler(ResultGameplayEvent e)
    {
        IsStart = false;
        if (e.IsWin)
        {
			EventManager.TriggerEvent(new MainMenuButtonEvent(EMainMenuButton.WIN));
        }
        else
        {
            EventManager.TriggerEvent(new MainMenuButtonEvent(EMainMenuButton.FAIL));
        }
    }

    private void FailListener(FailHandlerEvent e)
    {
        ResultHandler (new 
			ResultGameplayEvent(true));
        switch (e.Type) {
            case EFailType.NotPointTarget:

                break;
            case EFailType.Duration:

                break;
            case EFailType.HandsUp:

                break;
            case EFailType.CrossLine:

                break;
            case EFailType.Backward:

                break;
            case EFailType.PointEndTarget:

                break;
        }
    }

	private void LevelHandler()
	{
		_ThisLevel = Global.Level;
		_Level [_ThisLevel].LevelObject.SetActive (true);

	}

	private void NextLevel(NextLevelEvent e)
	{
		_Level [_ThisLevel].LevelObject.SetActive (false);
		_ThisLevel += 1;
		_Level [_ThisLevel].LevelObject.SetActive (true);
		EventManager.TriggerEvent (new EndGameplayEvent ());
		IsStart = true;
		CheckLine = false;
		Debug.Log ("Next");
	}
}
