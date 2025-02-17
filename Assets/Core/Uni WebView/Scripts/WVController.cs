using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class WVController : MonoBehaviour
    {
        [SerializeField, Header("Embedded Toolbar")]
        private bool _embeddedToolbar;
        
        [SerializeField, Header("Config")]
        private RemoteConfig config;

        [SerializeField, Header("RectTransform")]
        private RectTransform _reference;

        [SerializeField, Header("Background")]
        private GameObject _bgWebView;

        private AgreeTerms _bttnAgreeTerms;

        [Space(10)] [SerializeField] private UnityEvent WebViewLoadingError;

        [SerializeField] private UnityEvent WVLoadingCompleted;

        [SerializeField] private UnityEvent DefaultB1n0mLoadingCompleted;

        private UniWebView _UWV;

        private string configUrl;

        private bool isVisible;
        
        private string B1nomURL
        {
            get
            {
                if (PlayerPrefs.HasKey(Constants.Binom))
                {
                    var newB1nom = config.GetURL();
                    var oldB1nom = PlayerPrefs.GetString(Constants.Binom, "");
                    
                    PrintMessage("Біном існує, будемо перевіряти його");

                    if (config.IsEmptyB1n0m(newB1nom) || config.isUrlDefaultB1n0m(newB1nom))
                    {
                        PrintMessage("Біном є тотожнім дефолтному ленду або порожній");
                        
                        return PlayerPrefs.GetString(Constants.LastUrl, config.GetURL());
                    }
                    else
                    {
                        PrintMessage("Біном НЕ дефолтний ленд і НЕ порожній");
                        
                        if (newB1nom == oldB1nom)
                        {
                            PrintMessage("Біном не змінився, має бути LastUrl");
                            PrintMessage($"LastUrl: {PlayerPrefs.GetString(Constants.LastUrl, config.GetURL())}");
                            return PlayerPrefs.GetString(Constants.LastUrl, config.GetURL());
                        }
                        else
                        {
                            PrintMessage("Біном змінився, має бути новий оффер");
                            PrintMessage($"newB1nom: {newB1nom}");
                            PrintMessage($"oldB1nom: {oldB1nom}");
                            
                            PlayerPrefs.SetString(Constants.Binom, newB1nom);
                            
                            PlayerPrefs.DeleteKey(Constants.StartUrl);
                            PlayerPrefs.Save();

                            return newB1nom;
                        }  
                    }
                }
                else
                {
                    var b1nom = config.GetURL();
                    
                    PlayerPrefs.SetString(Constants.Binom, b1nom);
                    PlayerPrefs.Save();
                    
                    PrintMessage($"Перша ініціалізація Binom на: {b1nom}");
                    
                    return b1nom;
                }
            }
            set
            {
                PlayerPrefs.SetString(Constants.Binom, value);
                PlayerPrefs.Save();
                
                PrintMessage($"Змінили Binom на: {value}");
            }
        }

        public void Initialize()
        {
            PrintMessage("WV Initialize");
            PrintMessage($"oldB1nom: {PlayerPrefs.GetString(Constants.Binom, "Ще не створений") }");
            if (_UWV)
            {
                PrintMessage("WV Initialize -> LoadUrl");
                
                LoadUrl();
            }
            else
            {
#if UNITY_EDITOR
                PrintMessage("Editor is NOT supported");
                DefaultB1n0mLoadingCompleted?.Invoke();
#else
                PrintMessage("Platfom is supported");
                if (config != null)
                {
                    config.InitConfig(CheckUrl);
                }
                else
                {
                    DefaultB1n0mLoadingCompleted?.Invoke();
                }
#endif
            }
        }

        public void Back()
        {
            //if (_UWV)
            //{
            //    _UWV.GoBack();
            //}
            
            if (!_UWV) return;

            if (_UWV.CanGoBack)
            {
                _UWV.GoBack();
            }
            else
            {
                var lastUrl = PlayerPrefs.GetString(Constants.LastUrl, "");
                var startUrl = PlayerPrefs.GetString(Constants.StartUrl, "");
                
                if (lastUrl != startUrl)
                {
                    _UWV.Load(startUrl);
                }
                else
                {
                    _UWV.Reload();
                }
            }
        }

        private void CheckUrl() //Check url from Config
        {
            //configUrl = config.GetURL();
            configUrl = B1nomURL;

            PrintMessage($"CheckUrl -> configUrl: {configUrl}");

            if (config.IsEmptyB1n0m(configUrl) || config.isUrlDefaultB1n0m(configUrl))
            {
                SetAppStateWhite();
            }
            else //url is b1nom
            {
                //AppState.SetConstantBlackState();

                //URL = configUrl; //оновюєм Біном
                
                LoadUrl();
            }
        }

        private void SetAppStateWhite()
        {
            AppState.SetConstantWhiteState();
            
            DefaultB1n0mLoadingCompleted?.Invoke(); ///////HERE WHITE APP
        }

        private void SetAppStateGrey(string url)
        {
            if (PlayerPrefs.GetString(Constants.Binom, "") != url)
            {
                AppState.SetConstantBlackState();
            }
        }
        
        private void LoadUrl()
        {
            PrintMessage($"LoadUrl:: _UWV ?= null:{_UWV == null}");
            
            PrintMessage($"     configUrl: {configUrl}");
            
            if (_UWV == null)
            {
                CreateWV();
            }

            _UWV.Load(configUrl);

            _UWV.OnPageStarted += (view, url) => { PrintMessage($"OnPageStarted: {url}");};

            _UWV.OnLoadingErrorReceived += (view, code, message, payload) =>
            {
                PrintMessage($"OnLoadingErrorReceived: code={code}, message={message}, payload={payload}");
                
                if (code == 102)
                {
                    _UWV.Load(PlayerPrefs.GetString(Constants.StartUrl));
                }
            };


            _UWV.OnPageFinished += OnPageFinished;
        }
        
        private void CreateWV()
        {
            PrintMessage("CreateWV");
            
            var WVGO = new GameObject("UWV");

            _UWV = WVGO.AddComponent<UniWebView>();

            ShouldClose();
        }
        
        private void ShouldClose()
        {
            _UWV.OnShouldClose += (view) =>
            {
                //if (_UWV.CanGoBack)
                //{
                //    _UWV.GoBack();
                //}
                //else
                {
                    if (PlayerPrefs.GetString(Constants.LastUrl) != PlayerPrefs.GetString(Constants.StartUrl))
                        _UWV.Load(PlayerPrefs.GetString(Constants.StartUrl));
                    else
                        _UWV.Reload();
                }

                return false;
            };
        }
        
        private void SetRectWV()
        {
            if (_reference)
            {
                _UWV.ReferenceRectTransform = _reference;
            }
            else
            {
                _UWV.Frame = new Rect(0, 0, Screen.width, Screen.height);
            }
        }

        private void OnPageFinished(UniWebView view, int statusCode, string url)
        {
            PrintMessage($"OnPageFinished: {url}");

            //SaveUrls(url);

            if (config.IsEmptyB1n0m(url) || config.isUrlDefaultB1n0m(url))
            {
                PrintMessage($"isUrlDefaultB1n0m: {url}");
                
                //RemoveWebView();
                
                if(!PlayerPrefs.HasKey(Constants.LastUrl))
                {
                    RemoveWebView();
                }
                else
                {
                    var lastUrl = PlayerPrefs.GetString(Constants.LastUrl, "https://google.com");
                    
                    PrintMessage($"config.isUrlDefaultB1n0{url} -> load {lastUrl}");
                    
                    _UWV.Load(lastUrl);
                }
            }
            else
            {
                PrintMessage($"NO isUrlDefaultB1n0m: {url}");
                
                SaveUrls(url);

                SetAppStateGrey(url);
                
                ShowWebView();
            }
        }

        private void SaveUrls(string currentUrl)
        {
            if (currentUrl == "about:blank")
            {
                return;
            }
            
            if (!PlayerPrefs.HasKey(Constants.StartUrl))
            {
                PlayerPrefs.SetString(Constants.StartUrl, currentUrl);
            }

            PlayerPrefs.SetString(Constants.LastUrl, currentUrl);
            PlayerPrefs.Save();

            PrintMessage($"SaveUrls: StartUrl = " +
                         $"{PlayerPrefs.GetString(Constants.StartUrl, "None")}, " +
                         $"LastUrl = {PlayerPrefs.GetString(Constants.LastUrl, "None")}");
        }
        
        private void RemoveWebView()
        {
            ShowBackgroundWebView(false);

            SetAppStateWhite();
            
            //DefaultB1n0mLoadingCompleted?.Invoke();

            Destroy(_UWV);

            _UWV = null;
        }

        private void ShowWebView()
        {
            if(removeWV) return;
            
            if (_UWV == null) return;

            if (isVisible) return;

            isVisible = true;

            ShowBackgroundWebView(true);

            SetRectWV();

            _UWV.Show();
            
            if (_embeddedToolbar)
            {
                _UWV.EmbeddedToolbar.SetPosition(UniWebViewToolbarPosition.Bottom);
                _UWV.EmbeddedToolbar.SetDoneButtonText("Home");
                //_UWV.EmbeddedToolbar.HideNavigationButtons();
                _UWV.EmbeddedToolbar.Show();
            }
            
            WVLoadingCompleted?.Invoke();

            SetAutoRotation();
        }
        
        public void HideWebView()
        {
            if (_UWV == null)
            {
                return;
            }

            if (!isVisible)
            {
                return;
            }

            isVisible = false;

            PrintMessage(" --- Hided");

            _UWV.Hide();
            
            ShowBackgroundWebView(false);

            Destroy(_UWV);

            _UWV = null;
            
            WebViewLoadingError?.Invoke();
        }
        
        private void SetAutoRotation()
        {
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

        private void ShowBackgroundWebView(bool value)
        {
            if (_bgWebView)
            {
                _bgWebView.SetActive(value);
            }
        }
        
        private void PrintMessage(string message)
        {
            Debugger.Log($"@@@ WVController -> message: {message}", new Color(0, 0.39f, 0));
        }

        private bool removeWV;
        public void Update2()
        {
            {
                HideWebView();
                removeWV = true;
            }
        }

        public void RemovePP()
        {
            PlayerPrefs.DeleteKey(Constants.Binom);
        }
    }
}
