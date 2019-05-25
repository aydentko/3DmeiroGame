using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sm_35 : MonoBehaviour {
    public int now_part = 1;

    public void change_part(int a)
    {
        now_part = a;
    }
    private void OnTriggerEnter(Collider human)
    {
        if (human.gameObject.tag == "Player")
        {
            st_humanpos.cont();
            SceneManager.LoadScene("scene_2");
        }
    }
}
