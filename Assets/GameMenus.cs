using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenus : MonoBehaviour
{
    public static GameMenus instance;

    public GameObject theMenu;
    public GameObject[] windows;
    private Profile playerStats;

    public Text pName, php, pmp, pattack, pdefense, pexp, pWeaponEquip, pArmorEquip, currentGold;
    public Image pimg;
    public Slider pexpSlide;

    public ItemButton[] itemButtons;
    public string selectedItem;
    public Items activeItem;
    public Text itemName, itemDescription, equipButtonText;
    public string mainMenuGame;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(theMenu.activeInHierarchy)
        {
            GameManager.instance.gameMenuOpen = true;
            DialogsActivator.cActive = false;
        }
        else
        {
            GameManager.instance.gameMenuOpen = false;
            DialogsActivator.cActive = true;

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.playSFX(5);

            if (!theMenu.activeInHierarchy)
            {
                openMenu();
            }
            else
            {
                closeMenu();
            }
        }
    }

    public void onClickState(int state)
    {
        toggleWindows(state);
        switch (state)
        {
            case 0:
                onStatsButtonActive();
                break;
            case 1:
                onItemsButtonActive();
                break;
            case 2:
                onSaveButtonClick();
                break;
            case 3:
                closeMenu();
                break;
            case 4:
                onQuitButtonClick();
                break;
            default:
                closeMenu();
                break;
        }
    }

    public void toggleWindows(int state)
    {
        for(int i = 0; i < windows.Length; i++)
        {
            if(state == i)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }
    }

    public void onStatsButtonActive()
    {
        AudioManager.instance.playSFX(4);

        playerStats = GameManager.instance.playersStats;
        pName.text = playerStats.name;
        php.text = "HP: " + playerStats.currentHP + "/" + playerStats.maxHP;
        pmp.text = "MP: " + playerStats.currentMP + "/" + playerStats.maxMP;
        pattack.text = "Attack: " + (int)(playerStats.streng + playerStats.weponPower);
        pdefense.text = "Defence: " + (int)(playerStats.defence + playerStats.armorPower);
        pexpSlide.maxValue = playerStats.expLevel[playerStats.playerlvl];
        pexpSlide.value = playerStats.currentEXP;
        pexp.text = "" + playerStats.currentEXP + "/" + playerStats.expLevel[playerStats.playerlvl];
        pWeaponEquip.text = "Weapon: +" + playerStats.weponPower + "Attack - " + playerStats.equipperWepon;
        pArmorEquip.text = "Armor:+" + playerStats.armorPower + "Defense - " + playerStats.equipperArmor;
        currentGold.text = "" + playerStats.currentGold + "$";
    }
    public void onItemsButtonActive()
    {
        AudioManager.instance.playSFX(4);

        GameManager.instance.SortItems();

        for(int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;
            if (GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void selectItem(Items item)
    {
        AudioManager.instance.playSFX(4);

        activeItem = item;
        if(activeItem.isItem)
        {
            equipButtonText.text = "Use";
        } else if(activeItem.isWeapon || activeItem.isArmor)
        {
            equipButtonText.text = "Equip";
        } else
        {
            equipButtonText.text = "";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    public void discardItem()
    {
        AudioManager.instance.playSFX(5);

        if (activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void useItem()
    {
        activeItem.Use();
        AudioManager.instance.playSFX(6);
    }

    public void onSaveButtonClick()
    {
        AudioManager.instance.playSFX(4);

        GameManager.instance.saveData();
        QuestManager.instance.saveQuestData();
    }
    public void openMenu()
    {
        AudioManager.instance.playSFX(4);

        theMenu.SetActive(true);
    }
    public void closeMenu()
    {
        AudioManager.instance.playSFX(4);

        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        theMenu.SetActive(false);
    }
    public void onQuitButtonClick()
    {
        AudioManager.instance.playSFX(4);

        GameManager.instance.saveData();
        SceneManager.LoadScene(mainMenuGame);
        if(PlayerController.instance)
            Destroy(PlayerController.instance.gameObject);
        if(AudioManager.instance)
            Destroy(AudioManager.instance.gameObject);
        Destroy(gameObject);
    }

    public void playButtonSound()
    {
        if(AudioManager.instance)
             AudioManager.instance.playSFX(4);
    }
}
