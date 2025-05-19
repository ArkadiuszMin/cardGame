using System;
using Data;
using DG.Tweening;
using Event;
using Gameplay.Cards;
using UI;
using Unity.Mathematics;
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
        [SerializeField] private PlayerStatusUI stats;

        public PlayerStatus PlayerStatus { get; private set; }

        public int Xp { get; set; }
        public int Mana { get; set; }
        public int MaximumMana { get; set; }

        private Card _highlightedCard;
        private Card _selectedCard;

        public void Initialize(PlayerStatus playerStatus)
        {
            PlayerStatus = playerStatus;
            Xp = 0;
            MaximumMana = 1;
            Mana = MaximumMana;
            GameEvents.PlayerEvents.newTurnStarted += OnTurnStarted;
            
            deck.Initialize(this, startingDeckData);
            hand.Initialize(this);
            board.Initialize(this);
            graveyard.Initialize(this);
            stats.Initialize(MaximumMana);
        }


        private void Update()
        {
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
            stats.OnManaUse(card.ManaCost);
            GameEvents.CardEvents.cardPlayed.Invoke(card);
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

        private void RefreshMana()
        {
            MaximumMana = math.min(MaximumMana + 1, 10);
            Mana = MaximumMana;
            stats.RefreshMana(MaximumMana);
        }

        private void OnTurnStarted(PlayerStatus playerTurn)
        {
            if (playerTurn != PlayerStatus) return;
            DrawCard();
            RefreshMana();
            board.RefreshCreatures();
        }
    }
}