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
        public TMP_Text playerName;
        public TMP_Text manaText;
        public TMP_Text hpText;
        public Image hpFillImage;
        public Image manaFillImage;
        private PlayerStatus _playerStatus;
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

        public void OnDamage(int demage)
        {
            _curHp -= demage;

            if (_curHp <= 0)
            {
                _curHp = 0;
                ActionQueue.Add(() => GameManager.Instance.EndGame(_playerStatus));
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
            if (_maxMana == 0)
                manaFillImage.fillAmount = 0;
            else
                manaFillImage.fillAmount = (float)_curMana / _maxMana;

            hpText.text = $"{_curHp}/{_maxHp}";
            if (_maxHp == 0)
                hpFillImage.fillAmount = 0;
            else
                hpFillImage.fillAmount = (float)_curHp / _maxHp;
        }

        public void Initialize(int maximumMana, PlayerStatus playerStatus)
        {
            _maxMana = maximumMana;
            _curMana = _maxMana;
            _maxHp = 20;
            _curHp = 20;
            ActionQueue = new List<Action>();
            _playerStatus = playerStatus;
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

        public PlayerStatus getPlayerStatus()
        {
            return _playerStatus;
        }

    }
}