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
        public Player player;
        public TMP_Text manaText;
        public TMP_Text hpText;
        public Image hpFillImage;
        public Image manaFillImage;

        private void Awake()
        {
            GameEvents.CardEvents.cardPlayed += OnCardPlayed;
        }

        public void OnCardPlayed(Card card)
        {
            if (card.GetOwnerStatus() != player.PlayerStatus) return;

            var curMana = player.Mana;
            var maxMana = player.MaximumMana;

            manaText.text = $"{curMana}/{maxMana}";
            Debug.Log(curMana);
            Debug.Log(maxMana);
            Debug.Log(curMana / maxMana);
            
            manaFillImage.fillAmount = (float) curMana /  maxMana;
        }
    }
}