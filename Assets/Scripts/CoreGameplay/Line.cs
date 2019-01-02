﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCol;
	public bool IsTriggerOnce=true;

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
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
		if (IsTriggerOnce==false) 
		{
			if (c.gameObject.GetComponent<ObjectTarget>().Type == EObjectTarget.END_POINT) 
			{
				Debug.Log("HIT LINE");
				EventManager.TriggerEvent(new ResultGameplayEvent(true));
				IsTriggerOnce = true;
			}

			if (c.gameObject.GetComponent<ObjectTarget> ().Type == EObjectTarget.OBSTACLE) 
			{
				Debug.Log ("HIT OBSTACLE");
				IsTriggerOnce = true;
			}
		}	
      
    }
}
