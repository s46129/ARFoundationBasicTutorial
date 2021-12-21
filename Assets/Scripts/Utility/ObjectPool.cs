namespace ARTutorial.Utility
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ObjectPool : MonoBehaviour
    {
        private List<GameObject> onHierarchyObjects = new List<GameObject>();
        private List<GameObject> onPoolObjects = new List<GameObject>();
        private GameObject prefab;

        public void Initial(GameObject Prefab)
        {
            prefab = Prefab;
        }

        public GameObject GetObject()
        {
            GameObject get;
            if (onPoolObjects.Count > 0)
            {
                get = onPoolObjects[0];
                Debug.Log($"onPoolObjects.Count {onPoolObjects.Count}");
            }
            else
            {
                get = Instantiate(prefab);
                Debug.Log("Instantiate");
            }

            onHierarchyObjects.Add(get);
            if (onPoolObjects.Contains(get))
            {
                onPoolObjects.Remove(get);
            }


            return get;
        }

        public void Response(GameObject target)
        {
            onPoolObjects.Add(target);
            onHierarchyObjects.Remove(target);
        }
    }
}