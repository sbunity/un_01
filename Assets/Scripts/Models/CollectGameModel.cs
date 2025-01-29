using Values;

namespace Models
{
    public class CollectGameModel
    {
        private int _bet;
        private int _fullPrize;
        
        private const int MinBet = 5;
        private const int MaxBet = 15;

        public bool IsActiveMinusBtn => _bet > MinBet;
        public bool IsActivePlusBtn => _bet < MaxBet;
        public bool CanStartGame => Wallet.Money >= _bet;

        public int Bet => _bet;
        public int Prize => _fullPrize;
        
        public float Reward => (_fullPrize * _bet);
        public bool IsWin => _fullPrize > 0;

        public CollectGameModel()
        {
            _bet = 5;
            _fullPrize = 0;
        }

        public void PurchaseBet()
        {
            Wallet.TryPurchase(_bet);
        }

        public void AddPrize(int value)
        {
            _fullPrize += value;
        }

        public void ChangeBet(bool value)
        {
            _bet +=  value ? 1 : -1;
        }
    }
}