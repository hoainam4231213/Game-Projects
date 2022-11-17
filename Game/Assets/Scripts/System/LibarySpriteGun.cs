using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibarySpriteGun : BYSingleton<LibarySpriteGun>
{
    public List<Sprite> ls_sprites;
    private Dictionary<string, Sprite> dic_sprites = new Dictionary<string, Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Sprite s in ls_sprites)
        {
            dic_sprites.Add(s.name, s);
        }
    }

    public Sprite GetSpriteGun(string name)
    {
        return dic_sprites[name];
    }
}
