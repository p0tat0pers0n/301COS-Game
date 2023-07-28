using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Item Details")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public GameObject model;
    public string itemName;
    public int amount;
    public int hotbarSlot;
    public bool equipped;
}
