using System;
using System.Linq;
using UnityEngine;

namespace Gameplay.Cards
{
    public static class CardVisibilityService
    {
        public static bool GetCardVisibility(CardStatus status, PlayerStatus playerStatus)
        {
            return playerStatus switch
            {
                PlayerStatus.Me => GetVisibilityForMe(status),
                PlayerStatus.Opponent => GetVisibilityForOpponent(status),
                _ => throw new ArgumentOutOfRangeException(nameof(playerStatus), playerStatus, null)
            };
        }

        private static bool GetVisibilityForMe(CardStatus status)
        {
            var visibleStatues = new [] {CardStatus.InHand, CardStatus.InGame};

            return visibleStatues.Contains(status);
        }

        private static bool GetVisibilityForOpponent(CardStatus status)
        {
            var visibleStatues = new [] {CardStatus.InHand, CardStatus.InGame};

            return visibleStatues.Contains(status);
        }
    }
}