using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : BYSingleton<LoadSceneManager>
{
    private Action callback;
    public GameObject loadingScene;
    public Text progress_lb;
    public Slider progress_sl;

    private int index = -1;
    private string name = string.Empty;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int index, Action callback)
    {
        this.index = index;
        this.callback = callback;
        StopCoroutine("Load");
        StartCoroutine("Load");
    }
    public void LoadScene(string name, Action callback)
    {
        this.name = name;
        this.callback = callback;
        StopCoroutine("Load");
        StartCoroutine("Load");
    }

    IEnumerator Load()
    {
        
        progress_lb.text = "0%";
        progress_sl.value = 0;
        loadingScene.SetActive(true);
        
        AsyncOperation asyncOperation = null;
        if(index >= 0)
        {
            asyncOperation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        }
        else
        {
            asyncOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        }
        float count = 0;
        while (count <= 50)
        {
            count++;
            yield return new WaitForSeconds(0.1f);
            progress_sl.value = count / 100;
            progress_lb.text = count + "%";
        }
        yield return new WaitUntil(() => asyncOperation.progress >= 0.5f);

        while (!asyncOperation.isDone)
        {
            yield return new WaitForSeconds(0.1f);
            progress_sl.value = asyncOperation.progress;
            progress_lb.text = (asyncOperation.progress * 100).ToString() + "%";
        }

        
        progress_lb.text = "100%";
        progress_sl.value = 1;
        loadingScene.SetActive(false);
        callback?.Invoke();
        index = -1;
        name = string.Empty;


    }
}
