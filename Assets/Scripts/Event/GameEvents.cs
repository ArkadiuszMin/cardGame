using System;
using Gameplay.Cards;

namespace Event
{
    public static class GameEvents
    {
        public static class CardEvents
        {
            public static Action<Card> cardClicked;
            public static Action<Card> cardPlayed;    
        }
    }
}