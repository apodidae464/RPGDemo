using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image buttonImage;
    public Text amountText;
    public int buttonValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPress()
    {
        if(GameMenus.instance.theMenu.activeInHierarchy)
        {
            if(GameManager.instance.itemsHeld[buttonValue] != "")
            {
                GameMenus.instance.selectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
            }
        }

        if(Shope.instance.shopMenu.activeInHierarchy)
        {
            if(Shope.instance.buyMenu.activeInHierarchy)
            {
                Shope.instance.selectBuyItem(GameManager.instance.GetItemDetails(Shope.instance.itemForSale[buttonValue]));
            }
            if(Shope.instance.sellMenu.activeInHierarchy)
            {
                Shope.instance.selectSellItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
            }
        }
    }
}
