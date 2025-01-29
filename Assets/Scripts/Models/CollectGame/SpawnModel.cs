using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Models.CollectGame
{
    public class SpawnModel
    {
        private bool _canSpawnbaloon;
        private int _prize;
        private int _prizeIndex;
        private int _positionCount;
        private int _baloonsSkinCount;
        
        private List<int> _spawnPositionIndexes;
        private List<int> _prizes;

        private const float MinSize = 0.5f;
        private const float MaxSize = 1.6f;

        public bool CanSpawn
        {
            get => _canSpawnbaloon;
            set => _canSpawnbaloon = value;
        }

        public SpawnModel(List<int> prizes, int positionCount, int baloonSkinCount)
        {
            _prize = 0;
            _prizeIndex = 0;
            _prizes = new List<int>(prizes);
            _positionCount = positionCount;
            _baloonsSkinCount = baloonSkinCount;
        }

        public float GetRandomSize()
        {
            float size = Random.Range(MinSize, MaxSize);

            return size;
        }

        public int GetRandomPrize()
        {
            int prize = _prizes[_prizeIndex];

            _prizeIndex++;

            if (_prizeIndex == _prizes.Count)
            {
                _prizeIndex = 0;
            }

            return prize;
        }

        public int GetBaloonSpriteIndex()
        {
            int index = Random.Range(0, _baloonsSkinCount);

            return index;
        }
        
        public int GetPositionIndex()
        {
            if (_spawnPositionIndexes == null || _spawnPositionIndexes.Count == 0)
            {
                FillSpawnPositionIndexes();
            }

            int index = _spawnPositionIndexes[0];
            
            _spawnPositionIndexes.RemoveAt(0);

            return index;
        }

        private void FillSpawnPositionIndexes()
        {
            List<int> spawnPos = new List<int>();
            _spawnPositionIndexes = new List<int>();

            for (int i = 0; i < _positionCount; i++)
            {
                spawnPos.Add(i);
            }
            
            var random = new System.Random();
            _spawnPositionIndexes.AddRange(spawnPos.OrderBy(item => random.Next()).ToList());
        }
    }
}