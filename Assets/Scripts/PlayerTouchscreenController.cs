using System;
using System.Collections;
using System.Collections.Generic;
using RomanDoliba.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RomanDoliba.Core
{
    public class PlayerTouchscreenController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _player;
        [SerializeField] private Transform[] _moveToPoints;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private float _groundCheckDistance;
        [SerializeField] private float _jumpPower;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private float _minSwipeLength;
        private MyPlayerInput _playerInput;
        private int _playerIndexPosition;
        private Vector2 _touchPosition;
        private Vector2 _swipeVector;
        
        private void Awake()
        {
            _playerInput = new MyPlayerInput();
            _playerInput.PlayerTouchscreen.Touch.started += OnTouchStarted;
            _playerInput.PlayerTouchscreen.Touch.canceled += OnTouchCanceled;
        }

        private void OnTouchStarted(InputAction.CallbackContext context)
        {
            _touchPosition = _playerInput.PlayerTouchscreen.Swipe.ReadValue<Vector2>();
        }
        private void OnTouchCanceled(InputAction.CallbackContext context)
        {
            var touchEndPosition = _playerInput.PlayerTouchscreen.Swipe.ReadValue<Vector2>();
            _swipeVector = touchEndPosition - _touchPosition;
            
            if (Math.Abs(_swipeVector.x) > Math.Abs(_swipeVector.y))
            {
                if (touchEndPosition.x > _touchPosition.x)
                {
                    MoveRight();
                }
                else if (touchEndPosition.x < _touchPosition.x)
                {
                    MoveLeft();
                }
            }
            else
            {
                Jump();
            }
        }
        private void MoveRight()
        {
            if (_playerIndexPosition > 2)
            {
                _playerIndexPosition = 1;
            }
            var currentPosition = _player.transform.position;
            var rightPosition = new Vector3(_moveToPoints[_playerIndexPosition + 1].position.x, _player.position.y, _player.position.z);

            StartCoroutine(MovePlayer(currentPosition, rightPosition, _turnSpeed));
            
            _playerIndexPosition += 1;
                                   
            _playerAnimator.SetTrigger("RightTurn");
        } 

        private void MoveLeft()
        {
            if (_playerIndexPosition < 0)
            {
                _playerIndexPosition = 1;
            }
            var currentPosition = _player.transform.position;
            var leftPosition = new Vector3(_moveToPoints[_playerIndexPosition - 1].position.x, _player.position.y, _player.position.z);

            StartCoroutine(MovePlayer(currentPosition, leftPosition, _turnSpeed));
            
            _playerIndexPosition -= 1;
                                   
            _playerAnimator.SetTrigger("LeftTurn");
        } 

        private void Jump()
        {
             if (Physics.Raycast(_player.transform.position, Vector3.down, _groundCheckDistance))
            {
                _player.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
                _playerAnimator.SetTrigger("Jump");
            }
        }
        private IEnumerator MovePlayer(Vector3 currentPosition, Vector3 endPosition, float duration)
        {
            var currentTime = 0f;
            var deltaTime = 0f;
            var endTime = 1f;

            while (deltaTime != duration)
            {
                _player.transform.position = Vector3.Lerp(currentPosition, endPosition, currentTime);
                deltaTime = Mathf.Min(duration, deltaTime + Time.deltaTime);
                currentTime = Mathf.Min(endTime, (endTime * deltaTime) / duration);

                yield return new WaitForEndOfFrame();
            }
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }
        private void OnDisable()
        {
            _playerInput.Disable();
        }
    }
}
