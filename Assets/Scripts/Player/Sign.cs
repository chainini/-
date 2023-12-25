using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public Transform player;
    public GameObject E;
    public GameObject Mask;
    public float time;
    public float finishTime;
    public bool canPress;
    //public bool isDonw;
    public bool isPress;

    private IInteractable targetItem;
    private AudioDefination audioDefination;

    private void Awake()
    {
        time = 0.1f;
    }

    private void OnEnable()
    {
        EventHandle.PlayerE += OnPlayerPressE;
    }
    private void OnDisable()
    {
        EventHandle.PlayerE -= OnPlayerPressE;
        canPress = false;
    }

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "interactable" )
        {
            canPress = true;
            targetItem = collision.GetComponent<IInteractable>();
            audioDefination = collision.GetComponent<AudioDefination>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "interactable")
            canPress = false;
    }

    private void Update()
    {
        E.SetActive(canPress);
        Mask.SetActive(canPress);

        if(isPress)
        {
            time += Time.deltaTime;
            Mask.GetComponent<Image>().fillAmount = time / finishTime;
            if (time >= finishTime)
            {
                targetItem.TriggerAction();
                canPress = false;
                //isDonw = true;
                isPress = false;
                time = 0.1f;

                audioDefination.PlayAudioClip();
            }
        }

        if(!canPress)
        {
            time = 0.1f;
            Mask.GetComponent<Image>().fillAmount = 0;

        }
        

        transform.localScale = player.localScale/2;
    }

    private void OnPlayerPressE()
    {
        if(canPress)
        {
            Counter();
        }
    }

    //¼ÆÊ±
    private void Counter()
    {
        isPress = true;
    }
}
