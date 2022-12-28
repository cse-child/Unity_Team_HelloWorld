using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;

    private Animator animator;
    private NPCHeadLookAt npcHeadLookAt;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    public void Interact(Transform interactTransform)
    {
        //여기에 대화상자등을 넣어주면 됨
        Debug.Log("Interact!");
        //ChatBubble3D.Create(transform.transform, new Vector3(-0.3f, 1.7f, 0.0f), chatBubble3D.IconType.Happy, "Hello there!");

        //상호작용시 NPC애니메이션을 재생하기 위한 트리거를 넣어줌
        //예시로 Talk로 만들어둠
        animator.SetTrigger("Talk");

        float playerHeight = 1.3f;
        npcHeadLookAt.LookAtPosition(interactTransform.position + Vector3.up * playerHeight);
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
