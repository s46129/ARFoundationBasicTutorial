namespace ARTutorial.ShootGame
{
    using UnityEngine;
    using UnityEngine.Events;

    public class OnShootHitedEvent : MonoBehaviour
    {
        public UnityEvent OnHited;

        public void Hited()
        {
            OnHited?.Invoke();
        }
    }
}