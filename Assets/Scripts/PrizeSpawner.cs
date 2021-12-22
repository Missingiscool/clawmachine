using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PrizeSpawner : MonoBehaviour
{
    public GameObject prize;
    public int prize_number = 10;
    public int xbounds = 50;
    public int zbounds = 50;
    public int ybounds = 60;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < prize_number; i++)
        {
            Debug.Log("spawning prize" + i);
            Vector3 v = RandomVector(xbounds, ybounds, zbounds);
            GameObject.Instantiate(prize, v , Random.rotation);
        }
    }

    private Vector3 RandomVector(int x, int y, int z)
    {
        int rx = Random.Range(-x, x+1);
        int ry = Random.Range(y, y + 30);
        int rz = Random.Range(-z, z+1);
        return new Vector3(rx, ry, rz);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
