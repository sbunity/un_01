using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Models.CollectionGame
{
    public class SpawnModel
    {
        private int _positionCount;
        private int _baloonsSkinCount;
        private int _finishMoveBaloonCount;

        private List<int> _spawnPostionIndexes;
        private List<int> _activeBaloonsIndexes;
        private List<int> _indexes;

        private const int BaloonsCount = 20;
        private const float MinSize = 0.5f;
        private const float MaxSize = 1.6f;

        public bool CanSpawnBaloon => _indexes.Count > 0;
        public bool IsEndGame => _finishMoveBaloonCount == BaloonsCount;

        public SpawnModel(List<int> indexes,int baloonsSkinCount ,int positionCount)
        {
            _positionCount = positionCount;
            _baloonsSkinCount = baloonsSkinCount;
            _finishMoveBaloonCount = 0;

            FillIconIndexes(indexes);
        }

        public void UpdateFinishBallonsCount()
        {
            _finishMoveBaloonCount++;
        }

        public float GetRandomSize()
        {
            float size = Random.Range(MinSize, MaxSize);

            return size;
        }

        public int GetIconIndex()
        {
            int randomIndex = Random.Range(0, _indexes.Count);

            int index = _indexes[randomIndex];
            
            _indexes.RemoveAt(randomIndex);

            return index;
        }

        public int GetBaloonSpriteIndex()
        {
            int index = Random.Range(0, _baloonsSkinCount);

            return index;
        }

        public int GetPositionIndex()
        {
            if (_spawnPostionIndexes == null || _spawnPostionIndexes.Count == 0)
            {
                FillSpawnPositionIndexes();
            }

            int index = _spawnPostionIndexes[0];
            
            _spawnPostionIndexes.RemoveAt(0);

            return index;
        }

        private void FillSpawnPositionIndexes()
        {
            List<int> spawnPos = new List<int>();
            _spawnPostionIndexes = new List<int>();

            for (int i = 0; i < _positionCount; i++)
            {
                spawnPos.Add(i);
            }
            
            var random = new System.Random();
            _spawnPostionIndexes.AddRange(spawnPos.OrderBy(item => random.Next()).ToList());
        }

        private void FillIconIndexes(List<int> indexes)
        {
            _indexes = new List<int>(indexes);

            for (int i = _indexes.Count; i < BaloonsCount; i++)
            {
                _indexes.Add(-1);
            }
        }
    }
}