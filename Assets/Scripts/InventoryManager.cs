using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Item[] items = new Item[3];
    public Item potatoes;
    public GameObject hotbar;

    private int activeSlot;
    private GameObject hoe, shovel, wateringcan, potatoes;

    // Start is called before the first frame update
    void Start()
    {
        updateInventoryDisplay();
        hoe = Instantiate(Resources.Load())
    }

    public void updateInventoryDisplay()
    {
        for (int i = 0; i < items.Length; i++)
        {
            GameObject slot = hotbar.transform.Find("HotbarItem" + i).gameObject;
            slot.transform.Find("Icon").gameObject.GetComponent<Image>().sprite = items[i].icon;
            slot.transform.Find("Name").gameObject.GetComponent<Text>().text = items[i].itemName;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            activeSlot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeSlot = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeSlot = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            activeSlot = 4;
        }
    }

    private void unEquipItems()
    {
        for (int i = 0; i<4; i++)
        {
            items[i].equipped = false;
        }
    }

    private void holdItem()
    {
        switch(activeSlot)
        {
            case 1:
                items[activeSlot].equipped = true;

                break;
            case 2:
                items[activeSlot].equipped = true;

                break;
            case 3:
                items[activeSlot].equipped = true;

                break;
            case 4:
                potatoes.equipped = true;
                break;
        }
    }
}
