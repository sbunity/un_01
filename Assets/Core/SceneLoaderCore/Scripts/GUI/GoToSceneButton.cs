using Extension;
using UnityEngine;
using UnityEngine.UI;
using Prototype.SceneLoaderCore.Helpers;

namespace Prototype
{
    [RequireComponent(typeof(Button))]
    public class GoToSceneButton : ButtonClickHandler
    {
        [Header("Next Scene")]
        [Scene]
        public string target;
        
        protected override void OnButtonClick()
        {
            SceneLoader.Instance.SwitchToScene(target);
        }
        
        public void Click()
        {
            SceneLoader.Instance.SwitchToScene(target);
        }
    }
}
