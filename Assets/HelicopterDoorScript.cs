using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[PublicAPI]
public class HelicopterDoorScript : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        /*if(IsOpen != isOpen)
        {
            if (IsOpen)
                OpenDoor();
            else
                CloseDoor();
        }*/
    }

    // public bool IsOpen = false;
    private bool isOpen;
    public void OpenDoor()
    {
        animator.SetBool("IsOpen", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("IsOpen", false);
    }
}
