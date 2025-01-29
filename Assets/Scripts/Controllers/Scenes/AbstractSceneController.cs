using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using SO;
using Enums;
using Names;
using Sounds;
using Values;

namespace Controllers.Scenes
{
    public abstract class AbstractSceneController : MonoBehaviour
    {
        [SerializeField] 
        private Text _moneyCountText;
        [SerializeField] 
        private SoundsController _soundsController;
        [SerializeField]
        private SceneSounds _sceneSounds;

        private MusicController _musicController;

        private void OnEnable()
        {
            _musicController = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicController>();
            
            _sceneSounds.SetAudioClip();
            
            Initialize();
            Subscribe();
            OnSceneEnable();
            
            UpdateMoneyCountText(0);

            Wallet.OnChangedMoney += UpdateMoneyCountText;
        }

        private void Start()
        {
            PlayMusic();
            OnSceneStart();
        }

        private void OnDisable()
        {
            Unsubscribe();
            OnSceneDisable();
            
            Wallet.OnChangedMoney -= UpdateMoneyCountText;
        }

        protected abstract void OnSceneEnable();
        protected abstract void OnSceneStart();
        protected abstract void OnSceneDisable();
        protected abstract void Initialize();
        protected abstract void Subscribe();
        protected abstract void Unsubscribe();

        protected void LoadScene(SceneName sceneName)
        {
            SetClickClip();
            
            StartCoroutine(DelayLoadScene(sceneName.ToString()));
        }

        protected void UpdateMoneyCountText(float count)
        {
            if (_moneyCountText == null)
            {
                return;
            }

            _moneyCountText.text = Wallet.Money.ToString("F2");
        }

        protected void SetClickClip()
        {
            PlaySound(AudioNames.ClickClip);
        }

        protected AudioClip GetAudioClip(string clipName)
        {
            return _sceneSounds.GetAudioClipByName(clipName);
        }

        protected void PlaySound(AudioNames audioName)
        {
            AudioClip clip = GetAudioClip(audioName.ToString());
            
           _soundsController.TryPlaySound(clip);
        }

        protected void PlayMusic()
        {
            AudioClip clip = GetAudioClip(AudioNames.Background.ToString());
            
            _musicController.TryPlayMusic(clip);
        }

        private IEnumerator DelayLoadScene(string sceneName)
        {
            yield return new WaitForSecondsRealtime(0.3f);

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}