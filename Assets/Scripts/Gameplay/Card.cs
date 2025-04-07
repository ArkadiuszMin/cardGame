using Data;
using DG.Tweening;
using Interface;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public TMP_Text cardNameText;
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
            cardSpriteImage.sprite = data.cardSprite;
        }

        public void SetOwner(Player owner)
        {
            _owner = owner;
            transform.SetParent(_owner.transform);
        }

        public void SetStatus(CardStatus status, ICardHandler parent)
        {
            _cardHandler = parent;
            _status = status;

            cardBack.SetActive(_status == CardStatus.InDeck);

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
            Debug.Log("Pointer Enter");
            _owner.Highlight(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Pointer Exit");
            _owner.Unhighlight();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Click");
            _owner.Select(this);
        }

        public void Select(){
            cardBackground.color = Color.yellow;
        }

        public void Unselect(){
            cardBackground.color = _deafultBackgroundColor;
        }
    }

    public enum CardStatus
    {
        InGame, InHand, InDeck, InGraveyard, Removed
    }
}