using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swy : MonoBehaviour {
    public bool OnMove = false;
    public GameObject Wall_des;
    public GameObject Wall;
    bool isOnTriger = false;
    private void OnTriggerStay(Collider human)
    {
        if (human.gameObject.tag == "Player")
        {
            Destroy(Wall_des);
            if (!isOnTriger)
            {
                Instantiate(Wall, new Vector3(0, 4.0f, 36.0f), Quaternion.Euler(0, 90.0f, 0));
                isOnTriger = true;
            }
            OnMove = true;
        }
        else
        {
            OnMove = false;
        }

    }

    public bool IsMove()
    {
        return OnMove;
    }
}
