using System;
using Data;
using DG.Tweening;
using Event;
using Gameplay.Cards;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class Player : MonoBehaviour
    {
        public StartingDeckData startingDeckData;
        
        [SerializeField] private Hand hand;
        [SerializeField] private Deck deck;
        [SerializeField] private Board board;
        [SerializeField] private Graveyard graveyard;

        public PlayerStatus PlayerStatus { get; private set; }

        public int Xp { get; set; }
        public int Mana { get; set; }
        public int MaximumMana { get; set; }

        private Card _highlightedCard;
        private Card _selectedCard;

        public void Initialize(PlayerStatus playerStatus)
        {
            deck.Initialize(this, startingDeckData);
            hand.Initialize(this);
            board.Initialize(this);
            graveyard.Initialize(this);

            PlayerStatus = playerStatus;
            Xp = 0;
            MaximumMana = 10;
            Mana = MaximumMana;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DrawCard();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Select(null);
            }
        }

        public void DrawCard()
        {
            var card = deck.DrawCard();
            if (card != null)
                hand.AddCard(card);
        }

        public void PlaySelectedCard(){
            if (_selectedCard && _selectedCard.isInHand()){
                Unhighlight();
                PlayCard(_selectedCard);
                Select(null);
            }
        }

        private void PlayCard(Card card)
        {
            Mana -= card.ManaCost;
            hand.RemoveCard(card);
            board.AddCard(card);
            GameEvents.CardEvents.cardPlayed.Invoke(card);
        }

        public int GetLevel(int xp)
        {
            var levels = new[] { 0, 3, 4, 5, 6 };
            for (int i = 0; i < levels.Length; i++)
            {
                xp -= levels[i];
                if (xp < 0)
                {
                    return i - 1;
                }
            }

            return 0;
        }

        public void Highlight(Card card)
        {
            if(_highlightedCard)
                _highlightedCard.Unhighlight();
            _highlightedCard = card;
            if(_highlightedCard)
                _highlightedCard.Highlight();
        }
        public void Unhighlight()
        {
            Highlight(null);
        }

        public void Select(Card card)
        {
            if(_selectedCard)
                _selectedCard.Unselect();
            _selectedCard = card;
        }
        public void Unselect()
        {
            _selectedCard = null;
        }

        public void PutOnGraveyard(Card card)
        {
            board.RemoveCard(card);
            graveyard.AddCard(card);
            GameEvents.CardEvents.cardDied.Invoke(card);
        }
    }
}