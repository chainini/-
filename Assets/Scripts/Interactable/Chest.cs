using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public SpriteRenderer spriteRenderer;
    public Sprite Opensprite;
    public Sprite Closesprite;
    public bool isDonw;

    private void OnEnable()
    {
        spriteRenderer.sprite = isDonw ? Opensprite : Closesprite;
    }
    public void TriggerAction()
    {
        if(!isDonw)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        spriteRenderer.sprite = Opensprite;
        isDonw = true;
        this.gameObject.tag = "Untagged";
    }
}
