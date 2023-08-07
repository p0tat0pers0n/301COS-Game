using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Item Details")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public GameObject model;
    public string itemName;
    public int amount, hotbarSlot;
    public bool equipped;
    public Vector3 position;
    public Quaternion rotation;
}
