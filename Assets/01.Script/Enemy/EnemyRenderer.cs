using UnityEngine;
using Agents;
using DG.Tweening;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Enemys
{
    public class EnemyRenderer : MonoBehaviour, IAgentComponent
    {
        private Enemy _enemy;
        private Animator _animator;
        private float nextDir;
        public bool CanRotation { get; set; } = true;
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
            if (!CanRotation)
                return;
            Vector2 playerPos = _enemy.PlayerManager.PlayerTrm.position;
            float dir = Mathf.Atan2(playerPos.y-transform.position.y,playerPos.x -  transform.position.x) * Mathf.Rad2Deg - 90;
            //float dir = Mathf.Atan2(obj.position.y - transform.position.y, obj.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            transform.DOKill();
            transform.DORotate(new Vector3(0, 0, dir),1);
            //transform.rotation = Quaternion.Euler(0, 0, dir - 90);
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

