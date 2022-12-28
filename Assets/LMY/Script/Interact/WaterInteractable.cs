using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteractable : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool isFishing;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleFishing()
    {
        isFishing = !isFishing;
        animator.SetBool("IsFishing", isFishing);
    }

    public void Interact(Transform interactorTransform)
    {
        ToggleFishing();
    }

    public string GetInteractText()
    {
        return "Start Fishing";
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
