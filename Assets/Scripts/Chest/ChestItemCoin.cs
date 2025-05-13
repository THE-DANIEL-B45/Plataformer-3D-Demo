using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class ChestItemCoin : ChestItemBase
{
    public int coinNumber = 5;
    public GameObject coinObject;
    private List<GameObject> itens = new List<GameObject>();

    public Vector2 randomRange = new Vector2(-2f, 2f);

    public float tweenEndTime = 0.5f;

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItens();
    }

    private void CreateItens()
    {
        for(int i = 0; i < coinNumber; i++)
        {
            var item = Instantiate(coinObject);
            item.transform.position = transform.position + Vector3.forward * Random.Range(randomRange.x, randomRange.y) + Vector3.right * Random.Range(randomRange.x, randomRange.y);
            item.transform.DOScale(0, 0.2f).SetEase(Ease.OutBack).From();
            itens.Add(item);
        }
    }

    public override void Collect()
    {
        foreach(var i in itens)
        {
            i.transform.DOMoveY(2f, tweenEndTime).SetRelative();
            i.transform.DOScale(0, tweenEndTime / 2).SetDelay(tweenEndTime / 2);
            ItemManager.Instance.AddByType(ItemType.COIN);
        }
    }
}
