using System;
using Data;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class Player : MonoBehaviour
    {
        public StartingDeckData startingDeckData;
        [SerializeField]
        private Hand hand;
        [SerializeField]
        private Deck deck;

        private int _xp;

        private int _mana;
        private int _maximumMana;

        public void Initialize()
        {
            deck.Initialize(this, startingDeckData);
            hand.Initialize(this);

            _xp = 0;
            _maximumMana = 1;
            _mana = _maximumMana;
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