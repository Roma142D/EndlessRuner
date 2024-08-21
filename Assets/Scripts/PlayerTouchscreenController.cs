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
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform[] _moveToPoints;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private float _groundCheckDistance;
        [SerializeField] private float _jumpPower;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private float _minSwipeLength;
        [SerializeField] private CapsuleCollider _playerCollider;
        private MyPlayerInput _playerInput;
        private int _playerIndexPosition;
        private Vector2 _touchPosition;
        private Vector2 _swipeVector;
        
        private void Awake()
        {
            _playerIndexPosition = 1;
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
            
            if (_swipeVector.magnitude > _minSwipeLength)
            {
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
                    if (touchEndPosition.y > _touchPosition.y)
                    {
                        Jump();
                    }
                    else if (touchEndPosition.y < _touchPosition.y)
                    {
                        Roll();
                    }
                }
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

            StartCoroutine(MovePlayer(currentPosition, rightPosition, _turnSpeed, false));
            
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

            StartCoroutine(MovePlayer(currentPosition, leftPosition, _turnSpeed, false));
            
            _playerIndexPosition -= 1;
                                   
            _playerAnimator.SetTrigger("LeftTurn");
        } 

        private void Jump()
        {
            var currentPosition = _player.transform.position;
            var jumpPosition = new Vector3(_player.position.x, _player.position.y + _jumpPower, _player.position.z);
            if (Physics.Raycast(_player.transform.position, Vector3.down, _groundCheckDistance))
            {
                _playerAnimator.SetTrigger("Jump");
                _player.useGravity = false;
                StartCoroutine(MovePlayer(currentPosition, jumpPosition, _turnSpeed * 2, true));
            }
        }
        private void Roll()
        {
            var currentPosition = _player.transform.position;
            var downPosition = new Vector3(_player.position.x, _player.position.y + _jumpPower, _player.position.z);
            if (!Physics.Raycast(_player.transform.position, Vector3.down, _groundCheckDistance))
            {
                Physics.Raycast(_player.transform.position, Vector3.down, out RaycastHit ground);
                StartCoroutine(MovePlayer(currentPosition, ground.point, _turnSpeed * 0.5f, false));
            }
            else
            {
                _playerAnimator.SetTrigger("Roll");
                StartCoroutine(RollCoroutine());
            }
        }
        private IEnumerator MovePlayer(Vector3 currentPosition, Vector3 endPosition, float duration, bool isJump)
        {
            var currentTime = 0f;
            var deltaTime = 0f;
            var endTime = 1f;
            var cameraXPosition = _camera.position.x;

            while (deltaTime != duration)
            {
                _player.transform.position = Vector3.Lerp(currentPosition, endPosition, currentTime);
                cameraXPosition = Mathf.SmoothStep(_camera.position.x, endPosition.x, currentTime / 3);
                _camera.position = new Vector3(cameraXPosition, _camera.position.y, _camera.position.z);
                deltaTime = Mathf.Min(duration, deltaTime + Time.deltaTime);
                currentTime = Mathf.Min(endTime, (endTime * deltaTime) / duration);

                yield return new WaitForEndOfFrame();
            }
            if (isJump)
            {
                _player.useGravity = true;
            }
        }
        private IEnumerator RollCoroutine()
        {
            var normalCenter = _playerCollider.center;
            var normalHeight = _playerCollider.height;
            _playerCollider.center = new Vector3(_playerCollider.center.x, 0, 0);
            _playerCollider.height = 0f;
            yield return new WaitForSeconds(1.2f);
            _playerCollider.center = normalCenter;
            _playerCollider.height = normalHeight;
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
