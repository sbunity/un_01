using UnityEngine;
using TMPro;
using Views.General;

namespace Views.ClassicGame
{
    public class WinPanel : PanelView
    {
        [SerializeField] 
        private TextMeshProUGUI _descriptionText;

        public void UpdateDescription(float value)
        {
            _descriptionText.text = $"YOU WON {value:F2} COINS!";
        }
    }
}