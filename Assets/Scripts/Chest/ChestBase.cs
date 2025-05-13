using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBase : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.Z;
    public Animator animator;
    public string triggerOpen = "Open";

    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = 0.2f;
    public Ease tweenEase = Ease.OutBack;
    private float startScale;

    private bool chestOpened = false;

    [Space]
    public ChestItemBase chestItem;

    private void Start()
    {
        startScale = notification.transform.localScale.x;
        HideNotification();
    }

    private void Update()
    {
        if(Input.GetKeyDown(keyCode) && notification.activeSelf)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        if (chestOpened) return;
        animator.SetTrigger(triggerOpen);
        chestOpened = true;
        HideNotification();
        Invoke(nameof(ShowItem), 1f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), 1f);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }

    public void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if(p != null)
        {
            if (chestOpened) return;
            ShowNotification();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (p != null)
        {
            HideNotification();
        }
    }

    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale, tweenDuration).SetEase(tweenEase);
    }

    private void HideNotification()
    {
        notification.SetActive(false);
    }
}
