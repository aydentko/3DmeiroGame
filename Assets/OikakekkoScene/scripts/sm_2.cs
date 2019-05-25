using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sm_2 : MonoBehaviour {
    private void OnTriggerEnter(Collider human)
    {
        if (human.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("scene_25");
        }
    }
}
