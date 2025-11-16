using UnityEngine;

namespace CoinPusher
{
    /// <summary>
    /// Place this on the Chrono Vault chute collider to siphon coins.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ChronoVaultTrigger : MonoBehaviour
    {
        [SerializeField] private ChronoVaultManager vaultManager;
        [SerializeField] private string coinTag = "Coin";

        private void Reset()
        {
            var col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(coinTag) || vaultManager == null) return;
            Destroy(other.gameObject);
            vaultManager.OnVaultCoinEnter(1);
        }
    }
}
