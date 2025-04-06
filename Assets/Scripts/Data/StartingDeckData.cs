using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data{
    [CreateAssetMenu]
    public class StartingDeckData : ScriptableObject
    {
        public List<CardDeckData> cards;

        public List<CardData> GetCards()
        {
            var list = new List<CardData>();

            foreach (var cardDeck in cards){
                for (var i = 0; i < cardDeck.count; i++){
                    list.Add(cardDeck.cardData);
                }
            }

            return list;
        }
    }

    [Serializable]
    public class CardDeckData{
        public CardData cardData;
        public int count = 1;
    }
}
