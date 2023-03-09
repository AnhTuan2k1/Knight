using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoadershop : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI textProgress;
    PlayerSetting setting;

    private void Start()
    {
        LoadLevel(6);
    }


    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    private IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);

        operation.completed += Operation_completed;
        while (!operation.isDone)
        {
            slider.value = operation.progress;
            textProgress.text = operation.progress * 100f + "%";
            
            yield return null;
        }
    }

    private void Operation_completed(AsyncOperation obj)
    {
        obj.completed -= Operation_completed;
    }
}
