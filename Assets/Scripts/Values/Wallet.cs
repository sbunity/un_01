using System;
using UnityEngine;

namespace Values
{
    public static class Wallet
    {
        public static event Action<float> OnChangedMoney = null;

        public static float Money
        {
            get => PlayerPrefs.GetFloat("WalletMoney", 1000);

            private set
            {
                if (value > 999999999 || value < 0)
                    value = 999999999;
                
                PlayerPrefs.SetFloat("WalletMoney", value);
                PlayerPrefs.Save();

                OnChangedMoney?.Invoke(value);
            }
        }

        public static void AddMoney(float money)
        {
            if (money > 0)
                Money += money;
        }

        public static bool TryPurchase(float money)
        {
            if (Money >= money)
            {
                Money -= money;

                return true;
            }

            return false;
        }
    }
}