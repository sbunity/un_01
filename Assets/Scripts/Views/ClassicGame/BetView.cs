using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views.ClassicGame
{
    public class BetView : MonoBehaviour
    {
        public event Action<bool> PressBtnAction;

        [SerializeField] 
        private Button _minusBtn;
        [SerializeField] 
        private Button _plusBtn;
        [SerializeField]
        private Text _countText;

        private void OnEnable()
        {
            _minusBtn.onClick.AddListener(OnPressMinusBtn);
            _plusBtn.onClick.AddListener(OnPressPlusBtn);
        }

        private void OnDisable()
        {
            _minusBtn.onClick.RemoveAllListeners();
            _plusBtn.onClick.RemoveAllListeners();
        }

        public void UpdateCount(int value)
        {
            _countText.text = $"{value}.00";
        }

        public void SetActiveMinusBtn(bool value)
        {
            _minusBtn.interactable = value;
        }

        public void SetActivePlusBtn(bool value)
        {
            _plusBtn.interactable = value;
        }

        public void HideButtons()
        {
            _minusBtn.gameObject.SetActive(false);
            _plusBtn.gameObject.SetActive(false);
        }

        private void OnPressMinusBtn()
        {
            Notification(false);
        }

        private void OnPressPlusBtn()
        {
            Notification(true);
        }

        private void Notification(bool value)
        {
            PressBtnAction?.Invoke(value);
        }
    }
}