using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SmallBeeBullet : MonoBehaviour
{
    public float force;
    public float time;
    public float destroyTime;

    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        force = 10;
        time = 5;

        destroyTime = time;
    }

    private void Start()
    {
        Move();
    }

    private void Update()
    {
        
        destroyTime-=Time.deltaTime;
        if(destroyTime<=0)
        {
            Destroy(gameObject);
        }

    }

    public void Move()
    {
        transform.rotation = (Quaternion.Euler(0, 0, Vector3.Angle(transform.position,EventHandle.CallPlayerPos())));
        //transform.Translate( *speed*Time.deltaTime);
        //transform.LookAt((EventHandle.CallPlayerPos() - transform.position).normalized);
        rb.AddForce((EventHandle.CallPlayerPos() - transform.position).normalized*force,ForceMode2D.Impulse);
    }
}
