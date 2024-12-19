using Agents;
using UnityEngine;
using System.Collections;

public class DataBarierSkill : Skill
{
    //�踮� ��ȯ�ϰ� "2�ʵ��� �÷��̾ �޴� ��� ���ذ� 75% �����Ѵ�". 2�� ���� �ݰ� 3 ���� �� ������ 60�� ���ظ� ���Ѵ�. ��Ÿ�� 10��

    public float duration = 2f;             // �踮�� ���� �ð�
    public float damageReduction = 0.75f;   // ���� ������ 75��
    public float explosionRadius = 3f;      // ���� �ݰ�(����3)
    public float explosionDamage = 60f;     // ���� ���ط�(60)
    private Transform playerTrm;
    [SerializeField] private DataBarier _barierPrefab; // �踮�� ������

    private Health _playerHealth;           // �÷��̾��� ü�� ������Ʈ

    private void Awake()
    {
        playerTrm = GameObject.Find("Player").transform;
        _playerHealth = playerTrm.GetComponent<Health>();
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
        DataBarier barier = Instantiate(_barierPrefab, playerTrm.position, Quaternion.identity);
        barier.Initialize(playerTrm);
        barier.damageReduction = damageReduction;
        barier.duration = duration;
        barier.explosionRadius = explosionRadius;
        barier.explosionDamage = explosionDamage;
        barier.enemyLayer = whatIsEnemy;

        // 2�� ���� �÷��̾ �޴� ���� ���� ȿ�� ����
        StartCoroutine(ApplyDamageReduction(duration));
    }

    private IEnumerator ApplyDamageReduction(float duration)
    {
        Debug.Log("�޴� ���� ����");
        yield return new WaitForSeconds(duration);

    }

}
