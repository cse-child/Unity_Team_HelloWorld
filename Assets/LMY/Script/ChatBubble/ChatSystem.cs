using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatSystem : MonoBehaviour
{
    public Queue<string> sentences;
    public string currentSentenece;
    public TextMeshPro text;
    public GameObject bg;
    public void Ondialogue(string[] lines, Transform chatPoint)
    {
        transform.position = chatPoint.position;
        sentences = new Queue<string>();
        sentences.Clear();
        foreach (var line in lines) 
        {
            sentences.Enqueue(line);
        }

        StartCoroutine(DialogueFlow(chatPoint));
    }

    IEnumerator DialogueFlow(Transform chatPoint)
    {
        yield return null;
        while(sentences.Count > 0)
        {
            currentSentenece= sentences.Dequeue();
            text.text = currentSentenece;
            
            float x = text.preferredWidth;
            x = (x > 3) ? 3 : x + 0.3f;

            bg.transform.localScale = new Vector3(x, text.preferredHeight + 0.3f);

            transform.position = new Vector3(chatPoint.position.x, chatPoint.position.y + text.preferredHeight * 0.5f);
            yield return new WaitForSeconds(3f);
        }
        Destroy(gameObject);
    }
}
