using UnityEngine;

namespace CoinPusher
{
    /// <summary>
    /// Handles the spawning of coins from the chute.
    /// Attach to the dropper object and assign coin prefab/target area.
    /// </summary>
    public class CoinSpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float spawnInterval = 1.5f;
        [SerializeField] private float spawnForce = 2.5f;
        [SerializeField] private Vector2 lateralJitter = new Vector2(0.12f, 0.12f);

        [Header("Audio")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip dropClip;

        private float _timer;

        private void Reset()
        {
            spawnPoint = transform;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= spawnInterval)
            {
                Spawn();
            }
        }

        public void Spawn()
        {
            if (coinPrefab == null || spawnPoint == null)
            {
                Debug.LogWarning("CoinSpawner is missing references.");
                return;
            }

            _timer = 0f;
            var position = spawnPoint.position;
            position.x += Random.Range(-lateralJitter.x, lateralJitter.x);
            position.z += Random.Range(-lateralJitter.y, lateralJitter.y);

            var coin = Instantiate(coinPrefab, position, Random.rotation);
            if (coin.TryGetComponent<Rigidbody>(out var body))
            {
                body.AddForce(Vector3.down * spawnForce, ForceMode.Impulse);
            }

            if (audioSource != null && dropClip != null)
            {
                audioSource.PlayOneShot(dropClip);
            }
        }
    }
}
