using Agents;
using StatSystem;
using System.Collections;
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

    private bool _isSlowed = false;  // 이동 속도 감소 상태 추적

    public void Initialize(int damage, float speed, float lifetime, LayerMask whatIsEnemy, Vector3 direction, float slowAmount, float slowDuration)
    {
        _damage = damage;
        _speed = speed;
        _lifetime = lifetime;
        _whatIsEnemy = whatIsEnemy;
        _direction = direction;
        _slowAmount = slowAmount;
        _slowDuration = slowDuration;

        // 에너지볼 속도 감소 코루틴 시작
        StartCoroutine(SlowDownOverTime());

        Destroy(gameObject, _lifetime);  // 일정 시간 후 에너지볼 삭제
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;  // 이동
    }

    // 속도 감소 및 원래 속도로 복구하는 코루틴
    private IEnumerator SlowDownOverTime()
    {
        float timeElapsed = 0f;
        float initialSpeed = _speed;
        float targetSpeed = 0f;

        // 속도 감소
        while (timeElapsed < 1.5f)  // 1.5초 동안 속도 감소
        {
            _speed = Mathf.Lerp(initialSpeed, targetSpeed, timeElapsed / 1.5f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _speed = targetSpeed;

        // 1.5초 후에 원래 속도로 복구
        timeElapsed = 0f;
        while (timeElapsed < 1.5f)  // 1.5초 동안 원래 속도로 복구
        {
            _speed = Mathf.Lerp(targetSpeed, initialSpeed, timeElapsed / 1.5f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _speed = initialSpeed;  // 최종적으로 원래 속도로 복원
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
                Debug.Log($"에너지볼 : 에너미 체력 {_damage} 감소");
            }

            // 적 이동 속도 감소
            var enemyStatus = other.GetComponent<StatusSO>();
            if (enemyStatus != null && !_isSlowed)
            {
                _isSlowed = true;
                float slowEffect = enemyStatus.moveSpeed.GetValue() * -_slowAmount;
                enemyStatus.moveSpeed.AddModifier(slowEffect);  // 속도 감소

                // 일정 시간 후 속도 복구
                StartCoroutine(RemoveSlowEffect(enemyStatus, slowEffect));
            }
        }
    }

    // 적의 이동 속도 복구 
    private IEnumerator RemoveSlowEffect(StatusSO enemyStatus, float slowEffect)
    {
        yield return new WaitForSeconds(_slowDuration);
        enemyStatus.moveSpeed.RemoveModifier(slowEffect);  // 속도 복구
        _isSlowed = false;
    }
}
