using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class human_creater : MonoBehaviour {

    public GameObject human;
    GameObject Human;
    public float x, z;
	// Use this for initialization
	void Start () {
        if (st_humanpos.getclear_25() == false)
        {
            Human=Instantiate(human, new Vector3(0,4,-48.0f), Quaternion.identity) as GameObject;
        }
        else
        {
            Human=Instantiate(human, new Vector3(-32.0f,0,32.0f), Quaternion.Euler(0, 180.0f, 0)) as GameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos_hu = Human.transform.position;
        x = pos_hu.x;
        z = pos_hu.z;
	}
}
