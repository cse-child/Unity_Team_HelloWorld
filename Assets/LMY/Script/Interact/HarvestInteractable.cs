using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HarvestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;

    private Animator animator;
    private bool isHarvesting;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleHarvesting()
    {
        Destroy(gameObject);
    }

    public void Interact(Transform interactorTransform)
    {
        ToggleHarvesting();
        ItemLootManager.instance.OpenLootingUI();
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
