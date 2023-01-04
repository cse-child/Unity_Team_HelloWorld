using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSentence : MonoBehaviour
{
    public string[] sentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;

    public void TalkNpc()
    {
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Chat!");

            TalkNpc();
        } 
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        TalkNpc();
    //    }
    //}
}
