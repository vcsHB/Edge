using Agents;
using DG.Tweening;
using StatSystem;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    private int _damage;
    private float _speed;
    private float _lifetime;
    private float _slowAmount;
    private float _slowDuration;
    private LayerMask _whatIsEnemy;

    private Vector3 _direction;

    public void Initialize(int damage, float speed, float lifetime, LayerMask whatIsEnemy, Vector3 direction, float slowAmount, float slowDuration)
    {
        _damage = damage;
        _speed = speed;
        _lifetime = lifetime;
        _whatIsEnemy = whatIsEnemy;
        _direction = direction;
        _slowAmount = slowAmount;
        _slowDuration = slowDuration;

        // 속도 감소 설정 (Ease 적용)
        DOVirtual.DelayedCall(1f, () =>
        {
            DOTween.To(() => _speed, x => _speed = x, 0, 0.5f).SetEase(Ease.OutCubic);
        });

        // 에너지볼 수명 종료
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (((1 << other.gameObject.layer) & _whatIsEnemy) != 0)
        {

            // 적에게 피해 적용
            var enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(_damage);
                Debug.Log("에너미 체력 감소");
            }
            else
            {
                Debug.Log("예외");
            }

            //// 적 이동 속도 감소
            //var enemyStatus = other.GetComponent<StatusSO>();
            //if (enemyStatus != null)
            //{
            //    float slowEffect = enemyStatus.moveSpeed.GetValue() * -_slowAmount;
            //    enemyStatus.moveSpeed.AddModifier(slowEffect);

            //    DOVirtual.DelayedCall(_slowDuration, () =>
            //    {
            //        enemyStatus.moveSpeed.RemoveModifier(slowEffect);
            //    });
            //}
        }
    }
}
