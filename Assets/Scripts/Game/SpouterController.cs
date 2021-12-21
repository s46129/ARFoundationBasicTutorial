using System;
using ARTutorial.Utility;

namespace ARTutorial.ShootGame
{
    using UnityEngine;

    public class SpouterController : MonoBehaviour
    {
        /// <summary>
        /// 射出子彈的物件池
        /// </summary>
        [SerializeField] private ObjectPool targetPool;

        /// <summary>
        /// 射出的子彈Prefab
        /// </summary>
        [SerializeField] private GameObject targetPrefab;

        /// <summary>
        /// 發射位置
        /// </summary>
        [SerializeField] private Transform ShootPosition;

        private bool isStartShoot = false;

        /// <summary>
        /// 間隔時間
        /// </summary>
        public float Interval;

        private float tempTime;

        public void OnEnable()
        {
            GameManager.instance.OnSpouterEnable(this);
        }

        private void OnDisable()
        {
            isStartShoot = false;
        }

        private void Update()
        {
            if (isStartShoot == false)
            {
                return;
            }

            tempTime -= Time.deltaTime;
            if (tempTime <= 0)
            {
                Shoot();
                tempTime = Interval;
            }
        }

        public void StartPout()
        {
            tempTime = Interval;
            isStartShoot = true;
            targetPool.Initial(targetPrefab);
        }

        void Shoot()
        {
            GameObject get = targetPool.GetObject();
            get.transform.position = ShootPosition.position;
            get.GetComponent<TargetController>().Init(this, 3);
            get.GetComponent<Rigidbody>().AddForce(ShootPosition.forward);
        }

        public void OnHited(TargetController target)
        {
            //TODO: 加分數
            ResponseTarget(target);
        }

        public void ResponseTarget(TargetController target)
        {
            targetPool.Response(target.gameObject);
            target.GetComponent<Rigidbody>().velocity=Vector3.zero;
            target.gameObject.SetActive(false);
        }
    }
}