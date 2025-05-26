using System.Collections.Generic;
using Data;
using UnityEngine;
using Extensions;
using System;
using Gameplay.Cards;
using Interface;

namespace Gameplay
{
    public class Deck : MonoBehaviour, ICardHandler
    {
        private List<Card> _cards = new List<Card>();
        private Player _owner;
        public void Initialize(Player owner, StartingDeckData startingDeckData)
        {
            _owner = owner;
            foreach (var card in startingDeckData.GetCards())
            {
                CreateCard(card);
            }

            Shuffle();
        }

        private void CreateCard(CardData cardData)
        {
            var card = cardData.Create();
            card.SetOwner(_owner);
            CardStatusChanger.Change(card, CardStatus.InDeck, this);
            _cards.Add(card);
        }

        private void Shuffle()
        {
            _cards.Shuffle();
        }

        public Vector3 GetPosition(Card card)
        {
            return Vector3.zero;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public Card DrawCard()
        {
            if (_cards.Count > 0)
            {
                var card = _cards[0];
                _cards.RemoveAt(0);
                return card;
            }

            return null;
        }
        
        public void disableCards()
        {
            foreach (var card in _cards)
            {
                card.enabled = false;
            }
        }
    }
}