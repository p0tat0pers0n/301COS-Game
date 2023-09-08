using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScript : MonoBehaviour
{   
    public GameObject player;
    public PlayerMovement playerMovement;

    [SerializeField] private Vector3 respawnPosition;
    [SerializeField] private bool isDead;
    private int opacity, delay;
    private LayerMask mask;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        opacity = 50;
        mask = LayerMask.GetMask("Water");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead && opacity <= 255)
        {
            if (opacity >= 255)
            {
                player.transform.position = respawnPosition;
                isDead = false;
            }
            gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)opacity);
            opacity++;
        }
        else if (!isDead && opacity > 0 && delay >= 250)
        {
            opacity--;
            gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)opacity);
            if (opacity == 10) { playerMovement.allowPlayerMovement = true; }
        }

        if (!isDead && opacity >= 255)
        {
            delay++;
        }
        
        if (Physics.Raycast(player.transform.position, -Vector3.up, out hit, (float)1)) {
            Debug.Log(hit.collider.ToString());
            if (hit.collider.ToString() == "Ocean (UnityEngine.MeshCollider)")
            {
                isDead = true;
            }
        }
    }

    public void playerDeath()
    {
        isDead = true;
        playerMovement.allowPlayerMovement = false;
    }
}