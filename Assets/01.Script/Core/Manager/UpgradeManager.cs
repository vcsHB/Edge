using System.Collections.Generic;
using System.Linq;
using Core.MapConrtrolSystem;
using InteractSystem;
using UnityEngine;
namespace Core
{

    public class UpgradeManager : MonoSingleton<UpgradeManager>
    {
        [SerializeField] private PowerUpListSO _powerUpList;
        [SerializeField] private MapController _mapController;
        public Dictionary<int, int> upgradeDictionary = new();
        [SerializeField] private UpgradeItem _upgradeItemPrefab;


        public int Find(int id)
        {
            if (upgradeDictionary.TryGetValue(id, out int amount))
            {
                return amount;
            }
            return 0;
        }

        public void ApplyPowerUp(int id)
        {
            if (upgradeDictionary.ContainsKey(id))
            {
                upgradeDictionary[id]++;
                return;
            }
            upgradeDictionary.Add(id, 1);
        }

        public void DropUpgradeItem()
        {
            PowerUpSO[] arr = _powerUpList.list.Where(x => x.CheckCanUpgrade()).ToArray();

            PowerUpSO powerUp = arr[Random.Range(0, arr.Length)]; // 랜덤으로 1 픽

            UpgradeItem item = Instantiate(_upgradeItemPrefab, _mapController.GetRandomPointPosition(), Quaternion.identity);
            item.SetUpgradeInfo(powerUp);
        }

        

    }
}