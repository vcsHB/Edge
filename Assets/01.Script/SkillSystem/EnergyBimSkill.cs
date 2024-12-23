using UnityEngine;
using InputManage;
using Agents.Players;
//���콺�� �������� ���������� �߻�. ��
//���������� ���������̸� ���� ���� ������ 80�� ���ظ� �ְ�, ������ ���� ���ĳ���. ��
//���������� ���� �߿� �̵��� �Ұ����ϴ�. (�����ð� 0.75��) (0.1�� ��ȯ, 0.4�ʵ��� ���� �����ǰ� 0.25�ʵ��� ���� �����.) 
//��Ÿ�� 12��
public class EnergyBimSkill : Skill
{
    public float damagePerSecond = 80f; // �ʴ� ���ط�
    public float duration = 0.75f;      // �� ��ü ���� �ð�
    public float knockbackForce = 10f;  // ���ĳ��� ��
    public float bimLength = 50f;

    [SerializeField] private EnergyBim _energyBimPrefab;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMover _playerMover;

    private bool _isSkillActive = false;

    public override bool UseSkill()
    {
        if (!base.UseSkill() || _isSkillActive) return false;

        _isSkillActive = true;
        _playerMover.canMove = false;

        SpawnEnergyBim();
        return true;
    }

    private void SpawnEnergyBim()
    {
        Vector3 start = GameObject.Find("Player").transform.position;
        Vector3 mouseWorldPosition = _playerInput.MousePosition;
        Vector3 direction = (mouseWorldPosition - start).normalized;
        Vector3 end = start + direction * bimLength; // �� �ִ� ��Ÿ� 10

        EnergyBim energyBim = Instantiate(_energyBimPrefab, start, Quaternion.identity);
        energyBim.Initialize(damagePerSecond, duration, knockbackForce, whatIsEnemy, start, end);

        // ��ų ���� �ð� ���� �÷��̾� �̵� �Ұ�
        Invoke(nameof(EnablePlayerMovement), duration);
    }

    private void EnablePlayerMovement()
    {
        _playerMover.canMove = true;
    }


}