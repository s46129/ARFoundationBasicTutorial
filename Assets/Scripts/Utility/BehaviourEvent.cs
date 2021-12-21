namespace ARTutorial.Utility
{
    using UnityEngine;
    using UnityEngine.Events;

    public class BehaviourEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnEnableEvent;
        [SerializeField] private UnityEvent OnDisableEvent;

        void OnEnable()
        {
            OnEnableEvent?.Invoke();
        }

        void OnDisable()
        {
            OnDisableEvent?.Invoke();
        }
    }
}