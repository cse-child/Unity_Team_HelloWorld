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
    private float fadeTime; // 10이면 1초 (값이 클수록 빠름)
    private Image image;
    private FadeState fadeState;

    private void Awake()
    {
        image = GetComponent<Image>();

        // Fade In. 배경의 알파값이 1에서 0으로
        //StartCoroutine(Fade(1, 0));

        // Fade Out. 배경의 알파값이 0에서 1로
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
            // fadeTime으로 나누어서 fadeTime 시간 동안
            // percent 값이 0에서 1로 증가하도록 함
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // 알파값을 start부터 end까지 fadeTime 시간 동안 변화시킨다
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
