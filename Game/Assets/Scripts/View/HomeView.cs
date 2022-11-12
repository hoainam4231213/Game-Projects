using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeView : BaseView
{
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void OnCityScene()
    {
        LoadSceneManager.instance.LoadScene(2, () =>
        {
            MenuManager.instance.DisableMenu();
            ViewManager.instance.SwitchView(ViewIndex.EmptyView);
        });
    }
    public void OnTownScene()
    {
        LoadSceneManager.instance.LoadScene(3, () =>
        {
            MenuManager.instance.DisableMenu();
            ViewManager.instance.SwitchView(ViewIndex.EmptyView);
        });
    }
}
