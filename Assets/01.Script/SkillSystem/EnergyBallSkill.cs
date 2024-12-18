using UnityEngine;
using InputManage; // PlayerInput ���ӽ����̽� ����

public class EnergyBallSkill : Skill
{
    public int damage = 30;         // ���ط�
    public float speed = 15f;      // �ӵ�
    public float lifetime = 1.5f;  // ����
    public float cooldown = 10f;   // ��Ÿ��
    public LayerMask whatIsEnemy;  // �� ���̾�
    [SerializeField] private EnergyBall _energyBallPrefab;
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

        SpawnEnergyBall();
        return true;
    }

    private void SpawnEnergyBall()
    {
        Vector3 playerPosition = transform.position;
        Vector3 mouseWorldPosition = _playerInput.MousePosition;

        Vector3 direction = (mouseWorldPosition - playerPosition).normalized;

        EnergyBall energyBall = Instantiate(_energyBallPrefab, playerPosition, Quaternion.identity);
        energyBall.Initialize(damage, speed, lifetime, whatIsEnemy, direction);
    }
}
