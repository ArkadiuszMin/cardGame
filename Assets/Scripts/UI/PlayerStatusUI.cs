using System;
using Gameplay;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Event;
using Gameplay.Cards;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace UI
{
    public class PlayerStatusUI : MonoBehaviour, IPointerClickHandler
    {
        public TMP_Text manaText;
        public TMP_Text hpText;
        public Image hpFillImage;
        public Image manaFillImage;
        public PlayerStatus playerStatus;
        public GameManager gameManager;
        private List<Action> ActionQueue;

        private int _maxMana;
        private int _curMana;

        private int _curHp;
        private int _maxHp;

        public void RefreshMana(int maxMana)
        {
            _maxMana = maxMana;
            _curMana = _maxMana;

            RefreshUI();
        }

        public void onDamage(int demage)
        {
            _curHp -= demage;

            if (_curHp <= 0)
            {
                _curHp = 0;
                ActionQueue.Add(() => gameManager.EndGame(playerStatus));
            }

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
            manaFillImage.fillAmount = (float)_curMana / _maxMana;

            hpText.text = $"{_curHp}/{_maxHp}";
            hpFillImage.fillAmount = (float)_curHp / _maxHp;
        }

        public void Initialize(int maximumMana, PlayerStatus playerStatu)
        {
            _maxMana = maximumMana;
            _curMana = _maxMana;
            _maxHp = 20;
            _curHp = 20;
            playerStatus = playerStatu;
            ActionQueue = new List<Action>();
            RefreshUI();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameEvents.PlayerEvents.playerClicked?.Invoke(this);
        }
        
        public void ExecuteActions()
        {
            foreach (var action in ActionQueue)
            {
                action.Invoke();
            }
            
            ActionQueue.Clear();
        }
    }
}