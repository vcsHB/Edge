using InputManage;
using UnityEngine;

public class SordBoomerangSkill : Skill
{
//    �� ����� ����ϴ� ���� ���콺�� �������� ������.���� �θ޶�ó�� ȸ���ϸ� �߻�(�ִϸ��̼����� ���� ȸ���ϴ°� �����, ������Ʈ�� 2���Լ��� y�� ��ȭ�� �������� ������.), ���콺�� �������� ���ư��ٰ� ���ƿ´�.(0.5�ʵ��� ���ư��ٰ� 0.5�ʵ��� ���ƿ�)(�̰� ���õ� Ease�� �ʿ���. 2���Լ� ����..)
//  �浹�ϴ� ��� ������ 30�� ���ظ� �ش�. (���ĳ��� ����.)
//  ��Ÿ�� 11��
    public float boomerangDuration = 1f;   
    public float damage = 30f;             // �θ޶� ���ط�
    public AnimationCurve easeCurve;       // 2���Լ�?
    [SerializeField] private SordBoomerang _boomerangPrefab; // �θ޶� ������
    [SerializeField] private PlayerInput _playerInput;       

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;

        LaunchBoomerang();
        return true;
    }

    private void LaunchBoomerang()
    {
        Vector3 startPosition = GameObject.Find("Player").transform.position; // �÷��̾� ��ġ
        Vector3 targetPosition = _playerInput.MousePosition; // ���콺 ��ġ
        

        SordBoomerang boomerang = Instantiate(_boomerangPrefab, startPosition, Quaternion.identity); 
        boomerang.Initialize(targetPosition);
        boomerang.damage = damage;
        boomerang.duration = boomerangDuration;
        boomerang.easeCurve = easeCurve;
        boomerang.enemyLayer = whatIsEnemy;
    }
}
