using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class ItemCollectableCoin : ItemCollectableBase
{
    public int amount;
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddByType(ItemType.COIN);
    }
}
