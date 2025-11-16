using UnityEngine;
using UnityEngine.Events;

namespace CoinPusher
{
    /// <summary>
    /// Tracks player balance, handles auto-drop costs and jackpot payouts.
    /// </summary>
    public class EconomyManager : MonoBehaviour
    {
        [SerializeField] private int startingCoins = 200;
        [SerializeField] private int coinCost = 1;
        [SerializeField] private UnityEvent<int> onBalanceChanged;

        private int _balance;

        private void Awake()
        {
            _balance = startingCoins;
            onBalanceChanged?.Invoke(_balance);
        }

        public bool TrySpend(int amount)
        {
            if (_balance < amount) return false;
            _balance -= amount;
            onBalanceChanged?.Invoke(_balance);
            return true;
        }

        public void AddCoins(int amount)
        {
            _balance += amount;
            onBalanceChanged?.Invoke(_balance);
        }

        public void OnCoinCollected(int amount)
        {
            AddCoins(amount);
        }

        public bool RequestDropPayment()
        {
            return TrySpend(coinCost);
        }
    }
}
