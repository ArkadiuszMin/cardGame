using UnityEngine;

namespace UI
{
    public class EndGameButton : MonoBehaviour
    {
        public void OnClick()
        {
            Application.Quit();
        }
    }
}