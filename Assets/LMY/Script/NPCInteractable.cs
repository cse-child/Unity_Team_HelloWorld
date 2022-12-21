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
        //���⿡ ��ȭ���ڵ��� �־��ָ� ��
        Debug.Log("Interact!");

        //��ȣ�ۿ�� �ִϸ��̼��� ����ϱ� ���� Ʈ���Ÿ� �־���
        //���÷� Talk�� ������
        animator.SetTrigger("Talk");

        float playerHeight = 1.7f;
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
