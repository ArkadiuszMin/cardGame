using Gameplay.Cards;
using Interface;

namespace Gameplay.Cards
{
    public static class CardStatusChanger
    {
        public static void Change(Card card, CardStatus status, ICardHandler handler)
        {
            card.SetStatus(status, handler);
            var cardVisibility = CardVisibilityService.GetCardVisibility(status, card.GetOwnerStatus());
            card.SetVisibility(cardVisibility);
        }
    }
}