using UnityEngine;
using UnityEngine.UI;

namespace Views.CollectGame
{
    public class PrizeUpdaterView : MonoBehaviour
    {
        [SerializeField] 
        private Text _text;

        public void UpdateText(int value)
        {
            _text.text = value > 0 ? $"+{value}" : $"{value}";
        }
    }
}