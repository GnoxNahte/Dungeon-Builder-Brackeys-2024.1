using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionUI : MonoBehaviour
{
    [SerializeField] ItemLibrary itemLibrary;
    [SerializeField] GameObject itemCardPrefab;
    [SerializeField] Transform itemSelectionParent;

    [SerializeField] RectTransform selectedIndicatorTransform;

    [Header("Description")]
    // TODO: Change the variable names to be better
    [SerializeField] TextMeshProUGUI description_itemName;
    [SerializeField] TextMeshProUGUI description_itemDescription;
    [SerializeField] Image description_itemIcon;
    [Header("Monster Stats")]
    [SerializeField] GameObject monsterStatsParent;
    [SerializeField] TextMeshProUGUI costStat;
    [SerializeField] TextMeshProUGUI healthStat;
    [SerializeField] TextMeshProUGUI movementSpeedStat;
    [SerializeField] TextMeshProUGUI damageStat;
    [SerializeField] TextMeshProUGUI attackSpeedStat;
    [SerializeField] TextMeshProUGUI skillDescription;

    public ItemInfo SelectedTile { get { return selectedItem.GetComponent<ItemSelection>().itemInfo; } }

    [SerializeField] [ReadOnly]
    GameObject selectedItem;

    private IEnumerator Start()
    {
        // Not sure why but need wait for end of frame so that the selected indicator's position is correct
        yield return new WaitForEndOfFrame();

        OnSelectItem(itemSelectionParent.GetChild(1).gameObject);
    }

    public void OnSelectItem(GameObject itemGameObj)
    {
        selectedItem = itemGameObj;

        ItemInfo item = itemGameObj.GetComponent<ItemSelection>().itemInfo;
        description_itemName.text = $"<color=#00A3FF><b>{item.Name}</b></color>";
        description_itemDescription.text = item.Description;
        description_itemIcon.sprite = item.icon;
        description_itemIcon.transform.localScale = item.iconScaleMultiplier * 0.75f;

        selectedIndicatorTransform.position = selectedItem.transform.position;

        // Setting description values
        costStat.text = $"- {item.cost} Gold";
        healthStat.text = $"- {item.monsterStats.health} HP";
        movementSpeedStat.text = $"- {item.monsterStats.moveSpeed}";
        damageStat.text = $"- {item.monsterStats.baseDamage}";
        attackSpeedStat.text = $"- {item.monsterStats.attackSpeed}";
    }

    #if UNITY_EDITOR
    [ContextMenu("Build UI")]
    public void BuildUI()
    {
        // Destory previous children. Might have a better way of doing it.
        List<Transform> children = new List<Transform>();
        foreach (Transform child in itemSelectionParent)
            children.Add(child);
        for (int i = 0; i < children.Count; i++)
        {
            if (children[i].GetComponent<ItemSelection>() != null)
                DestroyImmediate(children[i].gameObject);
        }

        foreach (var item in itemLibrary.items)
        {
            GameObject itemCard = (GameObject)PrefabUtility.InstantiatePrefab(itemCardPrefab);
            itemCard.transform.SetParent(itemSelectionParent, false);
            itemCard.name = "Card " + item.Name;
            UnityEventTools.AddObjectPersistentListener(itemCard.GetComponent<Button>().onClick, OnSelectItem, itemCard);
            itemCard.GetComponentInChildren<TextMeshProUGUI>().text = item.Name;
            Image icon = itemCard.GetComponentsInChildren<Image>()[1];
            icon.sprite = item.icon;
            icon.transform.localScale = new Vector3(item.iconScaleMultiplier.x, item.iconScaleMultiplier.y, 1);
            itemCard.GetComponent<ItemSelection>().Init(item);
        }
    }
    #endif
}
