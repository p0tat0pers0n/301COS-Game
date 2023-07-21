using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Item Details")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    public int amount;
    public bool equipped;
}
