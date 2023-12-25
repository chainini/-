using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private Animation flash;
    private PlayerController playerController;


    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        flash=transform.GetChild(0).GetComponent<Animation>();
        rb=GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", physicsCheck.isGround);
        anim.SetBool("isDeath", playerController.isDie);
        anim.SetBool("isAttack", playerController.isAttack);
    }

    public void PlayerHurt()
    {
        flash.Play();
        anim.SetTrigger("hurt");
    }

    public void PlayerAttack()
    {
        anim.SetTrigger("Attack");
    }
}
