using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UIManage
{

    public class FeverGauge : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;


        private void Start() {
            
            ScoreManager.Instance.OnFeverChangedEvent += HandleRefreshGauge;
        }

        public void HandleRefreshGauge(int currnet, int max)
        {
            float ratio = (float)currnet / max;
            _fillImage.fillAmount = Mathf.Clamp01(ratio);
        }
    }

}