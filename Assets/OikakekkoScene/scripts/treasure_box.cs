using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class treasure_box : MonoBehaviour {

    public bool clear = false;
    public GameObject box_top;
    public float open_speed;
    int cou = 0;

    // Use this for initialization
    void Start () {
		
	}
    private void OnTriggerEnter(Collider human)
    {
        if(human.gameObject.tag == "Player")
        {
            clear = true;
        }
    }

    // Update is called once per frame
    void Update () {
        if (clear == true&&cou<180) {
            box_top.transform.Rotate(-open_speed, 0, 0);
            cou++;
        }
        if (cou == 180) SceneManager.LoadScene("clear_scene");
    }

   
    
}
