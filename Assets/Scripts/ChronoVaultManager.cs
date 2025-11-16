using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CoinPusher
{
    /// <summary>
    /// Unique feature: Chrono Vault stores coins that fall into a side chute
    /// and can later rewind onto the board as a swirling meteor shower.
    /// </summary>
    public class ChronoVaultManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem rewindFx;
        [SerializeField] private Transform releasePoint;
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private float releaseDelay = 0.15f;
        [SerializeField] private int coinsPerBurst = 5;
        [SerializeField] private int maxStoredCoins = 50;
        [SerializeField] private UnityEvent<float> onChargeChanged;

        private int _storedCoins;
        private bool _isReleasing;

        public void OnVaultCoinEnter(int amount)
        {
            _storedCoins = Mathf.Clamp(_storedCoins + amount, 0, maxStoredCoins);
            BroadcastCharge();
        }

        public void TriggerRewind()
        {
            if (_isReleasing || _storedCoins <= 0) return;
            StartCoroutine(RewindRoutine());
        }

        private IEnumerator RewindRoutine()
        {
            _isReleasing = true;
            rewindFx?.Play();

            while (_storedCoins > 0)
            {
                var spawnCount = Mathf.Min(coinsPerBurst, _storedCoins);
                for (var i = 0; i < spawnCount; i++)
                {
                    SpawnCoin();
                    _storedCoins--;
                }

                BroadcastCharge();
                yield return new WaitForSeconds(releaseDelay);
            }

            rewindFx?.Stop();
            _isReleasing = false;
        }

        private void SpawnCoin()
        {
            if (coinPrefab == null || releasePoint == null)
            {
                Debug.LogWarning("ChronoVaultManager missing prefab or release point");
                return;
            }

            var position = releasePoint.position;
            position += Random.insideUnitSphere * 0.1f;
            position.y += 0.25f;
            var coin = Instantiate(coinPrefab, position, Quaternion.identity);
            if (coin.TryGetComponent<Rigidbody>(out var body))
            {
                var force = (Vector3.down + Random.insideUnitSphere * 0.25f) * 3f;
                body.AddForce(force, ForceMode.Impulse);
                body.AddTorque(Random.insideUnitSphere * 3f, ForceMode.Impulse);
            }
        }

        private void BroadcastCharge()
        {
            var normalized = maxStoredCoins <= 0 ? 0f : (float)_storedCoins / maxStoredCoins;
            onChargeChanged?.Invoke(normalized);
        }
    }
}
