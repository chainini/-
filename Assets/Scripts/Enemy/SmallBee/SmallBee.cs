using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBee : Enemy
{
    //private GameObject player;
    public Transform randomPos1;
    public Transform randomPos2;
    public Transform randomPos3;
    public GameObject prefab;
    protected override void Awake()
    {
        anim = GetComponent<Animator>();

        currentSpeed = normalSpeed;
        //waitTimeCounter = waitTime;
        lostTimeCounter = lostTime;
        patrolState = new SmallBeePatrolState();
        chaseState = new SmallBeeChaseState();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public void OnCreateBullet()
    {
        Instantiate(prefab, transform.GetChild(0).position, Quaternion.identity);
    }

    public override bool FoundPlayer()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll((Vector2)transform.position, 2f);
        foreach (var collider2D in collider2Ds)
        {
            if (collider2D.CompareTag("Player"))
            {
                //player = collider2D.gameObject;
                return true;
            }
        }
        return false;
    }

    protected override void FixedUpdate()
    {
        if(!isDie)
        {
            currentState.PhysicsUpdate();
        }
        if(isDie)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    

    //public override void Move()
    //{
    //    if (FoundPlayer())
    //    {
    //        transform.localScale = new Vector3(faceDir.x, 1, 1);
    //        transform.Translate(player.transform.position);
    //    }
    //    else
    //    {
    //        transform.localScale = new Vector3(faceDir.x, 1, 1);
    //        transform.Translate(MoveToRandomPos());
    //    }
    //}

    //public Vector3 MoveToRandomPos()
    //{
    //    return new Vector3(Random.Range(randomPos1.position.x, randomPos2.position.x), Random.Range(randomPos3.position.y, randomPos1.position.y), 0);
    //}

    //public  void Attack()
    //{

    //}
}
