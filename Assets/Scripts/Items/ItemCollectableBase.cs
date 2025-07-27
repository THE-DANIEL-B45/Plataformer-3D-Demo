using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itens
{
    public class ItemCollectableBase : MonoBehaviour
    {
        public ItemType itemType;

        public string compareTag = "Player";
        public ParticleSystem myParticleSystem;
        public float timeToHide = 3f;
        public GameObject graphicItem;
        bool once;

        public Collider myCollider;

        [Header("Sounds")]
        public AudioSource audioSource;
        public SFXType sfxType;

        private void Awake()
        {
            //if (myParticleSystem != null) myParticleSystem.transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag) && !once)
            {
                once = true;
                Collect();
            }
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        }

        protected virtual void Collect()
        {
            PlaySFX();
            if(myCollider != null) myCollider.enabled = false;
            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke(nameof(HideObject), timeToHide);
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (myParticleSystem != null)
            {
                myParticleSystem.Play();
            }
            if (audioSource != null)
            {
                audioSource.Play();
            }
            ItemManager.Instance.AddByType(itemType);
        }
    }
}