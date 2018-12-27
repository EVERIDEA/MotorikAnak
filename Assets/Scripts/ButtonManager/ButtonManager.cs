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

public abstract class ButtonManager : MonoBehaviour
{

}
