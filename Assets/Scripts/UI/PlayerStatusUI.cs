using System;
using Gameplay;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Event;
using Gameplay.Cards;

namespace UI
{
    public class PlayerStatusUI : MonoBehaviour
    {
        public TMP_Text manaText;
        public TMP_Text hpText;
        public Image hpFillImage;
        public Image manaFillImage;

        private int _maxMana;
        private int _curMana;

        public void RefreshMana(int maxMana)
        {
            _maxMana = maxMana;
            _curMana = _maxMana;
            
            RefreshUI();
        }

        public void OnManaUse(int manaUsed)
        {
            _curMana -= manaUsed;
            RefreshUI();
        }

        private void RefreshUI()
        {
            manaText.text = $"{_curMana}/{_maxMana}";
            manaFillImage.fillAmount = (float) _curMana / _maxMana;
        }

        public void Initialize(int maximumMana)
        {
            _maxMana = maximumMana;
            _curMana = _maxMana;
            RefreshUI();
        }
    }
}