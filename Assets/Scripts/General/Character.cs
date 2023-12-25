using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("��������")]
    public float maxHealth;
    public float curentHealth;
    [Header("�����޵�")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

    public UnityEvent<Character> OnHealthChange;  

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;

    private AudioDefination audioDefination;

    private void Awake()
    {
        audioDefination = GetComponent<AudioDefination>();

        curentHealth = maxHealth;
    }

    private void OnEnable()
    {
        EventHandle.StartNewGame += NewGame;
    }
    private void OnDisable()
    {
        EventHandle.StartNewGame -= NewGame;
    }

    private void NewGame()
    {
        curentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)
        {
            return;
        }
        if(curentHealth-attacker.damege>0)
        {
            curentHealth -= attacker.damege;
            TriggerInvulnerable();
            //ִ������
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            curentHealth = 0;
            //��������
            OnDie?.Invoke();
        }

        audioDefination.PlayAudioClip();
        OnHealthChange?.Invoke(this);
    }

    private void TriggerInvulnerable()
    {
        if(!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
