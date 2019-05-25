using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_wall_circle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    public GameObject Human;
    float cou = 0;
	void Update () {
        Vector3 pos_hu = Human.transform.position;
        float x = pos_hu.x;
        float z = 36-pos_hu.z;
        if (x * x + z * z < 100.0f && cou < 14)
        {
            transform.Translate(0, 0.5f, 0);
            cou++;
        }
        else if (x * x + z * z>=100.0f  && cou > 0) {
            transform.Translate(0, -0.5f, 0);
            cou--;
        }

	}
}
