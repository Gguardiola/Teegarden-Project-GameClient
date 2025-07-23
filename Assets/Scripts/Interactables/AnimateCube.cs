using System;
using UnityEngine;

public class AnimateCube : Interactable
{
    Animator animator;

    private string startPrompt;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        startPrompt = promptMessage;
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            promptMessage = startPrompt;
        }
        else
        {
            promptMessage = "Stop animating";
        }
    }

    protected override void Interact()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Spinning"))
        {
         animator.Play("Default");   
        }
        else
        {
            animator.Play("Spinning");
        }
    }
}
