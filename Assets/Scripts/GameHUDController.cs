using UnityEngine;
using UnityEngine.UI;

namespace CoinPusher
{
    /// <summary>
    /// Simple HUD controller showing current balance and jackpot charge.
    /// </summary>
    public class GameHUDController : MonoBehaviour
    {
        [SerializeField] private Text coinLabel;
        [SerializeField] private Slider chronoSlider;

        private int _coins;
        private float _chronoCharge;

        public void OnCoinsChanged(int coins)
        {
            _coins = coins;
            if (coinLabel != null)
            {
                coinLabel.text = _coins.ToString("N0");
            }
        }

        public void OnChronoChargeChanged(float value)
        {
            _chronoCharge = value;
            if (chronoSlider != null)
            {
                chronoSlider.value = _chronoCharge;
            }
        }
    }
}
