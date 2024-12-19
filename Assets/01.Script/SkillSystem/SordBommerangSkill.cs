using InputManage;
using UnityEngine;

public class SordBoomerangSkill : Skill
{
//    주 무기로 사용하는 검을 마우스의 방향으로 던진다.검은 부메랑처럼 회전하며 발사(애니메이션으로 검이 회전하는거 만들고, 오브젝트를 2차함수의 y값 변화량 느낌으로 던진다.), 마우스의 방향으로 날아갔다가 돌아온다.(0.5초동안 날아갔다가 0.5초동안 돌아옴)(이것 역시도 Ease가 필요함. 2차함수 참고..)
//  충돌하는 모든 적에게 30의 피해를 준다. (밀쳐내진 않음.)
//  쿨타임 11초
    public float boomerangDuration = 1f;   
    public float damage = 30f;             // 부메랑 피해량
    public AnimationCurve easeCurve;       // 2차함수?
    [SerializeField] private SordBoomerang _boomerangPrefab; // 부메랑 프리팹
    [SerializeField] private PlayerInput _playerInput;       

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;

        LaunchBoomerang();
        return true;
    }

    private void LaunchBoomerang()
    {
        Vector3 startPosition = GameObject.Find("Player").transform.position; // 플레이어 위치
        Vector3 targetPosition = _playerInput.MousePosition; // 마우스 위치
        

        SordBoomerang boomerang = Instantiate(_boomerangPrefab, startPosition, Quaternion.identity); 
        boomerang.Initialize(targetPosition);
        boomerang.damage = damage;
        boomerang.duration = boomerangDuration;
        boomerang.easeCurve = easeCurve;
        boomerang.enemyLayer = whatIsEnemy;
    }
}
