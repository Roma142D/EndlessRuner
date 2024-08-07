using System.Collections;
using RomanDoliba.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RomanDoliba.Core
{
    public class PlayerControler : MonoBehaviour
    {
        [SerializeField] private Rigidbody _player;
        [SerializeField] private Transform[] _moveToPoints;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private float _groundCheckDistance;
        [SerializeField] private float _jumpPower;
        [SerializeField] private Animator _playerAnimator;
        private MyPlayerInput _playerInput;
        
        private int _playerIndexPosition;

        private void Awake()
        {
            _playerIndexPosition = 1;

            _playerInput = new MyPlayerInput();
            _playerInput.Player.MoveRight.performed += MoveRight;
            _playerInput.Player.MoveLeft.performed += MoveLeft;
            _playerInput.Player.Jump.performed += Jump;
        }

        
        private void MoveRight(InputAction.CallbackContext callback)
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

        private void MoveLeft(InputAction.CallbackContext callback)
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

        private void Jump(InputAction.CallbackContext callback)
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
