using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UIManage
{
    
    public class SkillDescriptionPanel : MonoBehaviour {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _contentText;
        private PowerUpSO _powerUp;

        public void SetPowerUp(PowerUpSO powerUp)
        {
            _powerUp = powerUp;
            _iconImage.sprite = _powerUp.icon;
            _contentText.text = $"[{_powerUp.title}] {_powerUp.description}";
        }
    }
}