using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;

    private Animator animator;
    private bool isOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        animator.SetBool("IsOpen", isOpen);
    }

    public void Interact(Transform interactorTransform)
    {
        ToggleDoor();
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
