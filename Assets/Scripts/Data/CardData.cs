using Gameplay;
using Gameplay.Cards;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public Sprite cardSprite;
        private Card _cardPrefab;
        public int manaCost;
        public int level;
        public CardType CardType;
        public int attack;
        public int maxHealth;
        public Card CardPrefab
        {
            get
            {
                if (_cardPrefab == null){
                    _cardPrefab = Resources.Load<Card>("CardPrefab");
                }

                return _cardPrefab;
            }
        }

        public Card Create()
        {
            var card = Instantiate(CardPrefab);
            card.SetData(this);
            return card;
        }
    }

    public enum CardType
    {
        Creature, Spell
    }
}