using UnityEngine;

namespace Enemys
{
    public class HealerSkillState : EnemyState
    {
        private HealerEnemy _enemy;
        private float _time;
        private float _oneHealingTime;
        private float _healingCnt;

        public HealerSkillState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _enemy = enemy as HealerEnemy;
        }

        public override void Enter()
        {
            base.Enter();
            _time = Time.time;
            _oneHealingTime = _enemy.healingTime / _enemy.healingCnt;
            _healingCnt = _enemy.healingTime / _oneHealingTime;
        }

        public override void Update()
        {
            base.Update();

            if(_time + _oneHealingTime < Time.time)
            {
                Collider2D[] collider = Physics2D.OverlapCircleAll(_enemy.transform.position, _enemy.skillRadius, _enemy.targetLayer);
                for (int i = 0; i < collider.Length; i++)
                {
                    if (collider[i].TryGetComponent(out Enemy enemy))
                    {
                        Debug.Log(enemy.Stat.health.baseValue);
                        enemy.Stat.health.baseValue += 5;
                    }
                }
                _time = Time.time;
                _healingCnt--;
            }
            if(_healingCnt <=0)
                _enemy.ChangeState(EnemyStateEnum.Move);


        }
    }
}

