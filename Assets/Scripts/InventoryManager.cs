using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Item[] items = new Item[3];
    public GameObject hotbar;
    public GameObject hand;

    private int activeSlot, prevSlot;

    // Start is called before the first frame update
    void Start()
    {
        updateInventoryDisplay();
        activeSlot = 0;
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
            items[i].equipped = false;
        }
        for (int i = 0; i<hand.transform.childCount; i++)
        {
            Destroy(hand.transform.GetChild(i));
        }
    }

    private void holdItem()
    {
        if (activeSlot != prevSlot) {
            prevSlot = activeSlot;
            GameObject itemModel = Instantiate(items[(activeSlot - 1)].model);
            itemModel.transform.parent = hand.transform;
            itemModel.transform.rotation = hand.transform.rotation;
            itemModel.transform.position = hand.transform.position;

            hotbar.transform.GetChild(activeSlot - 1).GetComponent<>//change material to get the weird grey selected colour
        }
    }
}
