using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[PublicAPI]
public class HelicopterScript : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Land()
    {
        animator.SetBool("IsLanded", true);
    }

    public void TakeOff()
    {
        animator.SetBool("IsLanded", false);
    }
}
