using UnityEngine;
using Agents;

namespace Enemys
{
    public class EnemyRenderer : MonoBehaviour, IAgentComponent
    {
        private Enemy _enemy;
        public void AfterInit()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(Agent agent)
        {
            _enemy = agent as Enemy;
        }

        private void Update()
        {
            Vector2 playerPos = _enemy.Player.PlayerTrm.position;
            float dir = Mathf.Atan2(playerPos.y-transform.position.y,playerPos.x -  transform.position.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, dir);
        }
    }
}

