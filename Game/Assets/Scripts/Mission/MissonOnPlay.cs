using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissonOnPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayMisson()
    {
        GameManager.instance.configMission = ConfigManager.instance.configMission.GetRecordByKeySearch(gameObject.name);
        LoadSceneManager.instance.LoadScene(GameManager.instance.configMission.Map, () =>
        {
            GameObject go = Instantiate(Resources.Load("Mission/" + GameManager.instance.configMission.Mission_type.ToString(), typeof(GameObject))) as GameObject;
            MenuManager.instance.DisableMenu();
            ViewManager.instance.SwitchView(ViewIndex.EmptyView);
        });
    }
}
