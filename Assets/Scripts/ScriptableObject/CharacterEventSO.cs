using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/CharcterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<Character> OnEventRaised;
    public void RaisedEvent(Character character)
    {
        OnEventRaised?.Invoke(character); 
    }
}
