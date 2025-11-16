using UnityEngine;

namespace CoinPusher
{
    /// <summary>
    /// Moves the pusher platform back and forth with easing to reduce abrupt motion.
    /// </summary>
    public class PusherController : MonoBehaviour
    {
        [SerializeField] private Vector3 pushDirection = Vector3.forward;
        [SerializeField] private float pushDistance = 0.35f;
        [SerializeField] private float cycleDuration = 1.75f;
        [SerializeField] private AnimationCurve easing = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private Vector3 _startPosition;
        private float _timer;

        private void Awake()
        {
            _startPosition = transform.localPosition;
        }

        private void Update()
        {
            if (cycleDuration <= 0.01f) return;

            _timer += Time.deltaTime;
            var normalized = (_timer % cycleDuration) / cycleDuration;
            var curveValue = easing.Evaluate(normalized);
            var offset = pushDirection.normalized * Mathf.Sin(curveValue * Mathf.PI * 2f) * pushDistance;
            transform.localPosition = _startPosition + offset;
        }
    }
}
