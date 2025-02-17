using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class AgreeTerms : MonoBehaviour
    {
        [SerializeField] private Button bttnAgreeTerms;

        [SerializeField] private CanvasGroup _canvasGroup;
        
        public void Init()
        {
            SetInteractable(false);
        }

        public void Set()
        {
            //PlayerPrefs.SetInt(WVController.AGREE_TERMS, 1);
            //PlayerPrefs.Save();

            SetInteractable(false);
        }

        public void SetInteractable(bool interactable)
        {
            _canvasGroup.alpha = interactable? 1 : 0;
            
            bttnAgreeTerms.interactable = interactable;
        }
    }
}
