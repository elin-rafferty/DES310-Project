using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    [SerializeField] Sprite idleSprite, pistolSprite, rifleSprite, shotgunSprite;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AgentWeapon agentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = GetSpriteFromEquippedWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = GetSpriteFromEquippedWeapon();
    }

    Sprite GetSpriteFromEquippedWeapon()
    {
        // Gotta assign something cause C# doesn't trust me
        Sprite sprite = idleSprite;
        {
            switch(agentWeapon.weapon.Name) {
                case "Laser Pistol":
                    sprite = pistolSprite;
                    break;
                case "Laser Rifle":
                    sprite = rifleSprite;
                    break;
                case "Shotgun Sprite":
                    sprite = shotgunSprite;
                    break;
            }
        }
        return sprite;
    }
}
