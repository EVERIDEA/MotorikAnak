using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EnumName", menuName = "CreateEnum/Enum")]
public class ScriptableObjectEnum : ScriptableObject {
    public EMainMenuButton TagName;

    public void Raise () {
        EventManager.TriggerEvent(TagName);
    }
} 