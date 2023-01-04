using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;

    private Animator animator;
    private NPCHeadLookAt npcHeadLookAt;

    public string[] sentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;

    public void TalkNpc()
    {
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    public void Interact(Transform interactTransform)
    {
        //���⿡ ��ȭ���ڵ��� �־��ָ� ��
        Debug.Log("Interact!");
        //ChatBubble.Create(transform.transform, new Vector3(-0.3f, 1.7f, 0.0f), ChatBubble.IconType.Happy, "Hello there!");
        TalkNpc();

        //��ȣ�ۿ�� NPC�ִϸ��̼��� ����ϱ� ���� Ʈ���Ÿ� �־���
        //���÷� Talk�� ������
        animator.SetTrigger("Talk");

        float playerHeight = 1.0f;
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
