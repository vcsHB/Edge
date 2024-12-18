using InputManage;
using UnityEngine;

public class SordBoomerangSkill : Skill
{
    public float cooldown = 11f;           // ��ų ��Ÿ��
    public float boomerangDuration = 1f;   // �θ޶��� ��ü ���� �ð�
    public float damage = 30f;             // �θ޶� ���ط�
    public AnimationCurve easeCurve;       // �θ޶��� ������ �
    public LayerMask enemyLayer;           // �� ���̾�
    [SerializeField] private SordBoomerang _boomerangPrefab; // �θ޶� ������
    [SerializeField] private PlayerInput _playerInput;       

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

        LaunchBoomerang();
        return true;
    }

    private void LaunchBoomerang()
    {
        Vector3 startPosition = transform.position; // �÷��̾� ��ġ
        Vector3 targetPosition = _playerInput.MousePosition; // ���콺 ��ġ

        SordBoomerang boomerang = Instantiate(_boomerangPrefab, startPosition, Quaternion.identity);
        boomerang.Initialize(targetPosition, transform);
        boomerang.damage = damage;
        boomerang.duration = boomerangDuration;
        boomerang.easeCurve = easeCurve;
        boomerang.enemyLayer = enemyLayer;
    }
}
