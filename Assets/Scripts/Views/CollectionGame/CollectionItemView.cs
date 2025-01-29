using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views.CollectionGame
{
    public class CollectionItemView : MonoBehaviour
    {
        public event Action<CollectionItemView> PressBtnAction;
        
        [SerializeField] 
        private Button _collectBtn;
        [SerializeField] 
        private Image _fiilImage;
        [SerializeField] 
        private GameObject _iconGameObject;
        [SerializeField]
        private Text _countText;

        private void OnEnable()
        {
            _collectBtn.onClick.AddListener(Notification);
        }

        private void OnDisable()
        {
            _collectBtn.onClick.RemoveAllListeners();
        }

        public void SetState(bool value)
        {
            _iconGameObject.SetActive(!value);
            _collectBtn.gameObject.SetActive(value);
        }

        public void SetCount(int value)
        {
            _countText.text = $"{value}/15";
        }

        public void SetFillAmount(float value)
        {
            _fiilImage.fillAmount = value;
        }

        private void Notification()
        {
            PressBtnAction?.Invoke(this);
        }
    }
}