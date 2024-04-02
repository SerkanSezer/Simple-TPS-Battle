using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform itemTemplateUI;
    [SerializeField] private PlayerInteract playerInteract;

    private void Awake() {
        playerInteract.OnWeaponGet += PlayerInteract_OnWeaponGet;
    }

    private void PlayerInteract_OnWeaponGet() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        foreach (var item in playerInteract.GetPlayerInventory().GetInventoryList()) {
            var itemUI = Instantiate(itemTemplateUI, transform);
            item.Key.SetItemUI(itemUI.GetComponent<ItemTemplateUI>());
            itemUI.GetComponent<ItemTemplateUI>().GetImageComponent().sprite = item.Key.GetItemSO().itemIcon;
            itemUI.GetComponent<ItemTemplateUI>().GetMagCountComponent().SetText(item.Value.ToString());
            itemUI.GetComponent<ItemTemplateUI>().GetBulletCountComponent().SetText(item.Key.bulletCount.ToString());
            if (item.Key.itemStatus == ItemStatus.NotExist) {
                itemUI.GetComponent<ItemTemplateUI>().GetImageComponent().color = new Color(1, 1, 1, 0.25f);
                itemUI.GetComponent<ItemTemplateUI>().GetItemButton().interactable = false;
            }
            else {
                itemUI.GetComponent<ItemTemplateUI>().GetItemButton().onClick.AddListener(() => {
                    playerInteract.SetWeapon(item.Key);
                });
            }
        }
    }
}
