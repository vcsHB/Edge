using UnityEngine;
using InputManage;

// �������� ��ų: ���콺 �������� ���������� �߻��ϰ�, ��λ� �浹�ϴ� ��� ������ ���ظ� �ش�.
// ������ ���ظ� �� �ڿ��� ���������� ������ �ʰ�, �ӵ��� ���� �������� �������.
// ���������� ������ ���� �̵��ӵ��� 25% ���ҽ�Ų��. (1.5�� ����)
// ��Ÿ�� 6��
public class EnergyBallSkill : Skill
{
    public int damage = 4;          // ���ط�
    public float speed = 10f;        // �ʱ� �ӵ�
    public float lifetime = 1.5f;    // �������� ���ӽð�
    public float slowDuration = 1.5f; // �� �̵� �ӵ� ���� ���� �ð�
    public float slowAmount = 0.25f; // �������� �̵� �ӵ� ������

    [SerializeField] private EnergyBall _energyBallPrefab;
    [SerializeField] private Transform _playerTrm;
    [SerializeField] private PlayerInput _playerInput;

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;
        SpawnEnergyBall();
        return true;
    }

    private void SpawnEnergyBall()
    {
        Vector3 playerPosition = _playerTrm.position;
        Vector3 mouseWorldPosition = _playerInput.MousePosition;

        // ���콺�������� ���ư�
        Vector3 direction = (mouseWorldPosition - playerPosition).normalized;

        // �������� ����
        EnergyBall energyBall = Instantiate(_energyBallPrefab, playerPosition, Quaternion.identity);
        energyBall.Initialize(damage, speed, lifetime, whatIsEnemy, direction, slowAmount, slowDuration);
    }
}
