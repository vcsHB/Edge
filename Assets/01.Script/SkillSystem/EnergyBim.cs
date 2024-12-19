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
        Vector2 direction = (end - start).normalized;

        float distance = Vector2.Distance(start, end);
        float lineWidth = _lineRenderer.startWidth / 2f; // 라인의 두께의 반만큼 반지름 사용

        // 2D CircleCast로 충돌 감지
        RaycastHit2D[] hits = Physics2D.CircleCastAll(start, lineWidth, direction, distance, _whatIsEnemy);

        foreach (var hit in hits)
        {
            Debug.Log($"에너지빔ㅁ: 공격받은적은 {hit.collider.gameObject.name} ");
            var enemyHealth = hit.collider.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(_damagePerSecond * 0.2f); // 0.2초마다 피해
            }
        }
    }


}
