using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public GameObject linePrefab;
    Line activeLine;

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject lineGo = Instantiate(linePrefab);
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
        if (activeLine!=null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }
       
	}
}
