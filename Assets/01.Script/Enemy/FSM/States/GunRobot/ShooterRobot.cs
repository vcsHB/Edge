using Unity.VisualScripting;
using UnityEngine;

namespace Enemys
{
    public class ShooterRobot : Enemy
    {
        public int bulletCnt;
        public float shootingDelay;
        public float moveTime;
        public Transform [] bulletFirePos;

    }
}


