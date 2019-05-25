using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime_des : MonoBehaviour {

    public GameObject slime_wall;
    public GameObject slime;

    private void OnTriggerEnter(Collider human)
    {
        if (human.gameObject.CompareTag("Player"))
        {
            Destroy(slime);
            Destroy(slime_wall);
        }
    }
}
