using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_wall_2 : MonoBehaviour {
    bool OnMove = false;
    //public GameObject sm_2;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    int cou = 0;
	void Update () {
        OnMove = GameObject.Find("Triger").GetComponent<swy>().IsMove();
        if (OnMove)
        {
            if(this.transform.position.y < 12.0f)
            {
                transform.Translate(0, 1.0f, 0);
                cou++;
            }
        }
        else
        {
            if (this.transform.position.y > 4f)
            {
                transform.Translate(0, -1.0f, 0);
                cou--;
            }
        }
        //float x = sm_2.GetComponent<human_creater>().x;
        //float z = sm_2.GetComponent<human_creater>().z;
        //if (((4<z&&z<12)&&(-36<x&&x<-28))&& cou < 7)
        //{
        //  transform.Translate(0, 1.0f, 0);
        // cou++;
        //}
        //else if (((-4 >= z || z >= 12) || (-36 >= x || x >= -28)) && cou > 0)
        //{
        //  transform.Translate(0, -1.0f, 0);
        // cou--;
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        // ぶつかったオブジェクトが収集アイテムだった場合
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("壁が上がる");
            this.OnMove = true;
        }
        else
        {
            this.OnMove = false;
        }
    }

}
