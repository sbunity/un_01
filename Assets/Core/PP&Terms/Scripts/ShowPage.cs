using UnityEngine;

namespace Core
{
    public class ShowPage : MonoBehaviour
    {
        public static ShowPage Instance;

        [SerializeField, Header("Embedded Toolbar")]
        private bool _embeddedToolbar;

        [SerializeField, Header("Reference RectTransform")]
        private RectTransform _referenceRectTransform;

        [SerializeField, Header("Webview Background")]
        private GameObject _bgWV;

        [SerializeField, Header("No Internet")]
        private GameObject _noInternetPopUp;

        private UniWebView _UWV;

        private void Awake()
        {
            Instance = this;
        }

        public void Load(string url = "http://google.com")
        {
            if (_UWV != null) return;

            CreateWebView();

            _UWV.Load(url);

            Subscribe();
        }

        private void Subscribe()
        {
            _UWV.OnPageErrorReceived += OnPageErrorReceived;

            _UWV.OnPageFinished += OnPageFinished;

            _UWV.OnShouldClose += OnPageClosed;
        }

        private void UnSubscribe()
        {
            _UWV.OnPageErrorReceived -= OnPageErrorReceived;

            _UWV.OnPageFinished -= OnPageFinished;

            _UWV.OnShouldClose -= OnPageClosed;
        }

        private void CreateWebView()
        {
            var WVGameObject = new GameObject("UWV");

            _UWV = WVGameObject.AddComponent<UniWebView>();

            SetFrame();
        }

        private void SetFrame()
        {
            if (_referenceRectTransform)
            {
                _UWV.ReferenceRectTransform = _referenceRectTransform;
            }
            else
            {
                _UWV.Frame = new Rect(0, 0, Screen.width, Screen.height);
            }
        }

        private bool OnPageClosed(UniWebView wv)
        {
            if (_bgWV) _bgWV.SetActive(false);

            return _bgWV;
        }
        
        private void OnPageFinished(UniWebView wv, int status, string url)
        {
            Show();
        }
        
        private void OnPageErrorReceived(UniWebView wv, int status, string url)
        {
            _noInternetPopUp.SetActive(true);

            UnSubscribe();

            Remove();
        }

        private void Remove()
        {
            if (_bgWV) _bgWV.SetActive(false);

            Destroy(_UWV);

            _UWV = null;
        }

        public void Hide()
        {
            Remove();
        }

        private void Show()
        {
            if (_UWV == null) return;

            if (_bgWV) _bgWV.SetActive(true);

            _UWV.Show();

            if (_embeddedToolbar)
                _UWV.EmbeddedToolbar.Show();
        }
    }
}
