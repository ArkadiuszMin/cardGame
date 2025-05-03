using Data;
using DG.Tweening;
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
        private CardStatus _status;
        private Canvas _canvas;
        private Color _deafultBackgroundColor;
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
            _status = status;
            
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
            Debug.Log(_status.ToString());
            if (_status != CardStatus.InHand) return;

            transform.DOScale(Vector3.one * 0.01f * 2f, 0.4f);
            transform.DOLocalMoveZ(1.2f, 0.4f);
            _canvas.sortingOrder = 1;
        }

        public void Unhighlight(){
            if (_status != CardStatus.InHand) return;

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
            _owner.Select(this);
        }

        public void Select(){
            cardBackground.color = Color.yellow;
        }

        public void Unselect(){
            cardBackground.color = _deafultBackgroundColor;
        }

        public void SetVisibility(bool isVisible)
        {
            CardAttackText.UpdateDisplay(isVisible);
            CardManaText.UpdateDisplay(isVisible);
            CardHealthText.UpdateDisplay(isVisible);
            cardBack.SetActive(!isVisible);
        }

        public bool isOnBoard()
        {
            return _status == CardStatus.InGame;
        }
    }

    public enum CardStatus
    {
        InGame, InHand, InDeck, InGraveyard, Removed
    }
}