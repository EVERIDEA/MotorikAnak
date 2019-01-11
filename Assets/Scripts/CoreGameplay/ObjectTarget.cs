using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTarget : MonoBehaviour {
    public EObjectTarget Type;
	// Use this for initialization
	private void OnEnable()
	{
		if (this.GetComponent<ObjectTarget>().Type==EObjectTarget.MID_POINT) 
		{
			this.gameObject.AddComponent<MidpointBehaviour>();
			EventManager.TriggerEvent (new MidPointHandlerEvent (true,this.gameObject));
		}
	}
}
