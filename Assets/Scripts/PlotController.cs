using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotController : MonoBehaviour
{
    /// <summary>
    /// This script manages each plot independantly with the proximity text interact and calls the potato manager to alter the number of potatoes.  
    /// </summary>
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject player;
    public Item wateringCan;
    public Item shovel;
    public Item hoe;
    public PotatoManager potatoCounter;

    private int growthState, growthTime, requiredGrowthTime, intervalGrowth;
    private double fixedGrowthTime;
    private bool planted, withinRange, watered, readyToPlant;
    public List<Material> materials = new List<Material>(5);
    // Start is called before the first frame update
    void Start()
    {
        // Sets the default values on load into the game
        growthState = 0;
        growthTime = 0;
        fixedGrowthTime = 0;
        intervalGrowth = 60;
        planted = false;
        watered = false;
        readyToPlant = false;
    }

    private void OnMouseOver()
    {
        // When the player's mouse is over the plot, check that they are within 5 metres and check the state to show the correct proximity text
        if (!planted || !watered || growthState == 3)
        {
            Ray ray;
            RaycastHit hit;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.distance <= 5)
                {
                    withinRange = true;
                    if (!planted) { interactText.GetComponent<Text>().text = "Plant Potatoes"; }
                    if (!watered && wateringCan.equipped && planted) { interactText.GetComponent<Text>().text = "Water Crops"; }
                    if (planted && growthState == 3) { interactText.GetComponent<Text>().text = "Harvest Potatoes"; }
                    if (!planted && !readyToPlant) { interactText.GetComponent<Text>().text = "Till Soil"; }
                }
            }
        }
    }

    private void OnMouseExit()
    {
        // Once the player mouses off the plot set the text to nothing and sets that they are not within range
        withinRange = false;
        interactText.GetComponent<Text>().text = "";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && withinRange && !planted && readyToPlant)
        {
            // If the plot is tilled, the player is within range, they have clicked, and the plot is not already planted in
            if (potatoCounter.potatoes >= 16)
            {
                // If the player has 16 potatoes or more allow them to plant 
                planted = true;
                withinRange = false;
                interactText.GetComponent<Text>().text = "";
                gameObject.GetComponent<MeshRenderer>().material = materials[2]; // Sets the material to the planted material
                potatoCounter.decreasePotatoes(16); // Removes 16 potatoes
            }
        }

        if (Input.GetMouseButtonDown(0) && withinRange && !watered && planted)
        {
            // If the player is within range, they've clicked, the plot is not watered and the plot has been planted in
            if (wateringCan.equipped)
            {
                // If the player is holding the watering can set the plot to watered and set the proximity interact to nothing
                watered = true;
                interactText.GetComponent<Text>().text = "";
            }
        }

        if (Input.GetMouseButtonDown(0) && withinRange && !watered && !planted)
        {
            // If the player is within range, the plot is not watered and there are no crops planted in the plot
            if (hoe.equipped)
            {
                // If the player is holding a hoe set the plot to ready and the proximity interact to nothing
                readyToPlant = true;
                interactText.GetComponent<Text>().text = "";
                text.GetComponent<TextMesh>().text = "Plant Crops";
                gameObject.GetComponent<MeshRenderer>().material = materials[1]; // Sets the plot material to the tilled state 
            }
        }

        if (Input.GetMouseButtonDown(0) && withinRange && shovel.equipped && growthState == 3)
        {
            // If the player is within range, they've clicken, the shovel is equipped and the crops are fully grown
            potatoCounter.increasePotatoes(); // Increases the potatoes by a random number between 16 and 40
            planted = false; // Sets the plot back to defaults
            watered = false;
            growthState = 0;
            fixedGrowthTime = 0;
            growthTime = 0;
            gameObject.GetComponent<MeshRenderer>().material = materials[0];
            readyToPlant = false;
        }
    }

    void FixedUpdate()
    {
        if (planted && watered)
        {
            // If the plot has crops in it and is watered
            fixedGrowthTime++;
            growthTime = (int)fixedGrowthTime / 50;
            requiredGrowthTime = intervalGrowth * growthState;
            // Calculate the time that is needed for it to grow to the next state

            text.GetComponent<TextMesh>().text = growthTime.ToString(); // Display this time over the plot

            switch (growthState)
            {
                // Switches the current growth state to easily allow for the changing between the stages
                // Checks if the time is correct then changes the material and the current growth state
                case 0:
                    if (growthTime >= requiredGrowthTime)
                    {
                        growthState = 1;
                        gameObject.GetComponent<MeshRenderer>().material = materials[growthState + 1];
                    }
                    break;
                case 1:
                    if (growthTime >= requiredGrowthTime)
                    {
                        growthState = 2;
                        gameObject.GetComponent<MeshRenderer>().material = materials[growthState + 1];
                    }
                    break;
                case 2:
                    if (growthTime >= requiredGrowthTime)
                    {
                        growthState = 3;
                        gameObject.GetComponent<MeshRenderer>().material = materials[growthState + 1];
                    }
                    break;
            }
        }else if (!watered && planted)
        {
            // If the crops have been planted and not watered an excalamation mark shows up over the plot
            text.GetComponent<TextMesh>().text = "!";
        }
        
        if (growthState == 3)
        {
            // If the crops are ready to harvest a message shows up
            text.GetComponent<TextMesh>().text = "Harvest Now";
        }
    }
}
