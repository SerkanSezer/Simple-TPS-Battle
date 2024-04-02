using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemTemplateUI itemTemplate;
    [SerializeField] ItemSO itemSO;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] ParticleSystem muzzleFX;
    public ItemType itemType;
    public ItemStatus itemStatus;
    public int magCount;
    public int bulletCount;
    public ItemType GetItemType() {
        return itemType;
    }
    public ItemSO GetItemSO() {
        return itemSO;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

    public ParticleSystem GetMuzzleFX() {
        return muzzleFX;
    }
    public void SetBulletCount(int bulletCount) {
        this.bulletCount = bulletCount;
    }
    public void SetMagCount(int magCount) {
        this.magCount = magCount;
    }
    public void UpdateBulletCount() {
        itemTemplate.GetBulletCountComponent().SetText(this.bulletCount.ToString());
    }
    public void UpdateMagCount() {
        itemTemplate.GetMagCountComponent().SetText(this.magCount.ToString());
    }

    public int GetBulletCount() {
        return bulletCount;
    }
    public int GetMagCount() {
        return magCount;
    }
    public int GetBulletCapacity() {
        return itemSO.bulletCapacity;
    }
    public void SetItemUI(ItemTemplateUI itemTemplateUI) {
        itemTemplate = itemTemplateUI;
    }
}
public enum ItemType {
    Rifle,
    Magazine,
    Medkit
}
public enum ItemStatus {
    Exist,
    NotExist
}
