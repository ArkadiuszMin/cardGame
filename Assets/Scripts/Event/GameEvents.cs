using System;
using Gameplay.Cards;
using UI;

namespace Event
{
    public static class GameEvents
    {
        public static class CardEvents
        {
            public static Action<Card> cardClicked;
            public static Action<Card> cardPlayed;
            public static Action<Card> cardDied;
        }

        public static class PlayerEvents
        {
            public static Action<PlayerStatus> newTurnStarted;
            public static Action<PlayerStatusUI> playerClicked;
        }
    }
}