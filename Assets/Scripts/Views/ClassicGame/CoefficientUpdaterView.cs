using UnityEngine;
using UnityEngine.UI;

namespace Views.ClassicGame
{
    public class CoefficientUpdaterView : MonoBehaviour
    {
        [SerializeField] 
        private Text _text;

        public void UpdateText(float coeff)
        {
            _text.text = $"{coeff:F1}x";
        }
    }
}