using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Sprite[] cutsceneImages;
    [SerializeField] private Image image;
    [SerializeField] private GameObject parentHeader;
    int currentCutscene;
    [Header("Wiggle")]
    [SerializeField] private float wiggleDelayed;
    [SerializeField] private float wiggleRotation;
    AudioSource audioSource;
    [SerializeField] AudioClip turningPageAudio;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentCutscene = 0;
        wiggleEffect();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextPage();
        }
    }

    void NextPage()
    {
        audioSource.PlayOneShot(turningPageAudio);
        if (currentCutscene >= cutsceneImages.Length - 1)
        {
            SceneManager.LoadScene("Level_Tiw");
        }
        else
        {
            currentCutscene++;
            image.sprite = cutsceneImages[currentCutscene];
        }



    }

    void wiggleEffect()
    {
        StartCoroutine(Wiggle());

        IEnumerator Wiggle()
        {
            image.gameObject.transform.rotation = Quaternion.Euler(0, 0, wiggleRotation);
            yield return new WaitForSeconds(wiggleDelayed);
            image.gameObject.transform.rotation = Quaternion.Euler(0, 0, -wiggleRotation);
            yield return new WaitForSeconds(wiggleDelayed);
            StartCoroutine(Wiggle());
        }
    }
}
