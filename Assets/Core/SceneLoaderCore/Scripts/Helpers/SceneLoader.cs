using System.Collections.Generic;
using Extension;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype.SceneLoaderCore.Helpers
{
	public class SceneLoader : MonoBehaviour
    {
	    public static SceneLoader Instance;

		[Header("Main scene"), Scene]
		public string mainScene;

		[Header("Preloader scene"), Scene]
		public string preloaderScene;
		
		private CanvasGroup _fadeGroup;     

		private string _targetScene;
		
		private void Awake()
		{
			if (Instance) return;
			
			Instance = this;

			DontDestroyOnLoad(gameObject);
		}
		
        private void Start()
        {
            SwitchToScene(preloaderScene);
        }
        
        public void SwitchToScene(string targetScene)
		{
			Debug.Log($"We want to go on scene: <<{targetScene}>>");
			
			SetTargetScene(targetScene);

			UIFader.Instance.FadeIn();
		}
        
        public string GetCurrentScene()
        {
	        return SceneManager.GetActiveScene().name;
        }

        internal void LoadScene()
        {
	        UIFader.Instance.FadeOut();

	        SceneManager.LoadScene(GetTargetScene());
        }

		private void SetTargetScene(string scene)
		{
			_targetScene = scene;
		}

		private string GetTargetScene()
		{
			return _targetScene;
		}
    }
}
