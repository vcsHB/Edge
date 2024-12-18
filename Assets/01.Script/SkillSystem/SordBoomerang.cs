using Agents;
using UnityEngine;

public class SordBoomerang : MonoBehaviour
{
    public float damage = 30f;             // �浹 �� ���ط�
    public float duration = 1f;           // ��ü ���� �ð� (0.5�� ������, 0.5�� �ڷ�)
    public AnimationCurve easeCurve;      // �� ȸ����
    public LayerMask enemyLayer;          
    private Transform owner;               // ���� ������ ��ü (�÷��̾�)

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _elapsedTime = 0f;
    private bool _isReturning = false;    // ���� ���ƿ��� ������ ����

    public void Initialize(Vector3 targetPosition)
    {
        _startPosition = transform.position;
        _targetPosition = targetPosition;
        owner = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        float t = _elapsedTime / (duration * 0.5f); // �̵� �ð� ����
        t = Mathf.Clamp01(t);
        float easedT = easeCurve.Evaluate(t); // Ease ó��

        if (_isReturning)
        {
            // ���ƿ��� ���
            transform.position = Vector3.Lerp(_targetPosition, _startPosition, easedT);
            if (t >= 1f) Destroy(gameObject);
        }
        else
        {
            // ������ ���ư��� ���
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, easedT);
            if (t >= 1f)
            {
                _isReturning = true;
                _elapsedTime = 0f; // �ð� �ʱ�ȭ
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
