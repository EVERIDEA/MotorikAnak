using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region EXAMPLE-EVENT-CLASS
public class SomeClass : GameEvent 
{

    public string SomeString;
    public int SomeInt;

    public SomeClass(string someString, int someInt)
    {
        SomeString = someString;
        SomeInt = someInt;
    }
}

public class LinkedIcon : GameEvent
{
	public int Id;
	public bool Linked;

	public LinkedIcon(int id, bool linked)
	{
		Id = id;
		Linked = linked;
	}
} 
#endregion

#region CORE-EVENT-INIT

#endregion
