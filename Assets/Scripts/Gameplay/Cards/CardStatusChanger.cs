using Gameplay.Cards;
using Interface;

namespace Gameplay.Cards
{
    public static class CardStatusChanger
    {
        public static void Change(Card card, CardStatus status, PlayerStatus playerStatus, ICardHandler handler)
        {
            card.SetStatus(status, handler);
            var cardVisibility = CardVisibilityService.GetCardVisibility(status, playerStatus);
            card.SetVisibility(cardVisibility);
        }
    }
}