using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_m_25 : MonoBehaviour {
    public GameObject sm_35;
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    GameObject stage1;
    GameObject stage2;
    GameObject stage3;
    int bpart=1;
	// Use this for initialization
	void Start () {
        stage1 = Instantiate(part1,new Vector3(0,0,0),Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        int part = sm_35.GetComponent<sm_35>().now_part;
        if (bpart == 1 && part == 2)
        {
            bpart = 2;
            Destroy(stage1);
            stage2 = Instantiate(part2, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        if (bpart == 2 && part == 3)
        {
            bpart = 3;
            Destroy(stage2);
            stage3 = Instantiate(part3, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        if (bpart == 3 && part == 2)
        {
            bpart = 2;
            Destroy(stage3);
            stage2 = Instantiate(part2, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        if (bpart == 2 && part == 1)
        {
            bpart = 1;
            Destroy(stage2);
            stage1 = Instantiate(part1, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }


    }
}
