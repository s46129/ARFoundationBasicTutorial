namespace ARTutorial.Utility
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private List<Transform> onHierarchyObjects = new List<Transform>();
        [SerializeField] private List<Transform> onPoolObjects = new List<Transform>();
        private GameObject prefab;

        public void Initial(GameObject Prefab)
        {
            prefab = Prefab;
        }

        public Transform Spawn()
        {
            Transform get;
            if (onPoolObjects.Count > 0)
            {
                get = onPoolObjects[0];
            }
            else
            {
                get = Instantiate(prefab).transform;
            }

            onHierarchyObjects.Add(get);
            if (onPoolObjects.Contains(get))
            {
                onPoolObjects.Remove(get);
            }


            return get;
        }

        public void Recycle(Transform target)
        {
            onPoolObjects.Add(target);
            onHierarchyObjects.Remove(target);
        }
    }
}