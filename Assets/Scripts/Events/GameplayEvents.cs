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

public class FailHandlerEvent : GameEvent {
    public EFailType Type;

    public FailHandlerEvent(EFailType type)
    {
        Type = type;
    }
}