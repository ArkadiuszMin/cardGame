using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Cards
{
    public static class CardVisibilityService
    {
        public static Dictionary<CardStatus, CardVisibility> VisibilityMap =
            new Dictionary<CardStatus, CardVisibility>()
            {
                { CardStatus.InDeck, new CardVisibility(false, false) },
                { CardStatus.InHand, new CardVisibility(true, true) },
                { CardStatus.InGame, new CardVisibility(true, true) },
                { CardStatus.InGraveyard, new CardVisibility(true, false) }
            };
        public static CardVisibility GetCardVisibility(CardStatus status, PlayerStatus playerStatus)
        {
            return VisibilityMap[status];
        }

        private static bool GetVisibilityForMe(CardStatus status)
        {
            var visibleStatues = new [] {CardStatus.InHand, CardStatus.InGame, CardStatus.InGraveyard};

            return visibleStatues.Contains(status);
        }

        private static bool GetVisibilityForOpponent(CardStatus status)
        {
            var visibleStatues = new [] {CardStatus.InHand, CardStatus.InGame, CardStatus.InGraveyard};

            return visibleStatues.Contains(status);
        }
    }

    public record CardVisibility
    {
        public bool IsCardVisible;
        public bool AreStatsVisible;
        public CardVisibility(bool isCardVisible, bool areStatsVisible)
        {
            IsCardVisible = isCardVisible;
            AreStatsVisible = areStatsVisible;
        }
    };
}