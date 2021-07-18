using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shope : MonoBehaviour
{
    public static Shope instance;

    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;

    public Text goldText;

    public string[] itemForSale;

    public ItemButton[] buyItemButton;
    public ItemButton[] sellItemButton;

    public Items selectedItem;
    public Text buyItemName, buyItemDescription, buyItemValue, sellItemName, sellItemDescription, sellItemValue;

    public bool isMusicPlayed;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openShop()
    {
        shopMenu.SetActive(true);
        openBuyMenu();
        GameManager.instance.shopActive = true;

        goldText.text = GameManager.instance.playersStats.currentGold.ToString();
    }

    public void closeShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopActive = false;
    }

    public void openBuyMenu()
    {
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        buyItemButton[0].onPress();

        for (int i = 0; i < buyItemButton.Length; i++)
        {
            buyItemButton[i].buttonValue = i;
            if (itemForSale[i] != "")
            {
                buyItemButton[i].buttonImage.gameObject.SetActive(true);
                buyItemButton[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemForSale[i]).itemSprite;
                buyItemButton[i].amountText.text = "";
            }
            else
            {
                buyItemButton[i].buttonImage.gameObject.SetActive(false);
                buyItemButton[i].amountText.text = "";
            }
        }
    }
    public void openSellMenu()
    {
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);

       sellItemButton[0].onPress();
        showSellItem();
        
    }

    private void showSellItem()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i < sellItemButton.Length; i++)
        {
            sellItemButton[i].buttonValue = i;
            if (GameManager.instance.itemsHeld[i] != "")
            {
                sellItemButton[i].buttonImage.gameObject.SetActive(true);
                sellItemButton[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                sellItemButton[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                sellItemButton[i].buttonImage.gameObject.SetActive(false);
                sellItemButton[i].amountText.text = "";
            }
        }
    }

    public void selectBuyItem(Items buyItem)
    {
        selectedItem = buyItem;
        if (selectedItem)
        {
            buyItemName.text = selectedItem.itemName;
            buyItemDescription.text = selectedItem.description;
            buyItemValue.text = "Value: " + selectedItem.value.ToString() + "$";
        }
        else
        {
            sellItemName.text = "...";
            sellItemDescription.text = "...";
            sellItemValue.text = "...";
        }
    }

    public void selectSellItem(Items sellItem)
    {
        selectedItem = sellItem;
        if(selectedItem)
        {
            sellItemName.text = selectedItem.itemName;
            sellItemDescription.text = selectedItem.description;
            sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.value * 0.5f).ToString() + "$";
        } 
        else
        {
            sellItemName.text = "...";
            sellItemDescription.text = "...";
            sellItemValue.text = "...";
        }
        
    }

    public void buyItem()
    {
        if (selectedItem == null)
            return;
        if(GameManager.instance.playersStats.currentGold >= selectedItem.value)
        {
            GameManager.instance.playersStats.currentGold -= selectedItem.value;
            GameManager.instance.AddItem(selectedItem.itemName);
        }

        goldText.text = GameManager.instance.playersStats.currentGold.ToString() + "$";
    }
    public void sellItem()
    {
        if (selectedItem == null)
            return;
        GameManager.instance.playersStats.currentGold += Mathf.FloorToInt(selectedItem.value * 0.5f);
        GameManager.instance.RemoveItem(selectedItem.itemName);

        goldText.text = GameManager.instance.playersStats.currentGold.ToString() + "$";
        showSellItem();
    }
}
