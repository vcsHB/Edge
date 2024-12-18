using UnityEngine;
using Agents;

namespace Enemys
{
    public class EnemyRenderer : MonoBehaviour, IAgentComponent
    {
        private Enemy _enemy;
        [SerializeField] private Transform obj;
        private Animator _animator;
        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void Initialize(Agent agent)
        {
            _enemy = agent as Enemy;
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            //Vector2 playerPos = _enemy.Player.PlayerTrm.position;
            //float dir = Mathf.Atan2(playerPos.y-transform.position.y,playerPos.x -  transform.position.x) * Mathf.Rad2Deg;

            float dir = Mathf.Atan2(obj.position.y - transform.position.y, obj.position.x - transform.position.x) * Mathf.Rad2Deg;

            _enemy.transform.rotation = Quaternion.Euler(0, 0, dir);
        }

        public void SetParam(AnimParamSO param, bool value) 
            => _animator.SetBool(param.animHash, value);

        public void SetParam(AnimParamSO param, float value)
            => _animator.SetFloat(param.animHash, value);

        public void SetParam(AnimParamSO param, int value)
            => _animator.SetInteger(param.animHash, value);
        public void SetParam(AnimParamSO param)
            => _animator.SetTrigger(param.animHash);
    }
}

