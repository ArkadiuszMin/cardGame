using System.Collections;
using System.Collections.Generic;
using Data;
using DG.Tweening;
using Event;
using Interface;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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

        private int _health;
        public int _attack;
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _deafultBackgroundColor = cardBackground.color;
        }

        private ICardHandler _cardHandler;
        public void SetData(CardData data)
        {
            _cardData = data;
            cardNameText.text = data.cardName;
            _health = data.maxHealth;
            _attack = data.attack;
            
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

        public void SetVisibility(bool isVisible)
        {
            CardAttackText.UpdateDisplay(isVisible);
            CardManaText.UpdateDisplay(isVisible);
            CardHealthText.UpdateDisplay(isVisible);
            cardBack.SetActive(!isVisible);
        }

        public void TakeDamage(int dmg)
        {
            _health -= dmg;
            CardHealthText.ChangeValue(_health);
        }

        public bool isInHand()
        {
            return Status == CardStatus.InHand;
        }
        
        public IEnumerator AnimateAttack(Vector3 targetPosition, float duration)
        {
            Vector3 originalPosition = transform.position;

            float elapsed = 0;
            while (elapsed < duration / 2f)
            {
                transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsed / (duration / 2f));
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPosition;
            yield return new WaitForSeconds(0.1f);

            elapsed = 0;
            while (elapsed < duration / 2f)
            {
                transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsed / (duration / 2f));
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.position = originalPosition;
        }
    }

    public enum CardStatus
    {
        InGame, InHand, InDeck, InGraveyard, Removed
    }
}