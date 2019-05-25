using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class warptoptoi : MonoBehaviour {

    public GameObject player;
    static public int ifspawn = 0;

    // Use this for initialization
    void Start () {
		
     
    }
	
	// Update is called once per frame
	void Update () {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ifspawn = 3;
            SceneManager.LoadScene("iF");
        }

    }

    public static int getifspawn()
    {
        return ifspawn;
    }

}
