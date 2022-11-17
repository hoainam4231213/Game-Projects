using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : BYSingleton<MenuManager>
{
    public Transform anchor;
    public List<Text> ls_text;
    public List<Sprite> ls_sprite;
    public List<Sprite> ls_spriteActive;
    private Dictionary<string, Text> dic_text = new Dictionary<string, Text>();
    private Dictionary<string, Sprite> dic_sprite = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> dic_spriteActive = new Dictionary<string, Sprite>();

    
    public Image currentImage;
    public Text currentText;
    // Start is called before the first frame update
    private void Start()
    {
        foreach(Text txt in ls_text)
        {
            dic_text.Add(txt.name, txt);
        }
        foreach (Sprite sprite in ls_sprite)
        {
            dic_sprite.Add(sprite.name, sprite);
        }
        foreach (Sprite sprite_active in ls_spriteActive)
        {
            dic_spriteActive.Add(sprite_active.name, sprite_active);
        }

        ChangeTextColor(currentText);
        ChangeSprite(currentImage);
    }

    public void ChangeTextColor(Text text)
    {
        foreach(Text txt in ls_text)
        {
            txt.color = txt == dic_text[text.name] ? Color.white : Color.gray; 
        }
    }

    public void ChangeSprite(Image old_image)
    {
        if (currentImage != null)
            currentImage.overrideSprite = dic_sprite[currentImage.name.ToLower()];
        currentImage = old_image;
        currentImage.overrideSprite = dic_spriteActive[(currentImage.name + "_active").ToLower()];
    }

    public void EnableMenu()
    {
        anchor.gameObject.SetActive(true);
    }
    public void DisableMenu()
    {
        anchor.gameObject.SetActive(false);
    }
}
