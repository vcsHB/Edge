using Agents;
using UnityEngine;

public class DataBarier : MonoBehaviour
{
    public float damageReduction = 0.75f;    // 피해 감소율
    public float duration = 2f;             // 배리어 지속 시간
    public float explosionRadius = 3f;      // 폭발 반경
    public float explosionDamage = 60f;     // 폭발 피해량
    public LayerMask enemyLayer;            // 적 레이어

    private bool _isActive = false;
    private Transform _owner;               // 배리어를 소유한 대상

    public void Initialize(Transform owner)
    {
        _owner = owner;
        transform.position = owner.position;
        _isActive = true;

        // 시작 시 배리어 시각 효과를 활성화하거나 파티클 추가 가능
        Invoke(nameof(Explode), duration); // duration 후 폭발 실행
    }

    private void Update()
    {
        if (_isActive && _owner != null)
        {
            // 배리어가 주인을 따라다니도록 설정
            transform.position = _owner.position;
        }
    }

    private void Explode()
    {
        _isActive = false;

        // 폭발 효과 (파티클 추가 가능)
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);
        foreach (var collider in hitColliders)
        {
            var enemyHealth = collider.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(explosionDamage);
            }
        }

        Destroy(gameObject); // 배리어 오브젝트 제거
    }

    private void OnDrawGizmosSelected()
    {
        // 폭발 반경을 시각적으로 확인 (디버깅용)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
