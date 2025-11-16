using UnityEngine;

namespace CoinPusher
{
    /// <summary>
    /// Simple auto-drop button hook that requests payment and spawns a coin.
    /// </summary>
    public class AutoDropController : MonoBehaviour
    {
        [SerializeField] private CoinSpawner spawner;
        [SerializeField] private EconomyManager economy;

        public void DropCoin()
        {
            if (spawner == null || economy == null) return;
            if (!economy.RequestDropPayment()) return;
            spawner.Spawn();
        }
    }
}
