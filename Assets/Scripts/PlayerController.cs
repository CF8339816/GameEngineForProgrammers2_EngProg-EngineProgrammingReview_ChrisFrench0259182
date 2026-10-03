using UnityEngine;
using UnityEngine.InputSystem;
#region coder & project
/// <summary>
/// NSCC GAME2025 / 4086 / Game Programming III(B)/ Doucette,Matthew
/// Unity: Player Input Controller Integration
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations: Integrated modern Input System while maintaining architecture flow.
/// </summary>
#endregion


public class PlayerController : MonoBehaviour
{    
    [SerializeField] private float moveSpeed = 5f;
    private PlayerControls inputActions;
    private Vector2 moveInput;
    private CharacterController characterController;
    private void Awake()
    {
        inputActions = new PlayerControls();
        characterController = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveStopped;
    }
    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Move.canceled -= OnMoveStopped;
        inputActions.Player.Disable();
    }
    private void Update()
    {
        MovePlayer ();
    }
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    private void OnMoveStopped(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }
    private void MovePlayer()
    {
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
        characterController.Move(direction * moveSpeed * Time.deltaTime);
    }
}