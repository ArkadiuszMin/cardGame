using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using DG.Tweening;
using Event;
using Interface;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Gameplay.Cards
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public TMP_Text cardNameText;
        public CardField CardAttackText;
        public CardField CardManaText;
        public CardField CardHealthText;
        
        public Image cardSpriteImage;
        public GameObject cardBack;
        public Image cardBackground;
        private CardData _cardData;
        private Player _owner;
        [HideInInspector] public CardStatus Status { get;  private set; }
        private Canvas _canvas;
        private Color _deafultBackgroundColor;

        private List<Action> ActionQueue;

        private int _health;
        public int _attack;
        public int ManaCost;
        
        public bool CanAttack = false;
        
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            ActionQueue = new List<Action>();
            _deafultBackgroundColor = cardBackground.color;
        }

        private ICardHandler _cardHandler;
        public void SetData(CardData data)
        {
            _cardData = data;
            cardNameText.text = data.cardName;
            _health = data.maxHealth;
            _attack = data.attack;
            ManaCost = data.manaCost;
            
            CardAttackText.Initialize(data.attack);
            CardManaText.Initialize(data.manaCost);
            CardHealthText.Initialize(data.maxHealth);
            cardSpriteImage.sprite = data.cardSprite;
        }

        public void SetOwner(Player owner)
        {
            _owner = owner;
            transform.SetParent(_owner.transform);
        }

        public PlayerStatus GetOwnerStatus()
        {
            return _owner.PlayerStatus;
        }

        public void SetStatus(CardStatus status, ICardHandler parent)
        {
            _cardHandler = parent;
            Status = status;
            
            transform.DOComplete();
            transform.localScale = Vector3.one * 0.01f;
            transform.SetParent(_cardHandler.GetTransform());

            UpdatePosition();
        }

        public void UpdatePosition()
        {
            var targetPosition = _cardHandler.GetPosition(this);
            transform.DOLocalMove(targetPosition, 0.4f);
        }

        public void Highlight(){
            if (Status != CardStatus.InHand) return;

            transform.DOScale(Vector3.one * 0.01f * 2f, 0.4f);
            transform.DOLocalMoveZ(1.2f, 0.4f);
            _canvas.sortingOrder = 1;
        }

        public void Unhighlight(){
            if (Status != CardStatus.InHand) return;

            transform.DOScale(Vector3.one * 0.01f, 0.4f);
            transform.DOLocalMoveZ(0f, 0.4f);
            _canvas.sortingOrder = 0;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Highlight();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Unhighlight();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameEvents.CardEvents.cardClicked?.Invoke(this);
        }

        public void Select(){
            _owner.Select(this);
            cardBackground.color = Color.yellow;
        }

        public void Unselect(){
            _owner.Unselect();
            cardBackground.color = _deafultBackgroundColor;
        }

        public void SetVisibility(CardVisibility visibility)
        {
            CardAttackText.UpdateDisplay(visibility.AreStatsVisible);
            CardManaText.UpdateDisplay(visibility.AreStatsVisible);
            CardHealthText.UpdateDisplay(visibility.AreStatsVisible);
            cardBack.SetActive(!visibility.IsCardVisible);
        }

        public void TakeDamage(int dmg)
        {
            _health -= dmg;
            CardHealthText.ChangeValue(_health);

            if (_health <= 0)
            {
                ActionQueue.Add(() => _owner.PutOnGraveyard(this));
            }
        }

        public void ExecuteActions()
        {
            foreach (var action in ActionQueue)
            {
                action.Invoke();
            }
            
            ActionQueue.Clear();
        }

        public bool isInHand()
        {
            return Status == CardStatus.InHand;
        }

        public bool CanBeSelected()
        {
            bool canAttack = Status == CardStatus.InGame && CanAttack;
            bool canBePlayed = Status == CardStatus.InHand && ManaCost <= _owner.Mana;

            return canBePlayed || canAttack;
        }
    }

    public enum CardStatus
    {
        InGame, InHand, InDeck, InGraveyard, Removed
    }
}