using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Enemys
{
    public enum EnemyStateEnum
    {
        Idle,Move,Attack,Dead
    }

    [CreateAssetMenu(fileName = "EnemyStateListSO", menuName = "SO/EnemyFSM/StateListSO")]
    public class EnemyStateListSO : ScriptableObject
    {
        public List<EnemyStateSO> states;
    }
}

