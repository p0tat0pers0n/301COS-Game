using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScript : MonoBehaviour
{   
    public GameObject player;

    [SerializeField] private Vector3 respawnPosition;
    [SerializeField] private bool isDead;
    private int opacity, delay;
    private LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        opacity = 50;
        mask = LayerMask.GetMask("Ocean");
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
        }

        if (!isDead && opacity >= 255)
        {
            delay++;
        }

        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance <= 5)
            {

            }
        }
    }

    public void playerDeath()
    {
        isDead = true;
    }
}
