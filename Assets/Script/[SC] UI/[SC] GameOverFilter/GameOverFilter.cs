using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverFilter : MonoBehaviour
{
    public static GameOverFilter instance;
    [SerializeField] private Image filter;
    [SerializeField] private float fadeDuration = 5f;
    void Awake()
    {
        filter.enabled = false;
        instance = this;
    }

    public void GameOver()
    {
        filter.enabled = true;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Color c = filter.color;
        c.a = 0f;
        filter.color = c;

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);

            c.a = alpha;
            filter.color = c;

            yield return null;
        }

        // ensure = 1
        c.a = 1f;
        filter.color = c;
    }
}
