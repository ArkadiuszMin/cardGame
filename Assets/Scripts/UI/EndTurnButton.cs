using Event;
using UnityEngine;

namespace UI
{
    public class EndTurnButton : MonoBehaviour
    {
        public void OnClick()
        {
            var context = GameContext.Instance;
            context.ChangeTurn();
            GameEvents.PlayerEvents.newTurnStarted.Invoke(context.PlayersTurn);
        }
    }
}