using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGameplayEvent : GameEvent {

}
public class ResultGameplayEvent : GameEvent
{
    public bool IsWin;

    public ResultGameplayEvent(bool isWin)
    {
        IsWin = isWin;
    }
}
public class EndGameplayEvent : GameEvent
{

}

public class FailHandlerEvent : GameEvent 
{
    public EFailType Type;

    public FailHandlerEvent(EFailType type)
    {
        Type = type;
    }
}

public class DisableAllPopupEvents:GameEvent
{
	
}

public class StartLevelEvent:GameEvent
{
	
}

public class NextLevelEvent : GameEvent
{
	
}

public class RestartLevelEvent : GameEvent
{

}

public class TimerHandlerEvent :GameEvent
{
	public bool IsOn;
	public float TimeLimit;

	public TimerHandlerEvent(bool isOn, float timeLimit )
	{
		IsOn = isOn;
		TimeLimit = timeLimit;
	}
}

public class ScoreHandlerEvent : GameEvent
{
	public int Value;
	public ScoreHandlerEvent(int value)
	{
		Value = value;
	}
}

public class MidPointHandlerEvent : GameEvent
{
	public bool IsCrossed;
	public GameObject Midpoint;
	public MidPointHandlerEvent(bool isCrossed, GameObject midpoint)
	{
		IsCrossed = isCrossed;
		Midpoint = midpoint;
	}
}