using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace DesignPatterns.ObjectPool {
    public class ManagerHealer : MonoBehaviour {
        [Tooltip("Prefab to shoot")]
        [SerializeField] private Cash cash;

        [Tooltip("Projectile force")]
        [SerializeField] private float muzzleVelocity = 700f;

        [Tooltip("End point of gun where shots appear")]
        [SerializeField] private Transform muzzlePosition;
        public GameObject cashInHand;

        // stack-based ObjectPool available with Unity 2021 and above
        private IObjectPool<Cash> objectPool;

        // throw an exception if we try to return an existing item, already in the pool
        [SerializeField] private bool collectionCheck = true;

        // extra options to control the pool capacity and maximum size
        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxSize = 100;

        // Custom calls for healer/manager
        UnitManager unit;
        UnitList list;

        private void Awake()
        {
            objectPool = new ObjectPool<Cash>(CreateProjectile,
                    OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                    collectionCheck, defaultCapacity, maxSize);
        }

        private void Start()
        {
            unit = GetComponentInParent<UnitManager>();
            list = GameObject.FindWithTag(unit.enemyTeamCount.ToString()).GetComponent<UnitList>();
        }

        // invoked when creating an item to populate the object pool
        private Cash CreateProjectile()
        {
            Cash projectileInstance = Instantiate(cash);
            projectileInstance.ObjectPool = objectPool;
            return projectileInstance;
        }

        // invoked when returning an item to the object pool
        private void OnReleaseToPool(Cash pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }

        // invoked when retrieving the next item from the object pool
        private void OnGetFromPool(Cash pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
        private void OnDestroyPooledObject(Cash pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }

        void Shoot() {
            // get a pooled object instead of instantiating
            Cash cashObject = objectPool.Get();

            if (cashObject == null)
                return;

            // Assign damage to bullet
            cashObject.damage = unit.damage;

            // align to gun barrel/muzzle position
            cashObject.transform.position = (muzzlePosition.position);//unit.cur.rotation);
            cashObject.transform.LookAt(unit.currentTarget.transform);

            // move projectile forward
            cashObject.GetComponent<Rigidbody>().AddForce(cashObject.transform.forward * muzzleVelocity, ForceMode.Acceleration);

            cashInHand.SetActive(false);
        }

        void RetrieveMoney()
        {
            cashInHand.SetActive(true);
        }

        public void SetTargetPriority()
        {
            List<GameObject> unitPriority = new List<GameObject>(list.unitsInATeam);

            if (unitPriority.Count == 0)
                return;

            unit.agent.ResetPath();

            unitPriority.Remove(unit.gameObject);
            unitPriority = unitPriority.OrderBy(e => e.GetComponent<UnitManager>().health - e.GetComponent<UnitManager>().maxHealth).ToList();

            unit.currentTarget = unitPriority[0];//GameObject.FindWithTag(unit.teamAffliation.ToString());
        }
    }
}
