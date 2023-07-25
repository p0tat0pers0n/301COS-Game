using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Item[] items = new Item[3];
    public Item potatoes;
    public GameObject hotbar;

    // Start is called before the first frame update
    void Start()
    {
        updateInventoryDisplay();
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
}
