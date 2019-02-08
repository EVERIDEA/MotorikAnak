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

	public List <MidpointBehaviour> _Midpoint = new List<MidpointBehaviour>();

    public List <FreepointBehaviour> _Freepoint = new List<FreepointBehaviour>();


    List<GameObject> _LineDrawer = new List<GameObject>();

    public string SelectedGameType;

    private Vector3 firstNode;

    public GameObject pointGroup;

    private bool shapeCompleted=false;

    public SpriteRenderer ChangeShape;

    public Sprite FirstShape;
    public Sprite SecondShape;

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
		EventManager.AddListener<MidPointHandlerEvent>(CrossedMidpoint);
		EventManager.AddListener<StartLevelEvent>(StartLevel);
        EventManager.AddListener<GameplayTypeHandlerEvent>(GameplayTypeListener);
        EventManager.AddListener<FreePointHandlerEvent>(CrossedFreepoint);
        EventManager.AddListener<ResumeHandlerEvent>(Resume);
    }

	private void Start()
	{

	}


    public void InitListener(InitGameplayEvent e) {
        IsStart = true;
    }
		
	void Update ()
    {
        if (SelectedGameType == "Line Draw")
        {
            if (IsStart)
            {
                if ((time > 0) || (StartTimer == true))
                {
                    time -= Time.deltaTime;
                    fillImg.fillAmount = time / timeAmt;
                    timeText.text = "Time : " + time.ToString("F");
                }
                if (time < 0)
                {
                    time = 0;
                    timeText.text = "Time : 0";
                    EventManager.TriggerEvent(new FailPopUpEvents("2", true));
                    StartTimer = false;
                    IsStart = false;
                }

                if (Global.Score < 0)
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
                    if ((hit.transform.gameObject.name == "A") || (hit.transform.gameObject.name == "Line(Clone)"))
                    {
                        Debug.Log("A");
                        CheckLine = true;
                    }
                    else
                    {
                        EventManager.TriggerEvent(new FailHandlerEvent(EFailType.NotPointTarget));
                        return; //Fail
                    }

                    GameObject lineGo = Instantiate(linePrefab);
                    _LineDrawer.Add(lineGo);

                    activeLine = lineGo.GetComponent<Line>();
                    lineGo.transform.parent = this.gameObject.transform;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    activeLine = null;
                    if (CheckLine == true)
                    {
                        EventManager.TriggerEvent(new FailHandlerEvent(EFailType.HandsUp));
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

                if (_Midpoint.Count != 0)
                {
                    Global.MidpointCount = true;
                    //ada isinya
                }

                if (_Midpoint.Count == 0)
                {
                    Global.MidpointCount = false;
                    //kosong
                }

            }
        }

        if (SelectedGameType == "Find Shape")
        {
            if (IsStart)
            {
                if ((time > 0) || (StartTimer == true))
                {
                    time -= Time.deltaTime;
                    fillImg.fillAmount = time / timeAmt;
                    timeText.text = "Time : " + time.ToString("F");
                }
                if (time < 0)
                {
                    time = 0;
                    timeText.text = "Time : 0";
                    EventManager.TriggerEvent(new FailPopUpEvents("2", true));
                    StartTimer = false;
                    IsStart = false;
                }

                if (Global.Score < 0)
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
                    if (hit.transform.gameObject.name == "Point")
                    {
                        Debug.Log("A");
                        firstNode = mousePos;
                        CheckLine = true;
                    }
                    else
                    {
                        //EventManager.TriggerEvent(new FailHandlerEvent(EFailType.NotPointTarget));
                        return; //Fail
                    }

                    GameObject lineGo = Instantiate(linePrefab);
                    _LineDrawer.Add(lineGo);

                    activeLine = lineGo.GetComponent<Line>();
                    lineGo.transform.parent = this.gameObject.transform;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    activeLine = null;
                    if (CheckLine == true)
                    {
                        EventManager.TriggerEvent(new FailHandlerEvent(EFailType.HandsUp));
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
                    if (Global.FreepointCount == false)
                    {
                        EventManager.TriggerEvent(new ResultGameplayEvent(true));
                        activeLine.UpdateLine(firstNode);
                    }
                }

                if (_Freepoint.Count != 0)
                {
                    Global.FreepointCount = true;
                    //There Is Freepoint
                }

                if (_Freepoint.Count == 0)
                {
                    Global.FreepointCount = false;
                    //No Freepoint
                }
            }
        }

        if (SelectedGameType == "Find Shape2")
        {
            if (IsStart)
            {
                if ((time > 0) || (StartTimer == true))
                {
                    time -= Time.deltaTime;
                    fillImg.fillAmount = time / timeAmt;
                    timeText.text = "Time : " + time.ToString("F");
                }
                if (time < 0)
                {
                    time = 0;
                    timeText.text = "Time : 0";
                    EventManager.TriggerEvent(new FailPopUpEvents("2", true));
                    StartTimer = false;
                    IsStart = false;
                }

                if (Global.Score < 0)
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
                    if (hit.transform.gameObject.name == "Point")
                    {
                        Debug.Log("A");
                        firstNode = mousePos;
                        CheckLine = true;
                    }
                    else
                    {
                        //EventManager.TriggerEvent(new FailHandlerEvent(EFailType.NotPointTarget));
                        return; //Fail
                    }

                    GameObject lineGo = Instantiate(linePrefab);
                    _LineDrawer.Add(lineGo);

                    activeLine = lineGo.GetComponent<Line>();
                    lineGo.transform.parent = this.gameObject.transform;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    activeLine = null;
                    if (CheckLine == true)
                    {
                        EventManager.TriggerEvent(new FailHandlerEvent(EFailType.HandsUp));
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
                    if (Global.FreepointCount == false)
                    {
                        if (shapeCompleted==true)
                        {
                            EventManager.TriggerEvent(new ResultGameplayEvent(true));
                        }
                        else
                        {
                            activeLine.UpdateLine(firstNode);
                            pointGroup.SetActive(true);
                            EventManager.TriggerEvent(new FailHandlerEvent(EFailType.FreepointCompleted));
                            ChangeShape.sprite = SecondShape;
                            shapeCompleted = true;
                        }
                    }
                }

                if (_Freepoint.Count != 0)
                {
                    Global.FreepointCount = true;
                    //There Is Freepoint
                }

                if (_Freepoint.Count == 0)
                {
                    Global.FreepointCount = false;
                    //No Freepoint
                }
            }

        }

        if (SelectedGameType == "Find The Difference")
        {
            if (IsStart)
            {
                //Do Something
            }

        }

        if (SelectedGameType == "Find The Difference")
        {
            if (IsStart)
            {
                //Do Something
            }
        }

        if (SelectedGameType == "Line Draw No Start End")
        {
            if (IsStart)
            {
                //Do Something
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
		_Midpoint.Clear();
        _Freepoint.Clear();
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
			case EFailType.HitBlocker:
				EventManager.TriggerEvent (new FailPopUpEvents ("7", true));
				IsStart = false;
				EventManager.TriggerEvent (new ScoreHandlerEvent (-1));
				break;
			case EFailType.MidpointSkipped:
				EventManager.TriggerEvent (new FailPopUpEvents ("8", true));
				IsStart = false;
				EventManager.TriggerEvent (new ScoreHandlerEvent (-1));
				break;
            case EFailType.FreepointCompleted:
                EventManager.TriggerEvent (new FailPopUpEvents("9", true));
                IsStart = false;
                break;
        }
    }
		
	private void NextLevel(NextLevelEvent e)
	{
		if (Global.Level==24) 
		{
			EventManager.TriggerEvent (new GameplayLevelEvents (Global.Level,false));

			Global.Level = 1;

			EventManager.TriggerEvent (new GameplayLevelEvents (Global.Level,true));

			EventManager.TriggerEvent (new EndGameplayEvent ());
            //IsStart = true;
            //CheckLine = false;
            EventManager.TriggerEvent(new StartLevelEvent());
            Debug.Log ("Hit the last level, Level will be looped");
		}
		else 
		{
			EventManager.TriggerEvent (new GameplayLevelEvents (Global.Level,false));

			Global.Level += 1;

			EventManager.TriggerEvent (new GameplayLevelEvents (Global.Level,true));

			EventManager.TriggerEvent (new EndGameplayEvent ());
            //IsStart = true;
            //CheckLine = false;
            EventManager.TriggerEvent(new StartLevelEvent());
            Debug.Log ("Next");
		}

	}

	private void StartLevel(StartLevelEvent e)
	{
		EventManager.TriggerEvent (new GameplayLevelEvents (Global.Level,false));
		EventManager.TriggerEvent (new EndGameplayEvent ());
        _Midpoint.Clear();
        pointGroup.SetActive(false);
        _Freepoint.Clear();
        EventManager.TriggerEvent (new GameplayLevelEvents (Global.Level,true));
		EventManager.TriggerEvent (new DisableAllPopupEvents ());
		IsStart = true;
		CheckLine = false;
        Global.MidpointCount = false;
        Global.FreepointCount = false;
        shapeCompleted = false;
        ChangeShape.sprite = FirstShape;
        Debug.Log ("Start Level");
	}

	private void Restart(RestartLevelEvent e)
	{
		EventManager.TriggerEvent (new GameplayLevelEvents (Global.Level,false));
		EventManager.TriggerEvent (new EndGameplayEvent ());
        _Midpoint.Clear();
        pointGroup.SetActive(false);
        _Freepoint.Clear();
        EventManager.TriggerEvent (new GameplayLevelEvents (Global.Level,true));
		IsStart = true;
		CheckLine = false;
        Global.MidpointCount = false;
        Global.FreepointCount = false;
        EventManager.TriggerEvent (new TimerHandlerEvent (true, 10f));
        shapeCompleted = false;
        ChangeShape.sprite = FirstShape;
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

	public void CrossedMidpoint(MidPointHandlerEvent e)
	{
		if (e.IsCrossed) 
		{
			_Midpoint.Add (e.Midpoint.GetComponent<MidpointBehaviour>());
		} 
		else 
		{
			_Midpoint.Remove (e.Midpoint.GetComponent<MidpointBehaviour>());
		}
	}

    public void GameplayTypeListener(GameplayTypeHandlerEvent e)
    {
        switch (e.Type)
        {
            case GameplayType.LINE_DRAW_MECHANIC:
                SelectedGameType = "Line Draw";
                break;
            case GameplayType.FIND_SHAPE_MECHANIC:
                SelectedGameType = "Find Shape";
                break;
            case GameplayType.FIND_SHAPE_MECHANIC2:
                SelectedGameType = "Find Shape2";
                break;
            case GameplayType.FIND_THE_DIFFERENCE_MECHANIC:
                SelectedGameType = "Find The Difference";
                break;
            case GameplayType.LINE_DRAW_MECHANIC_IGNORE_START_END_POINT:
                SelectedGameType = "Line Draw No Start End";
                break;
        }
    }

    public void CrossedFreepoint(FreePointHandlerEvent e)
    {
        if (e.IsCrossed)
        {
            _Freepoint.Add(e.Freepoint.GetComponent<FreepointBehaviour>());
        }
        else
        {
            _Freepoint.Remove(e.Freepoint.GetComponent<FreepointBehaviour>());
        }
    }

    private void Resume(ResumeHandlerEvent e)
    {
        IsStart = true;
        CheckLine = false;
    }
}
