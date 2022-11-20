using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoControl : MonoBehaviour
{
    public ConfigWeaponRecord cf;
    // Start is called before the first frame update
    public Image weapon_icon;
    public Text weapon_damge;
    private string damge;
    public Text weapon_clipSize;
    private string clipSize;
    public Text weapon_fireRate;
    private string fireRate;
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnShowWeaponInfo()
    {
        cf = ConfigManager.instance.configWeapon.GetRecordByKeySearch(gameObject.name);
        damge = cf.Damge.ToString();
        clipSize = cf.Clip_size.ToString();
        fireRate = cf.Rof.ToString();
        weapon_icon.overrideSprite = LibarySpriteGun.instance.GetSpriteGun(gameObject.name);
        weapon_damge.text = "Damage: " + damge + "+" + "<color=green>" + 1 + "</color>";
        weapon_clipSize.text = "Bullet: " + clipSize + "+" + "<color=green>" + 1 + "</color>";
        weapon_fireRate.text = "FireRate: " + fireRate + "+" + "<color=green>" + 0.01 + "</color>";
    }
}
