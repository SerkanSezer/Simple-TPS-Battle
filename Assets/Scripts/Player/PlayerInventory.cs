using System.Collections.Generic;

public class PlayerInventory{

    private Dictionary<Item,int> list;
    public PlayerInventory() {
        list = new Dictionary<Item, int>();
    }
    public Dictionary<Item, int> GetInventoryList() {
        return list;
    }

    public void AddItemToInventory(Item item, ItemType itemType) {
        foreach (var tempItem in list) {
            if (tempItem.Key.GetItemSO() == item.GetItemSO()) {
                list[tempItem.Key] += 1;
                tempItem.Key.SetMagCount(list[tempItem.Key]);
                if (tempItem.Key.itemStatus == ItemStatus.NotExist && itemType == ItemType.Rifle) { tempItem.Key.itemStatus = ItemStatus.Exist; }
                return;
            }
        }
        if (item.itemStatus == ItemStatus.NotExist && itemType == ItemType.Rifle) { item.itemStatus = ItemStatus.Exist; }
        list.Add(item, 1);
        item.SetMagCount(1);
    }

    public bool IsItemInPlayerInventory(Item item) {
        foreach (var tempItem in list) {
            if (tempItem.Key.GetItemSO() == item.GetItemSO()) {
                return true;
            }
        }
        return false;
    }

    public void UpdateItemCount(Item item) {
        list[item] = item.GetMagCount();
    }

    public void RemoveItemFromInventory(Item item) {
        list.Remove(item);
    }
}
