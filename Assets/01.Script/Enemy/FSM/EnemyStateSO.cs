using UnityEngine;

namespace Enemys
{
    [CreateAssetMenu(fileName = "EnemyStateSO", menuName = "SO/EnemyFSM/StateSO")]
    public class EnemyStateSO : ScriptableObject
    {
        public EnemyStateEnum stateEnum;
        public string className;
        public AnimParamSO animParam;
    }
}


