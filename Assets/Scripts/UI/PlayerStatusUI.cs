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
        public TMP_Text hpText;
        public Image hpFillImage;
        public Image manaFillImage;
    }
}