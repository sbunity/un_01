using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Values;

namespace Models
{
    public class CollectionGameModel
    {
        private List<int> _progressIndexes;
        private List<bool> _progressStates;

        private const string CollectionCountKey = "CollectionGameModel.CollectionCount";
        private const string SceneStateKey = "CollectionGameModel.SceneState";

        public List<int> ProgressIndexes => _progressIndexes;
        public List<bool> ProgressStates => _progressStates;

        public bool IsRestart
        {
            get => PlayerPrefs.GetInt(SceneStateKey, 0) == 1;
            set => PlayerPrefs.SetInt(SceneStateKey, value ? 1 : 0);
        }

        public CollectionGameModel()
        {
            FillProgressData();
        }

        public List<int> GetCollectionCounts()
        {
            List<int> counts = new List<int>();

            for (int i = 0; i < 15; i++)
            {
                int count = PlayerPrefs.GetInt(CollectionCountKey + i, 0);
                
                counts.Add(count);
            }

            return counts;
        }

        public List<bool> GetCollectionStates()
        {
            List<int> counts = new List<int>(GetCollectionCounts());

            return counts.Select(t => t == 15).ToList();
        }

        public List<float> GetCollectionFillAmounts()
        {
            List<int> counts = new List<int>(GetCollectionCounts());

            return counts.Select(count => count / 15f).ToList();
        }

        public void AddRewardToBalance()
        {
            Wallet.AddMoney(500);
        }

        public void ResetItemCount(int index)
        {
            PlayerPrefs.SetInt(CollectionCountKey+index, 0);
        }

        public void ChangeProgressState(int index)
        {
            int newIndex = _progressIndexes.IndexOf(index);

            int count = PlayerPrefs.GetInt(CollectionCountKey + index);
            count++;
            PlayerPrefs.SetInt(CollectionCountKey+index, count);

            _progressStates[newIndex] = true;
        }

        private void FillProgressData()
        {
            _progressStates = new List<bool>();
            _progressIndexes = new List<int>();

            List<int> allIndexes = new List<int>();

            for (int i = 0; i < 15; i++)
            {
                allIndexes.Add(i);
            }

            for (int i = 0; i < 10; i++)
            {
                int randomIndex = Random.Range(0, allIndexes.Count);
                _progressIndexes.Add(allIndexes[randomIndex]);
                _progressStates.Add(false);
                allIndexes.RemoveAt(randomIndex);
            }
        }
    }
}