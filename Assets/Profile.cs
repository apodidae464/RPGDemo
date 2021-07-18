using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour
{
    public string charName;
    public int playerlvl = 1;
    public int currentEXP = 0;
    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP = 30;
    public int streng;
    public int defence;
    public int weponPower;
    public int armorPower;
    public string equipperWepon;
    public string equipperArmor;
    public Sprite charImg;

    public int[] expLevel;
    public int maxlvl = 9;
    public int baseEXP = 1000;

    public int currentGold;
    // Start is called before the first frame update
    void Start()
    {
        expLevel = new int[maxlvl];
        for(int i = 1; i < expLevel.Length; i++)
        {
            expLevel[i] = baseEXP + (500 * (i-1));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addEXP(int exp)
    {
        if (playerlvl == maxlvl)
            return;
        currentEXP += exp;
        if(currentEXP > expLevel[playerlvl])
        {
            currentEXP -= expLevel[playerlvl];
            playerlvl++;
            updateBaseStats();
        }
    }

    void updateBaseStats()
    {
        streng++;
        defence++;
        maxHP += 100;
        maxMP += 5;
    }
}
