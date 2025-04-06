using Data;
using DG.Tweening;
using Interface;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class Card : MonoBehaviour
    {
        public TMP_Text cardNameText;
        public Image cardSpriteImage;
        private CardData _cardData;
        private Player _owner;
        private CardStatus _status;

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
            transform.SetParent(_cardHandler.GetTransform());

            UpdatePosition();
        }

        public void UpdatePosition()
        {
            var targetPosition = _cardHandler.GetPosition(this);
            transform.DOKill();
            transform.DOLocalMove(targetPosition, 0.4f);
        }
    }

    public enum CardStatus
    {
        InGame, InHand, InDeck, InGraveyard, Removed
    }
}