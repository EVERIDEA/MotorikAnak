using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ButtonAction
{
    public string NameId;
    public GameObject ButtonObject;
    public bool IsObjectActive = false;
    public EMainMenuButton Type;
}
[System.Serializable]
public class DirectButtonAction
{
    public string Id;
    public GameObject ButtonObject;
    public bool IsObjectActive = false;
    public string TargetObjectId;
}
public abstract class ButtonManager : MonoBehaviour
{

}
