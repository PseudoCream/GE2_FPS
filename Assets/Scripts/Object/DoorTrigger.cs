using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    Animator animator;
    public Transform door;

    private void Start()
    {
        animator = door.transform.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
            animator.SetBool("Open", false);
    }
}
