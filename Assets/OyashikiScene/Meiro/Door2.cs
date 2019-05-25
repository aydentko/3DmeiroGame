using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{

    private Animator animator;
    private Canvas interactionUI2;
    bool OpenTrigger = false;
    bool PlayerInCollider = false;

    // Use this for initialization
    void Start()
    {
        //animator = GetComponent<Animator> ();
        animator = transform.Find("Door_01").GetComponent<Animator>();
        interactionUI2 = GameObject.Find("InteractionUI2").GetComponent<Canvas>();
        if (interactionUI2 != null)
        {
            interactionUI2.rootCanvas.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (interactionUI2 != null)
            {
                interactionUI2.rootCanvas.enabled = true;
            }
            PlayerInCollider = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (interactionUI2 != null)
            {
                interactionUI2.rootCanvas.enabled = false;
            }
            PlayerInCollider = false;
        }
    }

    void OpenDoor()
    {
        if (animator != null)
        {
            animator.SetBool("IsOpen", !animator.GetBool("IsOpen"));
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (OpenTrigger)
        {
            OpenDoor();
            OpenTrigger = false;
        }
        if (interactionUI2.isActiveAndEnabled)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (PlayerInCollider)
                {
                    OpenTrigger = true;
                }
            }
        }
    }
}