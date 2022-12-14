using Abstraction;
using UnityEngine;

namespace Concreate
{
    public class StackableMoney : AStackable
    {
        [SerializeField]
        private new Rigidbody rigidbody;

        [SerializeField]
        private new BoxCollider collider;

        public override bool IsSelected { get; set; }
        public override bool IsCollected { get; set; }

        private void OnEnable()
        {
            SendStackable(this);

            EditPhysics();
        }

        public override GameObject SendToStack()
        {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            collider.enabled = false;
            return transform.gameObject;
        }

        internal void EditPhysics()
        {
            collider.enabled = true;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }

        private void OnDisable()
        {
            SendStackableOndisable(this);
        }
    }
}