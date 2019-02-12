using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCol;

    List <Vector2> points;

    public void UpdateLine(Vector2 mousePos)
    {
        if (points==null)
        {
            points = new List<Vector2>();
            SetPoint(mousePos);
            return;
        }
        
        if (Vector2.Distance(points.Last(),mousePos)>.1f)
        {
            SetPoint(mousePos);
        }
    }

    void SetPoint(Vector2 point)
    {
        points.Add(point);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);

        if (points.Count>1)
        {
            edgeCol.points = points.ToArray();
			edgeCol.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
		if (c.gameObject.GetComponent<ObjectTarget>().Type == EObjectTarget.END_POINT) 
		{
			Debug.Log("HIT LINE");
			if (Global.MidpointCount == false) 
			{
				EventManager.TriggerEvent (new ResultGameplayEvent (true));
			}
			if (Global.MidpointCount == true) 
			{
				Debug.Log ("MIDPOINT SKIPPED");
				EventManager.TriggerEvent (new FailHandlerEvent (EFailType.MidpointSkipped));
			}
		}

		if (c.gameObject.GetComponent<ObjectTarget> ().Type == EObjectTarget.OBSTACLE) 
		{
			Debug.Log ("HIT OBSTACLE");
			EventManager.TriggerEvent (new FailHandlerEvent (EFailType.CrossLine));
		}

		if (c.gameObject.GetComponent<ObjectTarget>().Type == EObjectTarget.BLOCKER) 
		{
			Debug.Log ("HIT BLOCKER");
			EventManager.TriggerEvent (new FailHandlerEvent (EFailType.HitBlocker));
		}

		if (c.gameObject.GetComponent<ObjectTarget>().Type == EObjectTarget.MID_POINT) 
		{
			Debug.Log ("HIT MIDPOINT");
			EventManager.TriggerEvent (new MidPointHandlerEvent (false,c.gameObject));
		}

        if (c.gameObject.GetComponent<ObjectTarget>().Type == EObjectTarget.FREE_POINT)
        {
            Debug.Log("HIT FREEPOINT");
            EventManager.TriggerEvent(new FreePointHandlerEvent(false, c.gameObject));
        }

		if (c.gameObject.GetComponent<ObjectTarget>().Type == EObjectTarget.CROSS_POINT)
		{
			Debug.Log("HIT CROSSPOINT");
			Global.CrossLine += 1;
			if ((Global.CrossLine>1)&&(c.gameObject.name=="Shape4")) 
			{
				Debug.Log ("This IS IT");
				EventManager.TriggerEvent (new ResultGameplayEvent (true));
			}
			if ((Global.CrossLine>1)&&(c.gameObject.name!="Shape4")) 
			{
				Debug.Log ("Wrong");
				EventManager.TriggerEvent (new FailHandlerEvent (EFailType.WrongAnswer));
			}
			//EventManager.TriggerEvent(new FreePointHandlerEvent(false, c.gameObject));
		}

    }
}
