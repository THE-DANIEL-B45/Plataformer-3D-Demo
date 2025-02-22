using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Itens
{
    public class ItemLayout : MonoBehaviour
    {
        private ItemSetup _currSetup;
        public Image uiIcon;
        public Image keyIcon;
        public TextMeshProUGUI uiValue;

        public void Load(ItemSetup setup)
        {
            _currSetup = setup;
            UpdateUI();
        }

        private void UpdateUI()
        {
            uiIcon.sprite = _currSetup.icon;
            if(_currSetup.keyIcon != null)keyIcon.sprite = _currSetup.keyIcon;
            else keyIcon.gameObject.SetActive(false);
        }

        private void Update()
        {
            uiValue.text = _currSetup.soInt.value.ToString();
        }
    }
}
