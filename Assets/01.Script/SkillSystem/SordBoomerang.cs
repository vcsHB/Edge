using Agents;
using UnityEngine;

public class SordBoomerang : MonoBehaviour
{
    public float damage = 30f;             // 충돌 시 피해량
    public float duration = 1f;           // 전체 비행 시간 (0.5초 앞으로, 0.5초 뒤로)
    public AnimationCurve easeCurve;      // 검 회오리
    public LayerMask enemyLayer;          
    private Transform owner;               // 검을 던지는 주체 (플레이어)

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _elapsedTime = 0f;
    private bool _isReturning = false;    // 현재 돌아오는 중인지 여부

    public void Initialize(Vector3 targetPosition)
    {
        _startPosition = transform.position;
        _targetPosition = targetPosition;
        owner = GameObject.Find("Player").GetComponent<Transform>();
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
            transform.position = Vector3.Lerp(_targetPosition, _startPosition, easedT);
            if (t >= 1f) Destroy(gameObject);
        }
        else
        {
            // 앞으로 날아가는 경우
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, easedT);
            if (t >= 1f)
            {
                _isReturning = true;
                _elapsedTime = 0f; // 시간 초기화
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((enemyLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            var enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(damage);
            }
        }
    }
}
