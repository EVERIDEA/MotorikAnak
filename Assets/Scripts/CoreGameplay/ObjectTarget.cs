using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTarget : MonoBehaviour {
    public EObjectTarget Type;
	// Use this for initialization
	public void OnEnable()
	{
		if (this.GetComponent<ObjectTarget>().Type==EObjectTarget.MID_POINT) 
		{
			this.gameObject.AddComponent<MidpointBehaviour>();
			EventManager.TriggerEvent (new MidPointHandlerEvent (true,this.gameObject));
		}

        if (this.GetComponent<ObjectTarget>().Type==EObjectTarget.FREE_POINT)
        {
            this.gameObject.AddComponent<FreepointBehaviour>();
            EventManager.TriggerEvent(new FreePointHandlerEvent(true,this.gameObject));
        }
	}
}
