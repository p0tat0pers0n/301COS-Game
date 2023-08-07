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
            activeSlot = 1;
            unEquipItems();
            holdItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeSlot = 2;
            unEquipItems();
            holdItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeSlot = 3;
            unEquipItems();
            holdItem();
        }
    }

    private void unEquipItems()
    {
        if (activeSlot != prevSlot)
        {
            for (int i = 0; i < 3; i++)
            {
                items[i].equipped = false;
                hotbar.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);//change material to get the unselected colour
            }
            for (int i = 0; i < hand.transform.childCount; i++)
            {
                Destroy(hand.transform.GetChild(i).gameObject);
            }
        }
    }
    Quaternion rotation;
    private void holdItem()
    {
        if (activeSlot != prevSlot) {
            prevSlot = activeSlot;
            GameObject itemModel = Instantiate(items[activeSlot - 1].model);
            itemModel.transform.parent = hand.transform;
            itemModel.transform.rotation = Quaternion.Euler(new Vector3(items[activeSlot - 1].rotation.x, 0, items[activeSlot - 1].rotation.z));
            itemModel.transform.position = new Vector3(hand.transform.position.x + items[activeSlot - 1].position.x, hand.transform.position.y + items[activeSlot - 1].position.y, hand.transform.position.z + items[activeSlot - 1].position.z);

            hotbar.transform.GetChild(activeSlot - 1).gameObject.GetComponent<Image>().color = new Color32(113, 102, 102, 100);//change material to get the weird grey selected colour
        }
    }
}
