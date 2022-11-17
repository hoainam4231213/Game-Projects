using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    public Image image;
    public Text text;
    public void OnShowHomeView()
    {
        MenuManager.instance.ChangeTextColor(text);
        MenuManager.instance.ChangeSprite(image);
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
}
