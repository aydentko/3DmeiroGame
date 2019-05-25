using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstfstart : MonoBehaviour {

    int firstfspawn;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start () {

        firstfspawn = warppoint3.getfirstfspawn();
        GameObject[] StartPoint = new GameObject[4];
        // スタート地点を示すオブジェクトを取得
        for(int i = 1; i < 4; i++)
        {
            StartPoint[i] = GameObject.Find("StartPoint" + i);

        }
        if(firstfspawn <= 0)
        {
            firstfspawn = 1;
        }
        player.transform.position = StartPoint[firstfspawn].transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
