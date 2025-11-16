using UnityEngine;
using UnityEngine.Events;

namespace CoinPusher
{
    /// <summary>
    /// Detects coins that fall into the collection tray using trigger colliders.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class CoinCollector : MonoBehaviour
    {
        [SerializeField] private string coinTag = "Coin";
        [SerializeField] private UnityEvent<int> onCoinsCollected;

        private void Reset()
        {
            var col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(coinTag)) return;

            Destroy(other.gameObject);
            onCoinsCollected?.Invoke(1);
        }
    }
}
