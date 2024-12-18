using Agents;
using DG.Tweening;
using StatSystem;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    private int _damage;
    private float _speed;
    private float _lifetime;
    private LayerMask _whatIsEnemy;

    public void Initialize(int damage, float speed, float lifetime, LayerMask whatIsEnemy, Vector3 direction)
    {
        _damage = damage;
        _speed = speed;
        _lifetime = lifetime;
        _whatIsEnemy = whatIsEnemy;

        transform.forward = direction;

        // 1초 후 속도 감소 시작
        DOVirtual.DelayedCall(1f, () =>
        {
            DOTween.To(() => _speed, x => _speed = x, 0, 0.5f).SetEase(Ease.OutCubic);
        });

        Destroy(gameObject, _lifetime); // 수명 종료 후 삭제
    }

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_whatIsEnemy.value & (1 << other.gameObject.layer)) > 0)
        {
            // 적에게 피해 적용
            var enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(_damage);
            }

            // 적 이동 속도 감소
            var enemyStatus = other.GetComponent<StatusSO>();
            if (enemyStatus != null)
            {
                float slowEffect = enemyStatus.moveSpeed.GetValue() * -0.5f;
                enemyStatus.moveSpeed.AddModifier(slowEffect);

                DOVirtual.DelayedCall(1.5f, () =>
                {
                    enemyStatus.moveSpeed.RemoveModifier(slowEffect);
                });
            }
        }
    }
}
