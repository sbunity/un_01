using System;
using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Core
{
    [DefaultExecutionOrder(-50)]
    public class InternetConnectionMonitor : MonoBehaviour
    {
        [SerializeField] private bool StartOnAwake = true;
        
        [Header("Ping parameters")]
        [SerializeField] private string url = "https://google.com";

        [SerializeField] private float pingInterval = 5f;
        
        [SerializeField] private GameObject panelNoInternet;

        [SerializeField, Space(10)] private UnityEvent InternetAvailable;

        [SerializeField] private UnityEvent InternetNotAvailable;
        
        public static InternetConnectionMonitor Instance;

        private UnityWebRequest _request;
        private bool? isConnected;
        private Coroutine testCoroutine;
        
        private bool? IsConnected
        {
            get => isConnected;
            set
            {
                if(panelNoInternet)
                    panelNoInternet.SetActive(value != true);
                
                if (isConnected == value) return;

                isConnected = value;
                
                CheckConnect(value);
            }
        }
        
        private void CheckConnect(bool? isConnected)
        {
            switch (isConnected)
            {
                case true:
                    InternetAvailable?.Invoke();
                    break;
                case false:
                    InternetNotAvailable?.Invoke();
                    break;
            }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                
                DontDestroyOnLoad(Instance.gameObject);

                if (StartOnAwake)
                {
                    StartCheckConnect();
                }
            }
        }

        public void StartCheckConnect()
        {
            if (testCoroutine == null)
            {
                testCoroutine = StartCoroutine(Testing());
            }
            else
            {
                //PrintMessage("Connection check already started!");
            }
        }

        public void StopConnectionCheck()
        {
            if (testCoroutine != null)
            {
                StopCoroutine(testCoroutine);
                
                testCoroutine = null;
            }
            else
            {
                PrintMessage("No active Connection check!");
            }
        }

        public void CheckErrorReceived()
        {
            if (IsConnected == true)
            {
                IsConnected = false;
            }
        }
        
        private IEnumerator Testing()
        {
            while (true)
            {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    IsConnected = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(url))
                    {
                        _request = new UnityWebRequest(url);

                        yield return _request.SendWebRequest();

                        if (_request.result == UnityWebRequest.Result.Success)
                        {
                            IsConnected = true;
                        }
                        else if (_request.result != UnityWebRequest.Result.InProgress)
                        {
                            IsConnected = false;
                        }
                    }
                    else
                    {
                        IsConnected = false;
                    }
                }

                yield return new WaitForSeconds(pingInterval);
            }
        }
        
        private void PrintMessage(string message)
        {
            //Debugger.Log($"@@@ InternetConnectionMonitor: {message}", new Color(0.8f, 0.5f, 0));
        }
    }

    [Serializable] public class UnityConnectivityEvent : UnityEvent<bool, string> { }
}
