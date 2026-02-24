using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputServcice : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;

    private PlayerInput _playerInput;

    private bool _isPressed = false;

    public event Action<Vector3> Moving;
    public event Action MoveEnded;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.performed += OnPressed;
        _playerInput.Player.Move.canceled += OnReleased;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Player.Move.performed -= OnPressed;
        _playerInput.Player.Move.canceled -= OnReleased;
    }

    private void OnPressed(InputAction.CallbackContext context)
    {
        _isPressed = true;
    }

    private void OnReleased(InputAction.CallbackContext context)
    {
        _isPressed = false;
        MoveEnded?.Invoke();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector3 mousePosition = context.action.ReadValue<Vector2>();
        
        if (TryGetMousePositionOnGround(mousePosition, out Vector3 point))
        {
            Moving?.Invoke(point);
        }
        /*else
        {
            MoveEnded?.Invoke();
        }*/
    }

    private void OnMoveEnd(InputAction.CallbackContext context)
    {
        MoveEnded?.Invoke();
    }

    private void Update()
    {
        if (_isPressed)
        {
            Debug.Log("IsPressed");
            if (TryGetMousePositionOnGround(_playerInput.Player.Move.ReadValue<Vector2>(), out Vector3 point))
            {
                Debug.Log("Moving");
                Moving?.Invoke(point);
            }
        }
    }

    public bool TryGetMousePositionOnGround(Vector2 mousePosition, out Vector3 point)
    {
        mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        float maxDistance = 100;

        if (Physics.Raycast(ray, out hit, maxDistance, _groundMask))
        {
            point = hit.point;

            return true;
        }

        point = Vector3.zero;

        return false;
    }
}
