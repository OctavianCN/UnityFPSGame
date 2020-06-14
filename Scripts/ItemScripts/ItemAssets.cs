using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets: MonoBehaviour 
{ 
    public static ItemAssets Instance { get; private set; }
    public Sprite pistolSprite;
    public Sprite m4Sprite;
    public Sprite medKitSprite;
    public Sprite pistolBulletSprite;
    public Sprite m4BulletSprite;

    private void Awake()
    {
        Instance = this;
        
    }

    

}
