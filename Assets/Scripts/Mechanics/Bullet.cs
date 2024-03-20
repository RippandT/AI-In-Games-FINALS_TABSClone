using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace DesignPatterns.ObjectPool {
    public class Bullet : MonoBehaviour {
        // deactivate after delay
        [SerializeField] private float timeoutDelay = 5f;

        public float damage = 0.0f;
        public string enemyTag;
        public GameObject whoShotIt;

        private IObjectPool<Bullet> objectPool;

        // public property to give the projectile a reference to its ObjectPool
        public IObjectPool<Bullet> ObjectPool { set => objectPool = value; }

        private void Start()
        {
            StartCoroutine(DeactivateRoutine(timeoutDelay));
        }

        public void Deactivate()
        {
            Debug.Log("Bullet Deactivated");

            // reset the moving Rigidbody
            Rigidbody rBody = GetComponent<Rigidbody>();
            rBody.velocity = new Vector3(0f, 0f, 0f);
            rBody.angularVelocity = new Vector3(0f, 0f, 0f);

            // release the projectile back to the pool
            objectPool.Release(this);
        }

        IEnumerator DeactivateRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            Deactivate();
        }

        void OnTriggerEnter(Collider other) {
            GameObject whatIHit = other.gameObject;

            // Indiscriminate bullet LMAO
            UnitManager unit = whatIHit.GetComponent<UnitManager>();
            if (unit != null)
            {
                if (other.CompareTag(enemyTag))
                    unit.currentTarget = whoShotIt;
                unit.health -= damage;
            }

            Deactivate();
        }
    }
}
