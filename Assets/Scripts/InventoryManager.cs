using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    /// <summary>
    /// This script manages the showing of inventory to the player.
    /// It also holds and removes held items based on the player's input
    /// </summary>
    public Item[] items = new Item[3];
    public GameObject hotbar;
    public GameObject hand;

    private int activeSlot, prevSlot;

    // Start is called before the first frame update
    void Start()
    {
        // Makes sure that the items given to the player at the beginning of the game are shown in the hotbar
        updateInventoryDisplay();
        activeSlot = 0;
    }
    /// <summary>
    /// https://gamedevbeginner.com/how-to-make-an-inventory-system-in-unity/
    /// I used this tutorial as inspiration for my inventory system
    /// I copied the updateInventoryDisplay function however, I have changed the rest of the code and added my own as indicated below
    /// </summary>

    public void updateInventoryDisplay()
    {
        // This function gets the icon and name of a hotbaritem and displays it to the player
        // I got this function from the previously mentioned tutorial which I modified to fit the smaller fixed inventory system and my gameobject names
        for (int i = 0; i < items.Length; i++)
        {
            GameObject slot = hotbar.transform.Find("HotbarItem" + i).gameObject; 
            slot.transform.Find("Icon").gameObject.GetComponent<Image>().sprite = items[i].icon; // Adds the icon to the hotbar
            slot.transform.Find("Name").gameObject.GetComponent<Text>().text = items[i].itemName; // Adds the item name to the hotbar
        }
    }

    private void Update()
    {
        // Checks if the player presses a number button and calls the holdItem() to hold the respective item
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
        // Unequips all current items to make way for the next one
        prevSlot = 0;
        for (int i = 0; i < 3; i++)
        {
            // Goes through each of the 3 slots and sets it to unequipped
            items[i].equipped = false;
            hotbar.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);//change material to get the unselected colour
        }
        for (int i = 0; i < hand.transform.childCount; i++)
        {
            // Goes through the player's hand's children and destroys anything there (It does all instead of the one expected in case of a possible bug)
            Destroy(hand.transform.GetChild(i).gameObject);
        }
    }

private void holdItem()
    {
        if (activeSlot != prevSlot) 
        {
            // Makes sure that the function only runs once by checking that the active slot is not the same as the one previously selected
            prevSlot = activeSlot;
            items[activeSlot - 1].equipped = true; // Sets the item in the list to equipped
            GameObject itemModel = Instantiate(items[activeSlot - 1].model); // Creates a copy of the asset from the assets folder and loads it into the scene
            itemModel.transform.parent = hand.transform; // Sets the parent of the tool model to the player's hand
            itemModel.transform.rotation = hand.transform.rotation * Quaternion.Euler(items[activeSlot - 1].rotation); // Adds the rotation of the hand to the specific rotation of the tools as given in the scriptable object
            itemModel.transform.position = new Vector3(hand.transform.position.x + items[activeSlot - 1].position.x, hand.transform.position.y + items[activeSlot - 1].position.y, hand.transform.position.z + items[activeSlot - 1].position.z);
            // Adds the position of the hand to the one given in the scriptable object
            hotbar.transform.GetChild(activeSlot - 1).gameObject.GetComponent<Image>().color = new Color32(113, 102, 102, 100);//change material to get the weird grey selected colour
        }
    }
}
