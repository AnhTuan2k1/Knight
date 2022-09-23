using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI textProgress;
    PlayerSetting setting;

    private void Start()
    {
        setting = GameObject.FindWithTag("PlayerSetting")
            .GetComponent<PlayerSetting>();

        LoadLevel(1);
    }


    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    private IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);


        while (!operation.isDone)
        {
            slider.value = operation.progress;
            textProgress.text = operation.progress * 100f + "%";

            if (setting != null) setting.SetScene((MyScene)sceneIndex);
            yield return null;
        }
    }
}
