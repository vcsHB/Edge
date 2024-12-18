using Unity.VisualScripting;
using UnityEngine;

namespace suhyun
{
    public class Arrow : MonoBehaviour
    {
        private float speed = 3;
        void Update()
        {

            Vector2 dir = GameObject.Find("Enemy").transform.position - transform.position;
            transform.Translate(dir.normalized * Time.deltaTime * speed);
            // ø°≥ πÃ ¥Í¿∏∏È ªÁ∂Û¡¸


        }
        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.name == "Enemy")
                Destroy(gameObject);
        }
    }
}

