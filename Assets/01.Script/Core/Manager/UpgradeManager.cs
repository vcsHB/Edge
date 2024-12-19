using System.Collections.Generic;
using UnityEngine;
namespace Core
{

    public class UpgradeManager : MonoSingleton<UpgradeManager>
    {
        public Dictionary<int, int> upgradeDictionary = new();


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
    }
}