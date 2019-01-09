using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public GameObject linePrefab;
    Line activeLine;
    bool IsStart = false;
	bool CheckLine = false;

	bool StartTimer=false;
	public GameObject TimerObj;
	public Image fillImg;
	float timeAmt = 10;
	float time;
	public Text timeText;

	public Text ScoreText;

    List<GameObject> _LineDrawer = new List<GameObject>();

    private void Awake()
    {
        EventManager.AddListener<InitGameplayEvent>(InitListener);
        EventManager.AddListener<EndGameplayEvent>(EndGameListener);
        EventManager.AddListener<FailHandlerEvent>(FailListener);
        EventManager.AddListener<ResultGameplayEvent>(ResultHandler);
		EventManager.AddListener<NextLevelEvent>(NextLevel);
		EventManager.AddListener<RestartLevelEvent>(Restart);
		EventManager.AddListener<TimerHandlerEvent>(Timer);
		EventManager.AddListener<ScoreHandlerEvent>(Scoring);
    }

	private void Start()
	{
		//time = timeAmt;
	}


    public void InitListener(InitGameplayEvent e) {
        IsStart = true;
    }
		
	void Update ()
    {
        if (IsStart)
        {
			if ((time>0)||(StartTimer==true))
			{
				time -= Time.deltaTime;
				fillImg.fillAmount = time / timeAmt; 
				timeText.text = "Time : "+time.ToString("F");  
			}
			if (time<0) 
			{
				time = 0;
				timeText.text = "Time : 0";
				EventManager.TriggerEvent (new FailPopUpEvents ("2",true));
				StartTimer = false;
				IsStart = false;
			}

			if (Global.Score<0) 
			{
				Global.Score = 0;
			}

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
					EventManager.TriggerEvent (new FailHandlerEvent (EFailType.NotPointTarget));
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
					EventManager.TriggerEvent (new FailHandlerEvent (EFailType.HandsUp));
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
		IsStart = false;
    }

    private void ResultHandler(ResultGameplayEvent e)
    {
        IsStart = false;
        if (e.IsWin)
        {
			EventManager.TriggerEvent(new MainMenuButtonEvent(EMainMenuButton.WIN));
			ScoreText.text = "Your Score : " + Global.Score.ToString ();
        }
        else
        {
            EventManager.TriggerEvent(new MainMenuButtonEvent(EMainMenuButton.FAIL));
        }
    }

    private void FailListener(FailHandlerEvent e)
    {
        switch (e.Type) {
			case EFailType.NotPointTarget:
				EventManager.TriggerEvent (new FailPopUpEvents ("1", true));
				IsStart = false;
				EventManager.TriggerEvent (new ScoreHandlerEvent (-1));
            	break;
            case EFailType.Duration:
				EventManager.TriggerEvent (new FailPopUpEvents ("2", true));
				IsStart = false;
				EventManager.TriggerEvent (new ScoreHandlerEvent (-1));
                break;
            case EFailType.HandsUp:
				EventManager.TriggerEvent (new FailPopUpEvents ("3", true));
				IsStart = false;
				EventManager.TriggerEvent (new ScoreHandlerEvent (-1));
                break;
            case EFailType.CrossLine:
				EventManager.TriggerEvent (new FailPopUpEvents ("4", true));
				IsStart = false;
				EventManager.TriggerEvent (new ScoreHandlerEvent (-1));
                break;
            case EFailType.Backward:
				EventManager.TriggerEvent (new FailPopUpEvents ("5", true));
				IsStart = false;
				EventManager.TriggerEvent (new ScoreHandlerEvent (-1));
                break;
            case EFailType.PointEndTarget:
				EventManager.TriggerEvent (new FailPopUpEvents ("6", true));
				IsStart = false;
				EventManager.TriggerEvent (new ScoreHandlerEvent (-1));
                break;
        }
    }
		
	private void NextLevel(NextLevelEvent e)
	{
		EventManager.TriggerEvent (new GameplayLevelEvents (Global.Level,false));

		Global.Level += 1;

		EventManager.TriggerEvent (new GameplayLevelEvents (Global.Level,true));

		EventManager.TriggerEvent (new EndGameplayEvent ());
		IsStart = true;
		CheckLine = false;
		Debug.Log ("Next");
	}

	private void Restart(RestartLevelEvent e)
	{
		EventManager.TriggerEvent (new GameplayLevelEvents (Global.Level,true));
		EventManager.TriggerEvent (new EndGameplayEvent ());
		IsStart = true;
		CheckLine = false;
		EventManager.TriggerEvent (new TimerHandlerEvent (true, 10f));
		Debug.Log ("Restart");
	}

	private void Timer(TimerHandlerEvent e)
	{
		if (e.IsOn)
		{
			//Timer is On, insert amount of time
			time = e.TimeLimit;	
			TimerObj.SetActive (true);
			StartTimer = true;
			//fillImg.fillAmount =1;

		} 
		else
		{
			//Do Something to make time off
			time = e.TimeLimit;
			TimerObj.SetActive (false);
			StartTimer = false;
		}
	}

	public void Scoring(ScoreHandlerEvent e)
	{
		Global.Score += e.Value;
	}
}
