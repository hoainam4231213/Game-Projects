using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BootLoader : MonoBehaviour
{
    public Image image_battle;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ConfigManager.instance.InitConfig(() =>
        {
            LoadSceneManager.instance.LoadScene(1, () =>
            {
                Debug.Log("load buffer");
                MenuManager.instance.EnableMenu();
                ViewManager.instance.SwitchView(ViewIndex.HomeView);
            });
        });
    }
}
