using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [Header("Items type")]

    public bool isItem;
    public bool isWeapon;
    public bool isArmor;
    [Header("Items details")]

    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    public int amountToChange;
    public bool affectHP, affectMP, affectStr;

    [Header("weapon details")]
    public int weaponStrength;
    public int armorStrength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        Profile player = GameManager.instance.playersStats;

        if(isItem)
        {
            if(affectHP)
            {
                player.currentHP += amountToChange;
                if(player.currentHP > player.maxHP)
                {
                    player.currentHP = player.maxHP;
                }
            }
            if (affectMP)
            {
                player.currentMP += amountToChange;
                if (player.currentMP > player.maxMP)
                {
                    player.currentMP = player.maxMP;
                }
            }
            if (affectStr)
            {
                player.streng += amountToChange;
            }
        }

        if(isWeapon)
        {
            if(player.equipperWepon != "")
            {
                GameManager.instance.AddItem(player.equipperWepon);
            }

            player.equipperWepon = itemName;
            player.weponPower = weaponStrength;
        }
        
        if(isArmor)
        {
            if (player.equipperArmor != "")
            {
                GameManager.instance.AddItem(player.equipperArmor);
            }

            player.equipperArmor = itemName;
            player.armorPower = armorStrength;
        }

        GameManager.instance.RemoveItem(itemName);
    }
}
