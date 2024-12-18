using Agents;
using UnityEngine;

public class EnergyBim : MonoBehaviour
{
    private float _damagePerSecond; // 초당 피해량
    private float _duration;       // 지속 시간
    private LayerMask _whatIsEnemy;
    private LineRenderer _lineRenderer;

    public void Initialize(float damagePerSecond, float duration, LayerMask whatIsEnemy, Vector3 start, Vector3 end)
    {
        _damagePerSecond = damagePerSecond;
        _duration = duration;
        _whatIsEnemy = whatIsEnemy;

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);

        InvokeRepeating(nameof(DamageEnemies), 0f, 0.2f); // 0.2초마다 적에게 피해를 줌
        Destroy(gameObject, _duration); // 지속 시간 후 자동 삭제
    }

    private void DamageEnemies()
    {
        Vector3 start = _lineRenderer.GetPosition(0);
        Vector3 end = _lineRenderer.GetPosition(1);
        RaycastHit[] hits = Physics.RaycastAll(start, (end - start).normalized, Vector3.Distance(start, end), _whatIsEnemy);

        foreach (var hit in hits)
        {
            var enemyHealth = hit.collider.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(_damagePerSecond * 0.2f); // 0.2초마다 피해
            }
        }
    }
}
