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
        GameManager.instance.configMission = ConfigManager.instance.configMission.GetRecordByKeySearch("M_001");
        LoadSceneManager.instance.LoadScene(2, () =>
        {
            GameObject go = Instantiate(Resources.Load("Mission/" + GameManager.instance.configMission.Mission_type.ToString(), typeof(GameObject))) as GameObject;
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
