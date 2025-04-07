using System;
using Data;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class Player : MonoBehaviour
    {
        public StartingDeckData startingDeckData;
        
        [SerializeField] private Hand hand;
        [SerializeField] private Deck deck;

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

        public void Initialize()
        {
            deck.Initialize(this, startingDeckData);
            hand.Initialize(this);

            Xp = 0;
            MaximumMana = 1;
            Mana = _maximumMana;
        }

        public void DrawCard()
        {
            var card = deck.DrawCard();
            hand.AddCard(card);
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
        
    }
}