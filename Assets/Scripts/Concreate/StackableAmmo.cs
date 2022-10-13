using Assets.Scripts.Abstraction.Abstracts;
using Controllers;
using Controllers.Player;
using System.Collections;
using UnityEngine;

namespace Concreate
{
    public class StackableAmmo :AStackable
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private BoxCollider collider;


        public override void SendPosition(Transform transform)
        {
            base.SendPosition(transform);
        }
        private void OnEnable()
        {
            SendPosition(this.transform);
        }

        public override void SetInit(Transform initTransform, Vector3 position)
        {
            base.SetInit(initTransform, position);
        }

        public override void SetVibration(bool isVibrate)
        {
            base.SetVibration(isVibrate);
        }

        public override void SetSound()
        {
            base.SetSound();
        }

        public override void EmitParticle()
        {
            base.EmitParticle();
        }

        public override void PlayAnimation()
        {
            base.PlayAnimation();
        }

        public override GameObject SendToStack()
        {
            //transform.localRotation = new Quaternion(0, 0, 0, 1);
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            //collider.enabled = false;

            transform.gameObject.layer = LayerMask.NameToLayer("Ammo/AmmoTaken");

            return transform.gameObject;

        }

        private void EditPhysics()
        {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }

    }
}