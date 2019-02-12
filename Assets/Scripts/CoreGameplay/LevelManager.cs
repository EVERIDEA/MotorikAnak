using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{	
	void Update ()
    {
        if (Global.Level<=22)
        {
            EventManager.TriggerEvent(new GameplayTypeHandlerEvent(GameplayType.LINE_DRAW_MECHANIC));
        }
        if (Global.Level==23)
        {
            EventManager.TriggerEvent(new GameplayTypeHandlerEvent(GameplayType.FIND_SHAPE_MECHANIC));
        }
        if (Global.Level==24)
        {
            EventManager.TriggerEvent(new GameplayTypeHandlerEvent(GameplayType.FIND_SHAPE_MECHANIC2));
        }
		if ((Global.Level==25)||(Global.Level==26))
		{
			EventManager.TriggerEvent(new GameplayTypeHandlerEvent(GameplayType.FIND_THE_DIFFERENCE_MECHANIC));
		}
		if (Global.Level==27)
		{
			EventManager.TriggerEvent(new GameplayTypeHandlerEvent(GameplayType.LINE_DRAW_MECHANIC_IGNORE_START_END_POINT));
		}
	}
}
