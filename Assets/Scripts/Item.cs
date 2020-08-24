using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public enum ItemType
    {
        weapon,
        armor,
        poison,
        material,
        book
    }
    public string id;
    public string itemName;
    public string description;
    public ItemType type;
    public int stack;
    public Dictionary<string, object> attributes = new Dictionary<string, object>();
}
