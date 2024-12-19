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

    private bool _isSlowed = false;  // �̵� �ӵ� ���� ���� ����

    public void Initialize(int damage, float speed, float lifetime, LayerMask whatIsEnemy, Vector3 direction, float slowAmount, float slowDuration)
    {
        _damage = damage;
        _speed = speed;
        _lifetime = lifetime;
        _whatIsEnemy = whatIsEnemy;
        _direction = direction;
        _slowAmount = slowAmount;
        _slowDuration = slowDuration;

        // �������� �ӵ� ���� �ڷ�ƾ ����
        StartCoroutine(SlowDownOverTime());

        Destroy(gameObject, _lifetime);  // ���� �ð� �� �������� ����
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;  // �̵�
    }

    // �ӵ� ���� �� ���� �ӵ��� �����ϴ� �ڷ�ƾ
    private IEnumerator SlowDownOverTime()
    {
        float timeElapsed = 0f;
        float initialSpeed = _speed;
        float targetSpeed = 0f;

        // �ӵ� ����
        while (timeElapsed < 1.5f)  // 1.5�� ���� �ӵ� ����
        {
            _speed = Mathf.Lerp(initialSpeed, targetSpeed, timeElapsed / 1.5f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _speed = targetSpeed;

        // 1.5�� �Ŀ� ���� �ӵ��� ����
        timeElapsed = 0f;
        while (timeElapsed < 1.5f)  // 1.5�� ���� ���� �ӵ��� ����
        {
            _speed = Mathf.Lerp(targetSpeed, initialSpeed, timeElapsed / 1.5f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _speed = initialSpeed;  // ���������� ���� �ӵ��� ����
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _whatIsEnemy) != 0)
        {
            // ������ ���� ����
            var enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(_damage);
                Debug.Log($"�������� : ���ʹ� ü�� {_damage} ����");
            }

            // �� �̵� �ӵ� ����
            var enemyStatus = other.GetComponent<StatusSO>();
            if (enemyStatus != null && !_isSlowed)
            {
                _isSlowed = true;
                float slowEffect = enemyStatus.moveSpeed.GetValue() * -_slowAmount;
                enemyStatus.moveSpeed.AddModifier(slowEffect);  // �ӵ� ����

                // ���� �ð� �� �ӵ� ����
                StartCoroutine(RemoveSlowEffect(enemyStatus, slowEffect));
            }
        }
    }

    // ���� �̵� �ӵ� ���� 
    private IEnumerator RemoveSlowEffect(StatusSO enemyStatus, float slowEffect)
    {
        yield return new WaitForSeconds(_slowDuration);
        enemyStatus.moveSpeed.RemoveModifier(slowEffect);  // �ӵ� ����
        _isSlowed = false;
    }
}
