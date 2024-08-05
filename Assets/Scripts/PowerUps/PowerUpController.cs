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
        [SerializeField] private SkinnedMeshRenderer _playerRenderer;
        [SerializeField] private Material _shieldMaterial;
        [SerializeField] private Material _currentMaterial;
        private Coroutine _powerUpRoutine;

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
                    _powerUpRoutine = StartCoroutine(PowerUpCoroutine(_powerUps.ShieldDuration, _shieldMaterial));
                    
                    break;
                
            }
        }
        private void PowerUpReset()
        {
            _powerUps.ShieldToggle(_playerCollider, false);
            _playerRenderer.material = _currentMaterial;
            _powerUpRoutine = null;
        }

        private IEnumerator PowerUpCoroutine(float duration, Material material)
        {
            while (duration > 0)
            {
                _playerRenderer.material = material;
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
