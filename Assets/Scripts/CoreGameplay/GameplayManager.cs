using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject linePrefab;
    Line activeLine;

    bool IsStart = false;

    List<GameObject> _LineDrawer = new List<GameObject>();

    private void Awake()
    {
        EventManager.AddListener<InitGameplayEvent>(InitListener);
        EventManager.AddListener<EndGameplayEvent>(EndGameListener);
        EventManager.AddListener<FailHandlerEvent>(FailListener);
        EventManager.AddListener<ResultGameplayEvent>(ResultHandler);
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
                if (hit.transform.gameObject.name == "A")
                {
                    Debug.Log("A");
                }
                else
                {
                    return; //Fail
                }
                if (_LineDrawer.Count >= 1)
                    return;


                GameObject lineGo = Instantiate(linePrefab);
                _LineDrawer.Add(lineGo);

                activeLine = lineGo.GetComponent<Line>();
                lineGo.transform.parent = this.gameObject.transform;
            }

            if (Input.GetMouseButtonUp(0))
            {
                activeLine = null;
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
        ResultHandler (new ResultGameplayEvent(true));
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
}
