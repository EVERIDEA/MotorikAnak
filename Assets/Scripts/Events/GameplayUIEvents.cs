using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailPopUpEvents : GameEvent {
    public string Id;
    public bool IsActive;

    public FailPopUpEvents(string id, bool isActive)
    {
        Id = id;
        IsActive = isActive;
    }
} 
