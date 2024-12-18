using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace WaveSystem
{
    public class WHTestEnemy : MonoBehaviour
    {
        public event Action<WHTestEnemy> OnDieEvent;
        void Start()
        {
            StartCoroutine(PlayerChycle());
        }

        private IEnumerator PlayerChycle()
        {
            yield return new WaitForSeconds(10f);
            OnDieEvent?.Invoke(this);
            Destroy(gameObject);
        }
    }
}

