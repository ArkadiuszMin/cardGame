using Event;
using UnityEngine;

namespace UI
{
    public class StartGameButton : MonoBehaviour
    {
        public void OnClick()
        {
            GameManager.Instance.StartGame();
        }
    }
}