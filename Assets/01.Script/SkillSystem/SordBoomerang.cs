using Agents;
using UnityEngine;

public class SordBoomerang : MonoBehaviour
{
    public float damage = 30f;             // �浹 �� ���ط�
    public float duration = 1f;           // ��ü ���� �ð� (0.5�� ������, 0.5�� �ڷ�)
    public AnimationCurve easeCurve;      // �� ȸ���� �ִϸ��̼� Ŀ��
    public LayerMask enemyLayer;          // �� ���̾�
    private Transform owner;              // ���� ������ ��ü (�÷��̾�)

    private Vector2 _startPosition;       // ���� ��ġ
    private Vector2 _targetPosition;      // ��ǥ ��ġ
    private float _elapsedTime = 0f;      // ��� �ð�
    private bool _isReturning = false;    // ���� ���ƿ��� ������ ����

    public void Initialize(Vector2 targetPosition)
    {
        _startPosition = transform.position;
        _targetPosition = targetPosition;
        owner = GameObject.Find("Player").transform;
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
            transform.position = Vector2.Lerp(_targetPosition, _startPosition, easedT);
            if (t >= 1f) Destroy(gameObject); // ���ƿ��� ����
        }
        else
        {
            // ������ ���ư��� ���
            transform.position = Vector2.Lerp(_startPosition, _targetPosition, easedT);
            if (t >= 1f)
            {
                _isReturning = true;
                _elapsedTime = 0f; // �ð� �ʱ�ȭ
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("��-�޶�");
        // �� ���̾� ����
        if ((enemyLayer & (1 << other.gameObject.layer)) > 0)
        {
            var enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(damage);
            }
        }
    }
}
