using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Profile playersStats;

    public bool gameMenuOpen;
    public bool dialogActive;
    public bool fadingBetweenArea;
    public bool shopActive;
    

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Items[] referenceItems;

    // Start is called before the first frame update
    void Start()
    {  

        instance = this;
        DontDestroyOnLoad(gameObject);
        playersStats.currentGold = 100;
        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingBetweenArea || shopActive)
        {
            if(PlayerController.instance)
                 PlayerController.instance.canMove = false;
        }
        else
        {
            if (PlayerController.instance)
                PlayerController.instance.canMove = true;
        }
    }

    public Items GetItemDetails(string itemToGrab)
    {
        for(int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }
        return null;

    }

    public void SortItems()
    {
        bool itemafterSpace = true;
        while(itemafterSpace)
        {
            itemafterSpace = false;

            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if(itemsHeld[i] != "")
                    {
                        itemafterSpace = true;
                    }
                }
            }
        }
        
    }

    public void AddItem(string itemName)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "" || itemsHeld[i] == itemName)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
            }
        }

        if(foundSpace)
        {
            bool itemExists = false;
            for(int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemName)
                {
                    itemExists = true;
                    i = referenceItems.Length;
                }
            }

            if(itemExists)
            {
                itemsHeld[newItemPosition] = itemName;
                numberOfItems[newItemPosition]++;
            } else
            {
                Debug.LogError(itemName + " Does not exists...");
            }
        }
        SortItems();

    }

    public void RemoveItem(string itemName)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == itemName)
            {
                foundItem = true;
                itemPosition = i;
                i = itemsHeld.Length;
            }
        }

        if(foundItem)
        {
            numberOfItems[itemPosition]--;
            if(numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }
            GameMenus.instance.onItemsButtonActive();
        } else
        {
            Debug.LogError("Can not find " + itemName);
        }
    }

    public void saveData()
    {
        PlayerPrefs.SetString("Current_Scene_", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Current_player_Pos_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Current_player_Pos_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Current_player_Pos_z", PlayerController.instance.transform.position.z);
    
        
        PlayerPrefs.SetString("Player_" + playersStats.charName + "_playerName", playersStats.charName);
        PlayerPrefs.SetInt("Player_" + playersStats.charName + "_level", playersStats.playerlvl);
        PlayerPrefs.SetInt("Player_" + playersStats.charName + "_currentEXP", playersStats.currentEXP);
        PlayerPrefs.SetInt("Player_" + playersStats.charName + "_currentHP", playersStats.currentHP);
        PlayerPrefs.SetInt("Player_" + playersStats.charName + "_currentMP", playersStats.currentMP);
        PlayerPrefs.SetInt("Player_" + playersStats.charName + "_maxHP", playersStats.maxHP);
        PlayerPrefs.SetInt("Player_" + playersStats.charName + "_maxMP", playersStats.maxMP);
        PlayerPrefs.SetInt("Player_" + playersStats.charName + "_strength", playersStats.streng);
        PlayerPrefs.SetInt("Player_" + playersStats.charName + "_defence", playersStats.defence);
        
        if(playersStats.equipperWepon != null)
        {
            PlayerPrefs.SetString("Player_" + playersStats.charName + "_equipmentWeapon", playersStats.equipperWepon);
            PlayerPrefs.SetInt("Player_" + playersStats.charName + "_weaponPower", playersStats.weponPower);
        }
        else
        {
            PlayerPrefs.SetString("Player_" + playersStats.charName + "_equipmentWeapon", "");
            PlayerPrefs.SetInt("Player_" + playersStats.charName + "_weaponPower", 0);
        }
        if (playersStats.equipperArmor != null)
        {
            PlayerPrefs.SetString("Player_" + playersStats.charName + "_equipmentArmor", playersStats.equipperArmor);
            PlayerPrefs.SetInt("Player_" + playersStats.charName + "_armorPower", playersStats.armorPower);
        }
        else
        {
            PlayerPrefs.SetString("Player_" + playersStats.charName + "_equipmentArmor", "");
            PlayerPrefs.SetInt("Player_" + playersStats.charName + "_armorPower", 0);
        }

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("ItemInventory_" + i, itemsHeld[i]);
            PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItems[i]);
        }
    }

    public void loadData()
    {
        PlayerController.instance.transform.position
            = new Vector3(
                PlayerPrefs.GetFloat("Current_player_Pos_x"),
                 PlayerPrefs.GetFloat("Current_player_Pos_y"),
                   PlayerPrefs.GetFloat("Current_player_Pos_z"));

        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene_"));
        playersStats.gameObject.SetActive(true);
        playersStats.name = PlayerPrefs.GetString("Player_" + playersStats.charName + "_playerName");
        playersStats.playerlvl = PlayerPrefs.GetInt("Player_" + playersStats.charName + "_level");
        playersStats.currentEXP = PlayerPrefs.GetInt("Player_" + playersStats.charName + "_currentEXP");
        playersStats.currentHP = PlayerPrefs.GetInt("Player_" + playersStats.charName + "_currentHP");
        playersStats.currentMP = PlayerPrefs.GetInt("Player_" + playersStats.charName + "_currentMP");
        playersStats.maxHP = PlayerPrefs.GetInt("Player_" + playersStats.charName + "_maxHP");
        playersStats.maxMP = PlayerPrefs.GetInt("Player_" + playersStats.charName + "_maxMP");
        playersStats.streng = PlayerPrefs.GetInt("Player_" + playersStats.charName + "_strength");
        playersStats.defence = PlayerPrefs.GetInt("Player_" + playersStats.charName + "_defence");
        playersStats.weponPower = PlayerPrefs.GetInt("Player_" + playersStats.charName + "_weaponPower");
        playersStats.armorPower = PlayerPrefs.GetInt("Player_" + playersStats.charName + "_armorPower");
        playersStats.equipperWepon = PlayerPrefs.GetString("Player_" + playersStats.charName + "_equipmentWeapon");
        playersStats.equipperArmor = PlayerPrefs.GetString("Player_" + playersStats.charName + "_equipmentArmor");

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("ItemInventory_" + i);
            numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }
    }

   
}
