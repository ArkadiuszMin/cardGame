using System;
using System.Collections.Generic;
using Gameplay.Cards;
using Interface;
using UnityEngine;

namespace Gameplay
{
    public class Hand : MonoBehaviour, ICardHandler
    {
        public int cardSize;
        public float cardInterval;
        private List<Card> _cards = new List<Card>();
        private Player _owner;
        public void Initialize(Player owner)
        {
            _owner = owner;
        }

        public void AddCard(Card card)
        {
            CardStatusChanger.Change(card, CardStatus.InHand, _owner.PlayerStatus, this);
            _cards.Add(card);
            UpdateHandPosition();
        }

        private void UpdateHandPosition()
        {
            foreach (var c in _cards)
            {
                c.UpdatePosition();
            }
        }

        public Vector3 GetPosition(Card card)
        {
            var count = _cards.Count;
            var index = _cards.IndexOf(card);

            var handSize = count * cardSize + (count - 1) * cardInterval;
            var positionX = (cardSize - handSize) / 2f;
            
            return new Vector3(positionX + (cardSize + cardInterval)*index, 0f, 0f);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
            UpdateHandPosition();
        }
    }
}