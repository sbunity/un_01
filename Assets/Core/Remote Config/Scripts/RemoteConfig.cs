using System;
using System.Threading.Tasks;
using Firebase.Extensions;
using UnityEngine;

namespace Core
{
	public class RemoteConfig : MonoBehaviour
	{
		private Firebase.DependencyStatus _status = Firebase.DependencyStatus.UnavailableOther;

		private Action _OnInitalized;

		public void InitConfig(Action callback)
		{
			PrintMessage("@@@ RemoteConfig -> InitConfig");

			Firebase.Analytics.FirebaseAnalytics.LogEvent("start_game_event", "start_game", 1);

			PrintMessage("@@@ RemoteConfig -> LogEvent start_game");
			
			_OnInitalized = callback;

			Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
			{
				_status = task.Result;

				PrintMessage("@@@ RemoteConfig -> _status: " + _status);
				
				if (_status == Firebase.DependencyStatus.Available)
				{
					Initialize();

					FetchDataAsync();
				}
				else
				{
					PrintMessage($"@@@ RemoteConfig -> _status: {_status}");
				}
			});
		}

		private void Initialize()
		{
			PrintMessage("@@@ RemoteConfig -> Initialize: " + _status);
			
			var defaults = new System.Collections.Generic.Dictionary<string, object>();

			defaults.Add(GetFirebaseKey(), GetDefaultB1n0m());

			Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);
		}

		public string GetURL()
		{
			PrintMessage("@@@ RemoteConfig -> GetURL: " + GetFirebaseKey());
			
			return Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(GetFirebaseKey()).StringValue;
		}

		public bool isUrlDefaultB1n0m(string url)
		{
			PrintMessage($"@@@ RemoteConfig -> isUrlDefaultB1n0m: {url} ?= {GetDefaultB1n0m()}");

			var domainUrl = new Uri(url).Host;

			var domainUDefaultBinom = new Uri(GetDefaultB1n0m()).Host;

			return string.CompareOrdinal(domainUrl, domainUDefaultBinom) == 0;
		}
		
		public bool IsEmptyB1n0m(string url)
		{
			PrintMessage($"@@@ RemoteConfig -> IsEmptyB1n0m: {url} is empty = {string.IsNullOrEmpty(url)}");

			return string.IsNullOrEmpty(url);
		}

		private string GetDefaultB1n0m()
		{
			PrintMessage("@@@ RemoteConfig -> GetDefaultB1n0m: " + Settings.DefaultB1n0m());
			
			return Settings.DefaultB1n0m();
		}

		private string GetFirebaseKey()
		{
			PrintMessage("@@@ RemoteConfig -> GetFirebaseKey: " + Settings.FirebaseKey());
			
			return Settings.FirebaseKey();
		}

		private Task FetchDataAsync()
		{
			PrintMessage("@@@ RemoteConfig -> FetchDataAsync");
			
			var fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);

			return fetchTask.ContinueWithOnMainThread(Complete);
		}

		private void Complete(Task task)
		{
			PrintFetchMessage(task);

			var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;

			switch (info.LastFetchStatus)
			{
				case Firebase.RemoteConfig.LastFetchStatus.Success:
					Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
						.ContinueWithOnMainThread(task =>
						{
							PrintMessage(String.Format("Remote data loaded and ready (last fetch time {0}).",
								info.FetchTime));

							_OnInitalized?.Invoke();
						});
					break;
				case Firebase.RemoteConfig.LastFetchStatus.Failure:
					switch (info.LastFetchFailureReason)
					{
						case Firebase.RemoteConfig.FetchFailureReason.Error:
							PrintMessage("Fetch failed for unknown reason");
							break;
						case Firebase.RemoteConfig.FetchFailureReason.Throttled:
							PrintMessage("Fetch throttled until " + info.ThrottledEndTime);
							break;
					}

					break;
				case Firebase.RemoteConfig.LastFetchStatus.Pending:
					PrintMessage("Latest Fetch call still pending.");
					break;
			}
		}

		private void PrintFetchMessage(Task task)
		{
			if (task.IsCanceled)
			{
				PrintMessage("Fetch canceled.");
			}
			else if (task.IsFaulted)
			{
				PrintMessage("Fetch encountered an error.");
			}
			else if (task.IsCompleted)
			{
				PrintMessage("Fetch completed successfully!");
			}
		}

		private void PrintMessage(string message)
		{
			Debugger.Log($"### RemoteConfig -> message: {message}" , new Color(0, 0, 0.5f));
		}
	}
}