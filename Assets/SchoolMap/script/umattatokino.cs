using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class umattatokino : MonoBehaviour {

    public GameObject player;

    private void Awake()
    {
        player = GameObject.Find("unitychan");
    }

    // Use this for initialization
    void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Alpha0))
        {
            player.transform.position = new Vector3(9, 3, 28);
        }

    }
}
