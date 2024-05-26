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
        private MyPlayerInput _playerInput;
        
        private int _playerIndexPosition;

        private void Awake()
        {
            _playerIndexPosition = 1;

            _playerInput = new MyPlayerInput();
            _playerInput.Player.MoveRight.performed += MoveRight;
            _playerInput.Player.MoveLeft.performed += MoveLeft;
        }

        
        private void MoveRight(InputAction.CallbackContext callback)
        {
            if (_playerIndexPosition > 2)
            {
                _playerIndexPosition = 1;
            }
            var currentPosition = _player.transform.position;
            var rightPosition = _moveToPoints[_playerIndexPosition + 1];

            _player.transform.position = Vector3.Lerp(currentPosition, rightPosition.position, _turnSpeed);
            _playerIndexPosition += 1;
                                   
            Debug.Log("MoveRight");
        } 

        private void MoveLeft(InputAction.CallbackContext callback)
        {
            if (_playerIndexPosition < 0)
            {
                _playerIndexPosition = 1;
            }
            var currentPosition = _player.transform.position;
            var leftPosition = _moveToPoints[_playerIndexPosition - 1];

            _player.transform.position = Vector3.Lerp(currentPosition, leftPosition.position, _turnSpeed);
            _playerIndexPosition -= 1;
                                   
            Debug.Log("MoveLeft");
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
