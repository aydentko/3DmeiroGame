using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sm_1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    //public GameObject Human;
    //int a=0;
	void Update () {
      //  Vector3 pos = Human.transform.position;
       // if (pos.z >= 40.0f) {
        //    SceneManager.LoadScene("scene_15");
        //}
	}

    void OnTriggerEnter(Collider other)
    {
        // プレイヤーが入った場合
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("scene_15");
        }
    }

}
