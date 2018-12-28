using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailPopUpButton : MonoBehaviour {
    [SerializeField]
    DirectButtonAction[] _PopUpButton;

    private void Awake()
    {
        for (int i = 0; i < _PopUpButton.Length; i++) {
            int index = i;
            _PopUpButton[i].ButtonObject.AddComponent<Button>().onClick.AddListener(delegate
            {
                //_PopUpButton[index].ObjectTarget.SetActive(_PopUpButton[index].IsObjectActive);
                EventManager.TriggerEvent(new FailPopUpEvents(_PopUpButton[index].TargetObjectId, _PopUpButton[index].IsObjectActive));
            });
        }
    }
}
