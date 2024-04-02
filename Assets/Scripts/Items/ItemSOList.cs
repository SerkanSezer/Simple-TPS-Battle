using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSOList", menuName = "ScriptableObjects/ItemSOList", order = 2)]
public class ItemSOList : ScriptableObject
{
    public List<ItemSO> list;
}
