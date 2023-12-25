using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Teleport : MonoBehaviour, IInteractable
{
    public GameSceneSO sceneToGo;
    public Vector3 positionToGo;
    public void TriggerAction()
    {
        EventHandle.CallLoadRequestEvent(sceneToGo, positionToGo, true);
    }
}
