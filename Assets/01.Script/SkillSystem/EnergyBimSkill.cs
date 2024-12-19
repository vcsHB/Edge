using UnityEngine;
using InputManage; // PlayerInput ���ӽ����̽� ����

//���콺�� �������� ���������� �߻�.���������� ���������̸� ���� ���� ������ 80�� ���ظ� �ְ�, 
//������ ���� ���ĳ���.
//�������� ���� �߿� �÷��̾� �̵� �Ұ���
//���ӽð� 0.75�� (0.1�� ��ȯ, 0.4�ʵ��� ���� �����ǰ� 0.25�ʵ��� ���� �����.) 
//��Ÿ�� 12��

public class EnergyBimSkill : Skill
{
    public float damagePerSecond = 50f; // �ʴ� ���ط�
    public float duration = 0.75f;         // �� ���� �ð�
    [SerializeField] private EnergyBim _energyBimPrefab;
    [SerializeField] private PlayerInput _playerInput; // PlayerInput ����

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;

        SpawnEnergyBim();
        return true;
    }

    private void SpawnEnergyBim()
    {
        Vector3 start = GameObject.Find("Player").transform.position;
        Vector3 mouseWorldPosition = _playerInput.MousePosition;

        Vector3 direction = (mouseWorldPosition - start).normalized;
        Vector3 end = start + direction * 10f; // �� �ִ� ��Ÿ� 10

        EnergyBim energyBim = Instantiate(_energyBimPrefab, start, Quaternion.identity);
        energyBim.Initialize(damagePerSecond, duration, whatIsEnemy, start, end);
    }
}
