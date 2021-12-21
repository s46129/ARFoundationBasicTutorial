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

        [SerializeField] private float ShootForce = 1;

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

        public void StopPout()
        {
            isStartShoot = false;
        }

        void Shoot()
        {
            Transform get = targetPool.Spawn();
            get.position = ShootPosition.position;
            get.gameObject.SetActive(true);
            get.GetComponent<TargetController>().Init(this, 3);
            get.GetComponent<Rigidbody>().AddForce(ShootPosition.forward * ShootForce);
        }

        public void OnHited(TargetController target)
        {
            GameManager.instance.AddScore(100);
            Recycle(target);
        }

        public void Recycle(TargetController target)
        {
            target.gameObject.SetActive(false);
            targetPool.Recycle(target.transform);
            target.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}