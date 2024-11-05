using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemValue
{
    public string itemName; // アイテム名
    public int itemID; // アイテムのID
    public int itemValue; // アイテムの効果量
}

[CreateAssetMenu(menuName = "ScriptableObject/ItemValueManager", fileName = "ItemValueManager")]

public class ItemValueManager : ScriptableObject
{
    public List<ItemValue> DataList;
}