using UnityEngine;

namespace PhysicsController
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseRigidbodyController : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; private set; }
        [SerializeField] private Transform _centerOfMass;

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Rigidbody.centerOfMass = _centerOfMass.localPosition;
        }

      

        protected virtual void FixedUpdate()
        {
            HandlePhysics();
        }

        protected abstract void HandlePhysics();
    }
}