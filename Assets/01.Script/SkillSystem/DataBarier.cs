using Agents;
using UnityEngine;

public class DataBarier : MonoBehaviour
{
    public float damageReduction = 0.75f;    // ���� ������
    public float duration = 2f;             // �踮�� ���� �ð�
    public float explosionRadius = 3f;      // ���� �ݰ�
    public float explosionDamage = 60f;     // ���� ���ط�
    public LayerMask enemyLayer;            // �� ���̾�

    private bool _isActive = false;
    private Transform _owner;               // �踮� ������ ���

    public void Initialize(Transform owner)
    {
        _owner = owner;
        transform.position = owner.position;
        _isActive = true;

        // ���� �� �踮�� �ð� ȿ���� Ȱ��ȭ�ϰų� ��ƼŬ �߰� ����
        Invoke(nameof(Explode), duration); // duration �� ���� ����
    }

    private void Update()
    {
        if (_isActive && _owner != null)
        {
            // �踮� ������ ����ٴϵ��� ����
            transform.position = _owner.position;
        }
    }

    private void Explode()
    {
        _isActive = false;

        // ���� ȿ�� (��ƼŬ �߰� ����)
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);
        foreach (var collider in hitColliders)
        {
            var enemyHealth = collider.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(explosionDamage);
            }
        }

        Destroy(gameObject); // �踮�� ������Ʈ ����
    }

    private void OnDrawGizmosSelected()
    {
        // ���� �ݰ��� �ð������� Ȯ�� (������)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
