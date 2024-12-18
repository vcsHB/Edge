using UnityEngine;
using InputManage; // PlayerInput ���ӽ����̽� ����

public class EnergyBimSkill : Skill
{
    public float damagePerSecond = 50f; // �ʴ� ���ط�
    public float duration = 2f;         // �� ���� �ð�
    public float cooldown = 8f;         // ��Ÿ��
    public LayerMask whatIsEnemy;       // �� ���̾�
    [SerializeField] private EnergyBim _energyBimPrefab;
    [SerializeField] private PlayerInput _playerInput; // PlayerInput ����

    private void Awake()
    {
        if (_playerInput == null)
        {
            Debug.LogError("PlayerInput�� ������� �ʾҽ��ϴ�.");
        }
    }

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;

        SpawnEnergyBim();
        return true;
    }

    private void SpawnEnergyBim()
    {
        Vector3 start = transform.position;
        Vector3 mouseWorldPosition = _playerInput.MousePosition;

        Vector3 direction = (mouseWorldPosition - start).normalized;
        Vector3 end = start + direction * 10f; // �� �ִ� ��Ÿ� 10

        EnergyBim energyBim = Instantiate(_energyBimPrefab, start, Quaternion.identity);
        energyBim.Initialize(damagePerSecond, duration, whatIsEnemy, start, end);
    }
}
