using System.Collections;
using System.Collections.Generic;
using RomanDoliba.ActionSystem;
using UnityEngine;

namespace RomanDoliba.PowerUp
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField] private PowerUps _powerUps;
        [SerializeField] private Collider _playerCollider;

        private void Awake()
        {
            GlobalEventSender.OnEvent += PowerUpActive;
           
        }
        private void PowerUpActive(string eventName)
        {
            switch (eventName)
            {
                case "Shield":
                    _powerUps.ShieldToggle(_playerCollider, true);
                    StartCoroutine(PowerUpCoroutine(_powerUps.ShieldDuration));
                    break;
                
            }
        }
        private void PowerUpReset()
        {
            _powerUps.ShieldToggle(_playerCollider, false);
        }

        private IEnumerator PowerUpCoroutine(float duration)
        {
            while (duration > 0)
            {
                duration -= Time.deltaTime;
                yield return null;
            }
            PowerUpReset();
        }

        private void OnDisable()
        {
            GlobalEventSender.OnEvent -= PowerUpActive;
        }
    }
}
