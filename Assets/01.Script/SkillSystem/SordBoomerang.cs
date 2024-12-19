using Agents;
using UnityEngine;

//주 무기로 사용하는 검을 마우스의 방향으로 던진다.
//검은 부메랑처럼 회전하며 발사(애니메이션으로 검이 회전하는거 만들고, 오브젝트를 2차함수의 y값 변화량 느낌으로 던진다.), 마우스의 방향으로 날아갔다가 돌아온다.(0.5초동안 날아갔다가 0.5초동안 돌아옴)(이것 역시도 Ease가 필요함. 2차함수 참고..)
//충돌하는 모든 적에게 30의 피해를 준다.
//쿨타임 11초
public class SordBoomerang : MonoBehaviour
{
    public float damage = 30f;             // 충돌 시 피해량
    public float duration = 1f;           // 전체 비행 시간 (0.5초 앞으로, 0.5초 뒤로)
    public AnimationCurve easeCurve;      // 검 회오리 애니메이션 커브
    public LayerMask enemyLayer;          // 적 레이어
    SpriteRenderer spriteRenderer;

    private Vector2 _startPosition;       // 시작 위치
    private Vector2 _targetPosition;      // 목표 위치
    private float _elapsedTime = 0f;      // 경과 시간
    private bool _isReturning = false;    // 현재 돌아오는 중인지 여부

    public void Initialize(Vector2 targetPosition)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _startPosition = transform.position;
        _targetPosition = targetPosition;
        if(targetPosition.x - _startPosition.x <= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        float t = _elapsedTime / (duration * 0.5f); // 이동 시간 비율
        t = Mathf.Clamp01(t);
        float easedT = easeCurve.Evaluate(t); // Ease 처리

        if (_isReturning)
        {
            // 돌아오는 경우
            transform.position = Vector2.Lerp(_targetPosition, _startPosition, easedT);
            if (t >= 1f) Destroy(gameObject); // 돌아오면 삭제
        }
        else
        {
            // 앞으로 날아가는 경우
            transform.position = Vector2.Lerp(_startPosition, _targetPosition, easedT);
            if (t >= 1f)
            {
                _isReturning = true;
                _elapsedTime = 0f; // 시간 초기화
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 적 레이어 감지
        if ((enemyLayer & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("부메랑");
            var enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(damage);
            }
        }
    }
}
