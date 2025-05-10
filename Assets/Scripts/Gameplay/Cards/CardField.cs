using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Gameplay.Cards
{
    public class CardField : MonoBehaviour
    {
        public TMP_Text Text;
        private int _maxValue;
        private int _currentValue;

        public void Initialize(int value)
        {
            Text.text = value.ToString();
            _maxValue = value;
            _currentValue = value;
        }

        public void UpdateDisplay(bool isVisible)
        {
            this.gameObject.SetActive(isVisible);
        }

        public void ChangeValue(int newValue)
        {
            // if (newValue == _currentValue) return;
            
            ChangeValueAnimation(newValue);
            _currentValue = newValue;
            SetTextColor();
        }

        private void SetTextColor()
        {
            if (_currentValue > _maxValue)
            {
                Text.color = Color.green;
            }
            else if (_currentValue == _maxValue)
            {
                Text.color = Color.white;
            }
            else
            {
                Text.color = Color.red;
            }
        }

        private void ChangeValueAnimation(int newValue)
        {
            var color = newValue < _currentValue ? Color.red : Color.green;
            
            var seq = DOTween.Sequence();
            seq.Append(Text.transform.DOScale(Vector3.one * 4f, 0.2f))
                .AppendCallback(() =>
                {
                    Text.text = newValue.ToString();
                    Text.color = color;
                })
                .Append(Text.transform.DOScale(Vector3.one, 0.2f));
        }
    }
}