using System;
using Core;
using Core.AppInstallTracker;
using Prototype.SceneLoaderCore.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Content : MonoBehaviour
{
    //[SerializeField] private bool isTest;
    
    //[SerializeField] private bool isError;

    //[SerializeField] private string _url403 = "https://duocriativolda.com";
    
    //[SerializeField] private string _url200 = "https://simoldesacos.com";

    private void Start()
    {
        //Debugger.Log($"Start Content");

        if (!AppInstallTracker.HasEnoughDaysPassed())
        {
            ShowWhiteApp();
            
            return;
        }
        
        AppState.Clear();
        
        if (AppState.GetAppStartedSuccess())
        {
            Init();
        }
        else
        {
            Init();
            //StartCoroutine(AdditionalCheck.Check(GetUrl(), Init));
        }
    }

    private void Init()
    {
        PrintMessage($"Init Content");

        PrintMessage($"     ConstantState: {AppState.GetConstantState()}");
        
        PrintMessage($"     CurrentState: {AppState.GetCurrentState()}");

        var isStateNone = AppState.GetConstantState() != State.None;

        if (CheckState(isStateNone ? AppState.GetConstantState() : AppState.GetCurrentState()))
        {
            ShowWhiteApp();
        }
        else
        {
            ShowGreyApp();
        }
    }

    private bool CheckState(State targetState, State expectedState = State.White)
    {
        return targetState == expectedState;
    }

    public void ShowWhiteApp()
    {
        PrintMessage($"Show White pp");
        
        if (SceneLoader.Instance)
        {
            SceneLoader.Instance.SwitchToScene(SceneLoader.Instance.mainScene);
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }

    private void ShowGreyApp()
    {
        PrintMessage($"Передаєм керування бекенду");
            
        if(InternetConnectionMonitor.Instance)
            InternetConnectionMonitor.Instance.StartCheckConnect();
    }
    
    /*private string GetUrl()
    {
        return Settings.AdditionalUrl();
        //return isTest? isError? _url403 : _url200 : Settings.AdditionalUrl();
    }*/

    public void CheckContentIfDroppedInternet()
    {
        PrintMessage($"CheckContentIfDroppedInternet");
        
        var isStateNone = AppState.GetConstantState() == State.None;
        
        PrintMessage($"isStateNone = {isStateNone}");

        if (isStateNone)
        {
            var currentState = AppState.GetCurrentState();
            
            PrintMessage($"isStateNone = {currentState}");
        
            if (CheckState(currentState) || CheckState(currentState, State.None))
            {
                ShowWhiteApp();
            }
        }
    }
    
    private void PrintMessage(string message)
    {
        Debugger.Log($"@@@ Content -> message: {message}", new Color(0.8f, 0.5f, 0));
    }
}
