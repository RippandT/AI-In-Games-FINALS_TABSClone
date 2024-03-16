using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace DesignPatterns.ObjectPool {
    public class GunProjectile : MonoBehaviour {
        [Tooltip("Prefab to shoot")]
        [SerializeField] private Bullet bullet;

        [Tooltip("Projectile force")]
        [SerializeField] private float muzzleVelocity = 700f;

        [Tooltip("End point of gun where shots appear")]
        [SerializeField] private Transform muzzlePosition;

        // stack-based ObjectPool available with Unity 2021 and above
        private IObjectPool<Bullet> objectPool;

        // throw an exception if we try to return an existing item, already in the pool
        [SerializeField] private bool collectionCheck = true;

        // extra options to control the pool capacity and maximum size
        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxSize = 100;

        UnitManager unit;
        public int maxAmmo = 5;
        public int ammoPerMagazine = 5;

        private void Awake()
        {
            objectPool = new ObjectPool<Bullet>(CreateProjectile,
                    OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                    collectionCheck, defaultCapacity, maxSize);
        }

        private void Start()
        {
            unit = GetComponentInParent<UnitManager>();
        }

        // invoked when creating an item to populate the object pool
        private Bullet CreateProjectile()
        {
            Bullet projectileInstance = Instantiate(bullet);
            projectileInstance.ObjectPool = objectPool;
            return projectileInstance;
        }

        // invoked when returning an item to the object pool
        private void OnReleaseToPool(Bullet pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }

        // invoked when retrieving the next item from the object pool
        private void OnGetFromPool(Bullet pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
        private void OnDestroyPooledObject(Bullet pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }

        void Shoot() {
            // Don't even shoot when there's no more ammo in the magazine
            if (ammoPerMagazine <= 0)
                return;

            // Spend shot ammo
            ammoPerMagazine -= 1;

            // get a pooled object instead of instantiating
            Bullet bulletObject = objectPool.Get();

            if (bulletObject == null)
                return;

            // Assign damage to bullet
            bulletObject.damage = unit.damage;

            // align to gun barrel/muzzle position
            bulletObject.transform.SetPositionAndRotation(muzzlePosition.position, unit.transform.rotation);

            // move projectile forward
            bulletObject.GetComponent<Rigidbody>().AddForce(bulletObject.transform.forward * muzzleVelocity, ForceMode.Acceleration);
        }
    }
}
