using Agents;
using UnityEngine;
using System.Collections;

public class DataBarierSkill : Skill
{
    public float duration = 2f;             // �踮�� ���� �ð�
    public float damageReduction = 0.75f;   // ���� ������
    public float explosionRadius = 3f;      // ���� �ݰ�
    public float explosionDamage = 60f;     // ���� ���ط�
    [SerializeField] private DataBarier _barierPrefab; // �踮�� ������

    private Health _playerHealth;           // �÷��̾��� ü�� ������Ʈ

    private void Awake()
    {
        _playerHealth = GameObject.Find("Player").GetComponent<Health>();
        if (_playerHealth == null)
        {
            Debug.LogError("Health ������Ʈ�� ������� ����.");
        }
    }

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;

        ActivateBarier();
        return true;
    }

    private void ActivateBarier()
    {
        // �踮�� ����
        DataBarier barier = Instantiate(_barierPrefab, transform.position, Quaternion.identity);
        barier.Initialize(transform); // �÷��̾ �����ڷ� ����
        barier.damageReduction = damageReduction;
        barier.duration = duration;
        barier.explosionRadius = explosionRadius;
        barier.explosionDamage = explosionDamage;
        barier.enemyLayer = LayerMask.GetMask("Enemy"); // �� ���̾� ����

        // 2�� ���� ���� ���� ȿ�� ����
        StartCoroutine(ApplyDamageReduction(duration));
    }

    private IEnumerator ApplyDamageReduction(float duration)
    {
        

        yield return new WaitForSeconds(duration);

    }

}
