using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestInteractable : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool isHarvesting;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleHarvesting()
    {
        isHarvesting = !isHarvesting;
        animator.SetBool("IsHarvesting", isHarvesting);
    }

    public void Interact(Transform interactorTransform)
    {
        ToggleHarvesting();
    }

    public string GetInteractText()
    {
        return "Start Harvesting";
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
