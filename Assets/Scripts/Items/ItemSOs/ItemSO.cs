using UnityEngine;

[CreateAssetMenu(fileName ="ItemSO",menuName ="ScriptableObjects/ItemSO",order =1)]
public class ItemSO : ScriptableObject
{

    public Transform prefab;
    public Sprite itemIcon;
    public int bulletCapacity;
    public string nameString;
    public float WEAPON_TIMER_MAX;
}
