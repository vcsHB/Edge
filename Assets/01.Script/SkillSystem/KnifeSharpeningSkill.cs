using UnityEngine;

public class KnifeSharpeningSkill : Skill
{
    public float cooldown = 13f;                // ��ų ��Ÿ��
    public float boostedDamage = 60f;           // ��ȭ�� ���ط�
    public float duration = 5f;                 // ���� �ð�
    [SerializeField] private KnifeSharpening _knifeSharpeningPrefab; // KnifeSharpening ������

    private PlayerAttack _playerAttack;         // �÷��̾��� ���� ������Ʈ

    private void Awake()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        if (_playerAttack == null)
        {
            Debug.LogError("PlayerAttack ������Ʈ�� ������� �ʾҽ��ϴ�.");
        }
    }

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;

        ActivateKnifeSharpening();
        return true;
    }

    private void ActivateKnifeSharpening()
    {
        // KnifeSharpening ������Ʈ ����
        KnifeSharpening knifeSharpening = Instantiate(_knifeSharpeningPrefab, transform.position, Quaternion.identity);
        knifeSharpening.Initialize(_playerAttack); // �÷��̾��� ���ݷ� ����
    }
}
