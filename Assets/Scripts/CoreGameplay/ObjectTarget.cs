using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTarget : MonoBehaviour {
    public EObjectTarget Type;

	public void OnEnable()
	{
		if (this.GetComponent<ObjectTarget>().Type==EObjectTarget.MID_POINT) 
		{
            if (this.gameObject.GetComponent<MidpointBehaviour>()==null)
            {
                this.gameObject.AddComponent<MidpointBehaviour>();
            }

            EventManager.TriggerEvent(new MidPointHandlerEvent(true, this.gameObject));
        }

        if (this.GetComponent<ObjectTarget>().Type==EObjectTarget.FREE_POINT)
        {
            if (this.gameObject.GetComponent<FreepointBehaviour>()==null)
            {
                this.gameObject.AddComponent<FreepointBehaviour>();
            }

            EventManager.TriggerEvent(new FreePointHandlerEvent(true, this.gameObject));
        }

		if (this.GetComponent<ObjectTarget>().Type==EObjectTarget.CROSS_POINT)
		{
			if (this.gameObject.GetComponent<CrosspointBehaviour>()==null)
			{
				this.gameObject.AddComponent<CrosspointBehaviour>();
			}

			//EventManager.TriggerEvent(new FreePointHandlerEvent(true, this.gameObject));
		}
	}

    /*
    public void OnDisable()
    {
        if (this.GetComponent<ObjectTarget>().Type == EObjectTarget.MID_POINT)
        {
            Destroy(this.gameObject.GetComponent<MidpointBehaviour>());
        }

        if (this.GetComponent<ObjectTarget>().Type == EObjectTarget.FREE_POINT)
        {
            Destroy(this.gameObject.GetComponent<FreepointBehaviour>());
        }
    }
    */
}
