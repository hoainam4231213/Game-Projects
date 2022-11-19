using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image icon_gun;
    public Image health_bar;
    public Text amount_hp;
    public Text amount_projecties;
    public Text amount_enemy;
    private WeaponControl weaponControl;
    private WeaponBehaviour current_wp;
    private PlayerControl playerControl;
    public RectTransform parentHub;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GameObject.FindObjectOfType<PlayerControl>();
        weaponControl = GameObject.FindObjectOfType<WeaponControl>();
        playerControl.OnHPChange += OnHPChange;
        weaponControl.OnChangeGun += OnChangeGun;
        
    }

    private void OnChangeGun(WeaponBehaviour weaponBehaviour)
    {
        if (current_wp != null)
            current_wp.OnAmoChange -= OnAmoChange;
        current_wp = weaponBehaviour;
        current_wp.OnAmoChange += OnAmoChange;
        icon_gun.overrideSprite = LibarySpriteGun.instance.GetSpriteGun(current_wp.weaponData.cf.Weapon_id);
        OnAmoChange(current_wp.projecties, current_wp.clip_size);
    }

    private void OnAmoChange(int projecties, int clip_size)
    {
        if (projecties > clip_size * 0.125)
            amount_projecties.text = projecties + "/" + clip_size;
        else
            amount_projecties.text = "<color=red>" + projecties + "</color>/" + clip_size;

    }

    private void OnHPChange(int hp, int maxhp)
    {
        health_bar.fillAmount = (float)hp / (float)maxhp;
        amount_hp.text = hp + "/" + maxhp;
    }

    public void OnSwitchGun()
    {
        weaponControl.ChangeGun();
    }
    public void OnAmountEnemy(int amount)
    {
        amount_enemy.text = amount.ToString();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
