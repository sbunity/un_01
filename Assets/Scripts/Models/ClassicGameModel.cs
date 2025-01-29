using System.Collections.Generic;
using UnityEngine;
using Values;

namespace Models
{
    public class ClassicGameModel
    {
        private int _bet;
        private int _activeBalloonsCount;
        
        private List<float> _scallers;
        private List<float> _coefficients;

        private const int MinBet = 5;
        private const int MaxBet = 15;

        public bool IsActiveMinusBtn => _bet > MinBet;
        public bool IsActivePlusBtn => _bet < MaxBet;

        public int Bet => _bet;

        public bool IsWin => GetReward() > 0;
        public bool IsLastBalloon => _activeBalloonsCount == 1;

        public ClassicGameModel(int balloonsCount)
        {
            _bet = 5;
            _activeBalloonsCount = balloonsCount;
            
            FillScallers(balloonsCount);
        }

        public void PurchaseBet()
        {
            Wallet.TryPurchase(_bet);
        }

        public void ChangeBet(bool value)
        {
            _bet +=  value ? 1 : -1;
        }

        public float GetScaleByIndex(int index)
        {
            return _scallers[index];
        }

        public void UpdateCoefficientBuIndex(int index, float coefficient)
        {
            _coefficients[index] = coefficient;
        }

        public void ResetCoefficientByIndex(int index)
        {
            _activeBalloonsCount--;
            _coefficients[index] = 0;
        }

        public void AddRewardToBalance()
        {
            Wallet.AddMoney(GetReward());
        }

        public float GetCoefficient()
        {
            float sum = 0;

            foreach (var coefficient in _coefficients)
            {
                if (coefficient == 0)
                {
                    continue;
                }

                float roundedValue = Mathf.Round(coefficient * 10) / 10;

                if (sum == 0)
                {
                    sum = 1;
                }

                sum *= roundedValue;
            }

            return sum;
        }

        public float GetReward()
        {
            float sum = 0;

            foreach (var coefficient in _coefficients)
            {
                if (coefficient == 0)
                {
                    continue;
                }

                float roundedValue = Mathf.Round(coefficient * 10) / 10;

                if (sum == 0)
                {
                    sum = 1;
                }

                sum *= roundedValue;
            }

            float reward = _bet * sum;

            return reward;
        }

        private void FillScallers(int balloonsCount)
        {
            _scallers = new List<float>();
            _coefficients = new List<float>();

            for (int i = 0; i < balloonsCount; i++)
            {
                float scale = Random.Range(1.1f, 4f);
                
                _scallers.Add(scale);
                _coefficients.Add(1f);
            }
        }
    }
}