using UnityEngine;
namespace InteractSystem
{
    public class UpgradeItem : InteractObject
    {
        private PowerUpSO _powerUpSO;
        [SerializeField] private PlayerSkill _skillEnumType;

        private void Awake()
        {
            OnInteractEvent.AddListener(HandleInteract);
        }

        public void SetUpgradeInfo(PowerUpSO powerUp)
        {
            _powerUpSO = powerUp;
        }

        private void HandleInteract()
        {
            _powerUpSO.effectList.ForEach(effect => effect.UseEffect());
            
            Destroy(gameObject);
        }


    }
}