using System;
using Data;
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
        public PlayerStatus PlayerStatus { get; private set; }

        public event Action<int, int> ManaChanged;
        public event Action<int, int, int> XpChanged;

        private int _xp;

        public int Xp
        {
            get => _xp;
            set
            {
                _xp = value;
                XpChanged?.Invoke(GetLevel(_xp), _xp, _xp);
            }
        }

        private int _mana;
        public int Mana
        {
            get => _mana;
            set
            {
                _mana = value;
                ManaChanged?.Invoke(_mana, _maximumMana);
            }
        }
        private int _maximumMana;
        public int MaximumMana
        {
            get => _maximumMana;
            set
            {
                _maximumMana = value;
                ManaChanged?.Invoke(_mana, _maximumMana);
            }
        }

        private Card _highlightedCard;
        private Card _selectedCard;

        public void Initialize(PlayerStatus playerStatus)
        {
            deck.Initialize(this, startingDeckData);
            hand.Initialize(this);
            board.Initialize(this);

            PlayerStatus = playerStatus;
            Xp = 0;
            MaximumMana = 1;
            Mana = _maximumMana;
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
            if (_selectedCard){
                Unhighlight();
                PlayCard(_selectedCard);
                Select(null);
            }
        }

        private void PlayCard(Card card){
            hand.RemoveCard(card);
            board.AddCard(card);
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
            if(_selectedCard)
                _selectedCard.Select();
        }
        public void Unselect(Card card)
        {
        }
        
    }
}