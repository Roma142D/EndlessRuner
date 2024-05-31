using UnityEngine;
using System;

namespace RomanDoliba.ActionSystem
{
    public class OnPhysikExecutor : ExecutorBase
    {
        [SerializeField] private bool _onCollision;
        [SerializeField] private bool _onTrigger;
        [SerializeField] private State _state;

        private void OnTriggerEnter(Collider collider)
        {
            if (_onTrigger && _state._enter)
            {
                Execute(collider);
            }
        }
        private void OnTriggerExit(Collider collider)
        {
            if (_onTrigger && _state._exit)
            {
                Execute(collider);
            }
        }
        private void OnTriggerStay(Collider collider)
        {
            if (_onTrigger && _state._stay)
            {
                Execute(collider);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (_onCollision && _state._enter)
            {
                Execute(collision);
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (_onCollision && _state._exit)
            {
                Execute(collision);
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            if (_onCollision && _state._stay)
            {
                Execute(collision);
            }
        }
        
        [Serializable]
        public struct State
        {
            public bool _enter;
            public bool _stay;
            public bool _exit;
        }
    }
}
