using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemSelection : MonoBehaviour
{
    public ItemInfo itemInfo;
    public UnityEvent<string> test;

    public void Init(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;
    }
}
