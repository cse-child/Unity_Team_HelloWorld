using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum FadeState { FadeIn = 0, FadeOut, FadeInOut, FadeLoop }

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime; // 10�̸� 1�� (���� Ŭ���� ����)
    private FadeState fadeState;

    //public GameObject blood;
    //public GameObject black;
    public Image bloodScreen;
    public Image blackImage;

    private Coroutine bloodCoroutine;
    private Coroutine blackCoroutine;

    //public Image image;

    private void Awake()
    {
        //image = GetComponent<Image>();
        // Fade In. ����� ���İ��� 1���� 0����
        //StartCoroutine(Fade(1, 0));

        // Fade Out. ����� ���İ��� 0���� 1��
        //StartCoroutine(Fade(0, 1));

        //OnFade(FadeState.FadeLoop);
    }

    public void OnFade(string key, FadeState state)
    {
        fadeState = state;

        if (key == "blood")
        {
            switch (fadeState)
            {
                case FadeState.FadeIn:
                    bloodCoroutine = StartCoroutine(BloodFade(1, 0));
                    break;
                case FadeState.FadeOut:
                    bloodCoroutine = StartCoroutine(BloodFade(0, 1));
                    break;
                case FadeState.FadeInOut:
                case FadeState.FadeLoop:
                    StartCoroutine(FadeInOut(key));
                    break;
            }
        }
        else if (key == "black")
        {
            switch (fadeState)
            {
                case FadeState.FadeIn:
                    blackCoroutine = StartCoroutine(BlackFade(1, 0));
                    break;
                case FadeState.FadeOut:
                    blackCoroutine = StartCoroutine(BlackFade(0, 1));
                    break;
                case FadeState.FadeInOut:
                case FadeState.FadeLoop:
                    StartCoroutine(FadeInOut(key));
                    break;
            }
        }
    }

    private IEnumerator FadeInOut(string key)
    {
        if (key == "blood")
        {
            while (true)
            {
                yield return bloodCoroutine = StartCoroutine(BloodFade(1, 0));

                yield return bloodCoroutine = StartCoroutine(BloodFade(0, 1));

                if (fadeState == FadeState.FadeInOut)
                    break;
            }
        }
        else if (key == "black")
        {
            while (true)
            {
                yield return blackCoroutine = StartCoroutine(BlackFade(1, 0));

                yield return blackCoroutine = StartCoroutine(BlackFade(0, 1));

                if (fadeState == FadeState.FadeInOut)
                    break;
            }
        }
    }

    private IEnumerator BloodFade(float start, float end)
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
            Color color = bloodScreen.color;
            color.a = Mathf.Lerp(start, end, percent);
            bloodScreen.color = color;

            yield return null;
        }
    }

    private IEnumerator BlackFade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            // fadeTime���� ����� fadeTime �ð� ����
            // percent ���� 0���� 1�� �����ϵ��� ��
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // ���İ��� start���� end���� fadeTime �ð� ���� ��ȭ��Ų��
            Color color = blackImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            blackImage.color = color;

            yield return null;
        }
    }

    public void StopFade(string key)
    {
        if (key == "blood")
        {
            StopCoroutine(bloodCoroutine);
            Color color = bloodScreen.color;
            color.a = 0.0f;
            bloodScreen.color = color;
        }
        else if (key == "black")
        {
            StopCoroutine(blackCoroutine);
            Color color = blackImage.color;
            color.a = 0.0f;
            blackImage.color = color;
        }
    }
}
