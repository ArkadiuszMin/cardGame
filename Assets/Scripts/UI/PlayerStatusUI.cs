using Gameplay;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace UI
{
    public class PlayerStatusUI : MonoBehaviour
    {
        public TMP_Text manaText;
        public TMP_Text xpText;
        public TMP_Text levelText;
        public Image xpFillImage;

        private Player _player;

        public void LinkToPlayer(Player _player){
            _player.ManaChanged += UpdateManaText;
            _player.XpChanged += UpdateLevel;
        }
        private void UpdateManaText(int mana, int maxMana)
        {
            manaText.text = $"{mana}/{maxMana}";
        }
        private void UpdateLevel(int level, int xp, int nextLevelXp)
        {
            levelText.text = $"Level: {level + 1}";
            xpText.text = $"{xp}/{nextLevelXp}";
            var percent = (float)xp / nextLevelXp;
            xpFillImage.DOKill();
            xpFillImage.DOFillAmount(percent, 0.4f);
        }
    }
}