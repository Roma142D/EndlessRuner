using UnityEngine;
using UnityEngine.UI;
using RomanDoliba.PowerUp;
using RomanDoliba.ActionSystem;
using System;
using System.Collections;

namespace RomanDoliba.UI
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] private Image _timerRenderer;
        [SerializeField] private PowerUps powerUps;
        

        private void Awake()
        {
            _timerRenderer.gameObject.SetActive(false);
            _timerRenderer.fillAmount = 1f;
            GlobalEventSender.OnEvent += StartTimer;
        }
        

        private void StartTimer(string eventName)
        {
            if (eventName == "Shield")
            {
                _timerRenderer.gameObject.SetActive(true);
                StartCoroutine(TimerCoroutine(powerUps.ShieldDuration));
            }
        }

        private IEnumerator TimerCoroutine(float duration)
        {
            var currentTime = 0f;
            var deltaTime = 0f;
            var endTime = 1f;

            while (deltaTime != duration)
            {
                _timerRenderer.fillAmount = Mathf.SmoothStep(1f, 0f, currentTime);
                deltaTime = Mathf.Min(duration, deltaTime + Time.deltaTime);
                currentTime = Mathf.Min(endTime, (endTime * deltaTime) / duration);

                yield return new WaitForEndOfFrame();
            }
            _timerRenderer.gameObject.SetActive(false);
            _timerRenderer.fillAmount = 1f;
        }

        private void OnDestroy()
        {
            GlobalEventSender.OnEvent -= StartTimer;
        }
    }
}
