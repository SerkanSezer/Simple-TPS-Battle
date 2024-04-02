using StarterAssets;
using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [HideInInspector] public Transform currentWeapon;

    [SerializeField] ItemSOList itemSOList;
    [SerializeField] Transform weaponHolder;
    [SerializeField] Transform interactPopup;

    public event Action<Item> OnWeaponChange;
    public event Action OnWeaponGet;
    private StarterAssetsInputs starterAssetsInputs;

    private PlayerInventory playerInventory;
    private float getTimer;

    private void Awake() {
        playerInventory = new PlayerInventory();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Start() {
        InitFirstWeaponWorld(itemSOList.list[0].prefab.GetComponent<Item>());
    }
    private void Update() {
        if (starterAssetsInputs.get) {
            getTimer += Time.deltaTime;
            if (getTimer > 0.1f) {
                getTimer = 0;
                starterAssetsInputs.get = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Item>(out Item item)) {
            interactPopup.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.TryGetComponent<Item>(out Item item)) {
            if (starterAssetsInputs.get) {
                AddWeaponWorld(item);
                Destroy(other.gameObject);
                interactPopup.gameObject.SetActive(false);
                starterAssetsInputs.get = false;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent<Item>(out Item item)) {
            interactPopup.gameObject.SetActive(false);
        }
    }

    private void AddWeaponWorld(Item item) {
        if (!playerInventory.IsItemInPlayerInventory(item)) {
            var tempItem = Instantiate(item.GetItemSO().prefab, weaponHolder);
            tempItem.transform.localPosition = Vector3.zero;
            tempItem.GetComponent<Rigidbody>().useGravity = false;
            tempItem.GetComponent<Rigidbody>().isKinematic = true;
            playerInventory.AddItemToInventory(tempItem.GetComponent<Item>(),item.GetItemType());
            OnWeaponGet?.Invoke();
            tempItem.GetComponent<Item>().SetBulletCount(item.GetItemSO().bulletCapacity);
            tempItem.GetComponent<Item>().UpdateBulletCount();
        }
        else {
            playerInventory.AddItemToInventory(item,item.GetItemType());
            OnWeaponGet?.Invoke();
            
        }
        DeactivateAllWeaponWorld(weaponHolder);
    }

    private void InitFirstWeaponWorld(Item item) {
        
        var tempItem = Instantiate(item.GetItemSO().prefab, weaponHolder);
        tempItem.transform.localPosition = Vector3.zero;
        tempItem.GetComponent<Rigidbody>().useGravity = false;
        tempItem.GetComponent<Rigidbody>().isKinematic = true;
        playerInventory.AddItemToInventory(tempItem.GetComponent<Item>(), item.GetItemType());
        currentWeapon = tempItem.transform;
        OnWeaponGet?.Invoke();
        OnWeaponChange?.Invoke(tempItem.GetComponent<Item>());
        tempItem.GetComponent<Item>().SetBulletCount(item.GetItemSO().bulletCapacity);
        tempItem.GetComponent<Item>().UpdateBulletCount();
    }

    public void SetWeapon(Item item) {
        currentWeapon = item.transform;
        item.transform.gameObject.SetActive(true);
        OnWeaponChange?.Invoke(item);
        DeactivateAllWeaponWorld(weaponHolder);
        starterAssetsInputs.select = true;
    }

    private void DeactivateAllWeaponWorld(Transform weaponHolder) {
        foreach (Transform child in weaponHolder) {
            if (child != currentWeapon) {
                child.gameObject.SetActive(false);
            }
        }
    }

    public PlayerInventory GetPlayerInventory() {
        return playerInventory;
    }
    

}
