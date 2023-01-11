using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum FadeState { FadeIn = 0, FadeOut, FadeInOut, FadeLoop }

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime; // 10�̸� 1�� (���� Ŭ���� ����)
    private Image image;
    private FadeState fadeState;

    private void Awake()
    {
        image = GetComponent<Image>();

        // Fade In. ����� ���İ��� 1���� 0����
        //StartCoroutine(Fade(1, 0));

        // Fade Out. ����� ���İ��� 0���� 1��
        //StartCoroutine(Fade(0, 1));

        //OnFade(FadeState.FadeLoop);
    }

    public void OnFade(FadeState state)
    {
        fadeState = state;

        switch(fadeState)
        {
            case FadeState.FadeIn:
                StartCoroutine(Fade(1, 0));
                break;
            case FadeState.FadeOut:
                StartCoroutine(Fade(0, 1));
                break;
            case FadeState.FadeInOut:
            case FadeState.FadeLoop:
                StartCoroutine(FadeInOut());
                break;
        }
    }

    private IEnumerator FadeInOut()
    {
        while(true)
        {
            yield return StartCoroutine(Fade(1, 0));
            
            yield return StartCoroutine(Fade(0, 1));

            if (fadeState == FadeState.FadeInOut)
                break;
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while(percent < 1)
        {
            // fadeTime���� ����� fadeTime �ð� ����
            // percent ���� 0���� 1�� �����ϵ��� ��
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // ���İ��� start���� end���� fadeTime �ð� ���� ��ȭ��Ų��
            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }
    }

    public void StopFade()
    {
        StopAllCoroutines();
        Color color = image.color;
        color.a = 0.0f;
        image.color = color;
    }
}
