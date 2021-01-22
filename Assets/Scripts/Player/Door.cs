using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float distFromDoor = 1;

    private bool open = false;
    private Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, distFromDoor))
        {
            if(hit.transform.name == "Door")
            {
                animator = hit.transform.GetComponent<Animator>();
                open = true;
                animator.SetBool("Open", open);
            }
        }
    }
}
