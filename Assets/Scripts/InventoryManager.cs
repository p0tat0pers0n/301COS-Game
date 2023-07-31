using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Item[] items = new Item[3];
    public GameObject hotbar;
    public GameObject player;
    public GameObject hoe, shovel, wateringcan;

    private int activeSlot;

    // Start is called before the first frame update
    void Start()
    {
        updateInventoryDisplay();
    }
    /// <summary>
    /// https://gamedevbeginner.com/how-to-make-an-inventory-system-in-unity/
    /// </summary>

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
            unEquipItems();
            activeSlot = 1;
            holdItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            unEquipItems();
            activeSlot = 2;
            holdItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            unEquipItems();
            activeSlot = 3;
            holdItem();
        }
    }

    private void unEquipItems()
    {
        for (int i = 0; i<3; i++)
        {
            Debug.Log(i);
            items[i].equipped = false;
        }
    }

    private void holdItem()
    {
        switch(activeSlot)
        {
            case 1:
                items[activeSlot].equipped = true;
                hoe.transform.parent = player.transform.parent;
                Debug.Log("parented");
                break;
            case 2:
                items[activeSlot].equipped = true;
                shovel.transform.parent = player.transform.parent;
                break;
            case 3:
                items[activeSlot].equipped = true;
                wateringcan.transform.parent = player.transform.parent;
                break;
        }
    }
}
