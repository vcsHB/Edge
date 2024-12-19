using Agents;
using UnityEngine;

public class EnergyBim : MonoBehaviour
{


    private float _damagePerSecond; // �ʴ� ���ط�
    private float _duration;       // ���� �ð�
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

        InvokeRepeating(nameof(DamageEnemies), 0f, 0.2f); // 0.2�ʸ��� ������ ���ظ� ��
        Destroy(gameObject, _duration); // ���� �ð� �� �ڵ� ����
    }

    private void DamageEnemies()
    {
        Vector3 start = _lineRenderer.GetPosition(0);
        Vector3 end = _lineRenderer.GetPosition(1);
        Vector2 direction = (end - start).normalized;

        float distance = Vector2.Distance(start, end);
        float lineWidth = _lineRenderer.startWidth / 2f; // ������ �β��� �ݸ�ŭ ������ ���

        // 2D CircleCast�� �浹 ����
        RaycastHit2D[] hits = Physics2D.CircleCastAll(start, lineWidth, direction, distance, _whatIsEnemy);

        foreach (var hit in hits)
        {
            Debug.Log($"����������: ���ݹ������� {hit.collider.gameObject.name} ");
            var enemyHealth = hit.collider.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(_damagePerSecond * 0.2f); // 0.2�ʸ��� ����
            }
        }
    }


}
