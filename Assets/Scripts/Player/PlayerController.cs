using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    private PlayerAnimation playerAnimation;
    private Collider2D coll;
    public SpriteRenderer spriteRenderer;
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public float rollFirce;
    public float hurtForce;
    [Header("状态")]
    public bool isHurt;

    public bool isDie;

    public bool isAttack;
    [Header("物理材质")]
    public PhysicsMaterial2D Wall;
    public PhysicsMaterial2D Normal;

    private AudioDefination audioDefination;

    private void Awake()
    {
        inputControl = new PlayerInputControl();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<Collider2D>();

        audioDefination = transform.GetChild(0).GetComponent<AudioDefination>();

        //跳跃
        inputControl.GamePlay.Jump.started += Jump;
        inputControl.GamePlay.Roll.started += Roll;
        //攻击
        inputControl.GamePlay.Attack.started += PlayerAttack;
        //E
        inputControl.GamePlay.E.performed += PlayerCollection;
    }
    private void OnEnable()
    {
        inputControl.Enable();
        EventHandle.LoadRequestEvent += OnLoadRequestEvent;
        EventHandle.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        EventHandle.PlayerPos += OnPlayerPos;
    }
    private void OnDisable()
    {
        inputControl.Disable();
        EventHandle.LoadRequestEvent -= OnLoadRequestEvent;
        EventHandle.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        EventHandle.PlayerPos -= OnPlayerPos;
    }

    

    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        CheckState();
    }
    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
        {
            Move();
        }
    }

    private Vector3 OnPlayerPos()
    {
        return this.transform.position;
    }

    private void OnAfterSceneLoadEvent()
    {
        inputControl.GamePlay.Enable();
    }

    private void OnLoadRequestEvent(GameSceneSO sceneToLoad, Vector3 vector, bool arg3)
    {
        
        inputControl.GamePlay.Disable();
        var isLocation = sceneToLoad.sceneType == SceneType.Location;
        spriteRenderer.enabled = isLocation;
        //Debug.Log(isLocation);
    }

    private void Move()
    {
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

            //人物翻转 
            int faceDir = (int)transform.localScale.x;
            if (inputDirection.x > 0)
                faceDir = 2;
            if (inputDirection.x < 0)
                faceDir = -2;
            transform.localScale = new Vector3(faceDir, 2, 2);

            //SignCanvas.transform.position+= new Vector3(inputDirection.x * speed * Time.deltaTime, rb.velocity.y,0);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(physicsCheck.isGround)
        {
            audioDefination.PlayAudioClip();
            rb.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
        }
    }

    private void Roll(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.forward * rollFirce, ForceMode2D.Impulse);
        } 
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;
    }

    private void PlayerCollection(InputAction.CallbackContext context)
    {
        EventHandle.OnPlayerE();
    }

    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? Normal : Wall;
    }

    #region UnityEvent
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.transform.position.x, 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDie()
    {
        isDie = true;
        inputControl.GamePlay.Disable();
    }
    #endregion


    /// <summary>
    /// Massage
    /// </summary>
    public void OnHurtAnimationExit()
    {
        isHurt=false;
    }

    public void OnAttackFinishEnter()
    {
        isAttack = true;
    }
    public void OnAttackFinishExit()
    {
        isAttack=false;
    }

}
