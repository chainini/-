using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBeeSpawer : MonoBehaviour
{
    public int BeeCounter;
    public int CurrentBeeCounter;
    public float spawnerTime;
    public float spawnTimeCD;
    public GameObject prefab;
    public Transform randomPos1;
    public Transform randomPos2;
    public Transform randomPos3;

    private void OnEnable()
    {
        EventHandle.SmallBeeRandomPos += OnSmallBeeRandomPos;
    }
    private void OnDisable()
    {
        EventHandle.SmallBeeRandomPos -= OnSmallBeeRandomPos;
    }

    private Vector3 OnSmallBeeRandomPos()
    {
        return new Vector3(Random.Range(randomPos1.position.x, randomPos2.position.x), Random.Range(randomPos1.position.y, randomPos3.position.y), 0);
    }

    private void Start()
    {
        spawnTimeCD = spawnerTime;
    }

    private void Update()
    {
        spawnerTime-=Time.deltaTime;
        if(spawnerTime <= 0 )
        {
            if(CurrentBeeCounter<=2 && BeeCounter>=0)
            {
                //Éú³É
                //CreateBee();
            }


            CurrentBeeCounter++;
            BeeCounter--;
            spawnerTime = spawnTimeCD;
        }
    }

    public void CreateBee()
    {
        Instantiate(prefab,CreateRandomPos(),Quaternion.identity);
    }

    public Vector3 CreateRandomPos()
    {
        return new Vector3(Random.Range(randomPos1.position.x, randomPos2.position.x), Random.Range(randomPos3.position.y, randomPos1.position.y), 0);
    }
}
