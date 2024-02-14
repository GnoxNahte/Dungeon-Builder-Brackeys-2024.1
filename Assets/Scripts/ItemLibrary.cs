using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Library", menuName = "ScriptableObjs/Item Library")]
public class ItemLibrary : ScriptableObject
{
    public ItemInfo[] items;
}
