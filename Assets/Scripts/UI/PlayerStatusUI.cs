using Gameplay;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace UI
{
    public class PlayerStatusUI : MonoBehaviour
    {
        public Player player;
        public TMP_Text manaText;
        public TMP_Text xpText;
        public TMP_Text levelText;
        public Image xpFillImage;

        public void OnEnable(){
            player.ManaChanged += UpdateManaText;
            player.XpChanged += UpdateLevel;
        }

        public void OnDisable(){
            player.ManaChanged -= UpdateManaText;
            player.XpChanged -= UpdateLevel;
        }
        private void UpdateManaText(int mana, int maxMana)
        {
            manaText.text = $"{mana}/{maxMana}";
        }
        private void UpdateLevel(int level, int xp, int nextLevelXp)
        {
            levelText.text = $"Level: {level + 1}";
            xpText.text = $"{xp}/{nextLevelXp}";
            var percent = 0f;

            if (nextLevelXp != 0)
                percent = (float)xp / nextLevelXp;
            else
                percent = (float)xp;
                
            xpFillImage.DOKill();
            xpFillImage.DOFillAmount(percent, 0.4f);
        }
    }
}