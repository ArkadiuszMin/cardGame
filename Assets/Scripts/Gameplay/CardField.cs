using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class CardField : MonoBehaviour
    {
        public TMP_Text Value;
        private int _maxValue;
        private int _currentValue;

        public void Initialize(int value)
        {
            Value.text = value.ToString();
            _maxValue = value;
            _currentValue = value;
        }

        public void UpdateDisplay(bool isVisible)
        {
            this.gameObject.SetActive(isVisible);
        }
    }
}