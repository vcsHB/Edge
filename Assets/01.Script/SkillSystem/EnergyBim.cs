
using Agents;
using UnityEngine;
using System.Collections;


public class EnergyBim : MonoBehaviour
{
    private float _damagePerSecond;
    private float _duration;
    private float _knockbackForce;
    private LayerMask _whatIsEnemy;
    private LineRenderer _lineRenderer;

    public void Initialize(float damagePerSecond, float duration, float knockbackForce, LayerMask whatIsEnemy, Vector3 start, Vector3 end)
    {
        _damagePerSecond = damagePerSecond;
        _duration = duration;
        _knockbackForce = knockbackForce;
        _whatIsEnemy = whatIsEnemy;

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);

        // ºö ´Ü°è ¿¬Ãâ
        StartCoroutine(BimLifecycle(start, end));
    }

    private IEnumerator BimLifecycle(Vector3 start, Vector3 end)
    {
        // 0.1ÃÊ ¼ÒÈ¯
        yield return new WaitForSeconds(0.1f);

        // 0.4ÃÊ µ¿¾È ºö À¯Áö
        float activeTime = 0.4f;
        float elapsedTime = 0f;
        while (elapsedTime < activeTime)
        {
            DamageAndKnockbackEnemies(start, end);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 0.25ÃÊ µ¿¾È ºö »ç¶óÁü
        float fadeTime = 0.25f;
        _lineRenderer.enabled = false;
        yield return new WaitForSeconds(fadeTime);

        Destroy(gameObject);
    }

    private void DamageAndKnockbackEnemies(Vector3 start, Vector3 end)
    {
        Debug.Log("³Ë¹é");
        Vector2 direction = (end - start).normalized;
        float distance = Vector2.Distance(start, end);
        float lineWidth = _lineRenderer.startWidth / 2f;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(start, lineWidth, direction, distance, _whatIsEnemy);
        foreach (var hit in hits)
        {
            var enemyHealth = hit.collider.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(_damagePerSecond * Time.deltaTime);
            }

            var enemyRigidbody = hit.collider.GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                Vector2 knockbackDirection = (enemyRigidbody.transform.position - start).normalized;
                enemyRigidbody.AddForce(knockbackDirection * _knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
