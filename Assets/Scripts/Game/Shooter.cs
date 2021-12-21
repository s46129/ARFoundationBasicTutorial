using System;
using UnityEditor;

namespace ARTutorial.ShootGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class Shooter : MonoBehaviour
    {
        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                touchPosition = Input.mousePosition;
                return true;
            }
#else
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }
#endif
            touchPosition = default;
            return false;
        }

        private Camera mainCamera;

        [SerializeField] private float MaxDistance = 15;
        [SerializeField] private LayerMask _layerMask;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        void Update()
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

#if UNITY_EDITOR
            if (EventSystem.current.IsPointerOverGameObject() == true)
                return;
#else
            if (Input.touchCount > 0 &&
                EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == true)
                return;
#endif


#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
#else
            if (Input.GetTouch(0).phase == TouchPhase.Began)
#endif
            {
                Ray ray = mainCamera.ScreenPointToRay(touchPosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, MaxDistance, _layerMask))
                {
                    hitInfo.transform.GetComponent<OnShootHitedEvent>().Hited();
                }
            }
        }
    }
}