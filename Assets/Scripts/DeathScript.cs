using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScript : MonoBehaviour
{   
    /// <summary>
    /// This script plays on the player's death. Dimming the screen and returning them to the field outside their house. 
    /// </summary>
    public GameObject player;
    public PlayerMovement playerMovement;

    [SerializeField] private Vector3 respawnPosition;
    [SerializeField] private bool isDead;
    private int opacity, delay;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        opacity = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead && opacity <= 255)
        {
            if (opacity >= 255)
            {
                player.transform.position = respawnPosition; // Player's screen has dimmed, now teleported
                isDead = false; // No longer dead
            }
            gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)opacity); // Increases the opacity and sets the player ui to it
            opacity++;
        }
        else if (!isDead && opacity > 0 && delay >= 250)
        {
            opacity--; // Once the player has been teleported start decreasing the opacity 
            gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)opacity);
            if (opacity == 10) { playerMovement.allowPlayerMovement = true; } // Once the opacity is effectively gone allow player movement again
        }

        if (!isDead && opacity >= 255)
        {
            delay++; // Adds a delay before allowing an increase in opacity
        }
        
        // Checks if the player dies to the ocean and marks them as dead
        if (Physics.Raycast(player.transform.position, -Vector3.up, out hit, (float)1)) {
            if (hit.collider.ToString() == "Ocean (UnityEngine.MeshCollider)")
            {
                isDead = true;
                playerMovement.allowPlayerMovement = false;
            }
        }
    }

    // Allows other scripts to reference the death script and kill the player
    public void playerDeath()
    {
        isDead = true;
        playerMovement.allowPlayerMovement = false;
    }
}