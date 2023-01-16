using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;

    private Animator animator;
   // private NPCHeadLookAt npcHeadLookAt;


    private void Awake()
    {
        animator = GetComponent<Animator>();
     //   npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    public void Interact(Transform interactTransform)
    {
        //��������
        UIControl uiControl = FindObjectOfType<UIControl>();
        uiControl.ShopUI.SetActive(true);
        uiControl.ShopUI.transform.SetAsLastSibling();
        uiControl.CheckCursorState();
        UISoundControl.instance.SoundPlay(1);


        //���⿡ ��ȭ���ڵ��� �־��ָ� ��
      //  Debug.Log("Interact!");
        //ChatBubble.Create(transform.transform, new Vector3(-0.3f, 1.7f, 0.0f), ChatBubble.IconType.Happy, "Hello there!");

        //��ȣ�ۿ�� NPC�ִϸ��̼��� ����ϱ� ���� Ʈ���Ÿ� �־���
        //���÷� Talk�� ������
        animator.SetTrigger("Talk");

      //  float playerHeight = 1.0f;
      //  npcHeadLookAt.LookAtPosition(interactTransform.position + Vector3.up * playerHeight);
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
