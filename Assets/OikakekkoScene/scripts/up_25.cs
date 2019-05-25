using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class up_25 : MonoBehaviour {
    public GameObject sm_35;
    private void OnTriggerEnter(Collider human)
    {
        if (human.gameObject.tag == "Player")
        {
            if (sm_35.GetComponent<sm_35>().now_part == 1)
            {
                sm_35.GetComponent<sm_35>().change_part(2);
            }
            else if (sm_35.GetComponent<sm_35>().now_part == 2)
            {
                sm_35.GetComponent<sm_35>().change_part(3);
            }
        }
    }
}
