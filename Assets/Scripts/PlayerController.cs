using UnityEngine;
using UnityEngine.InputSystem; 


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private CharacterController _characterController;
    private Vector2 _inputVector = Vector2.zero;

    void Start()
    {       
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {     
        Vector3 moveDirection = new Vector3(_inputVector.x, 0f, _inputVector.y);            
        _characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    
    public void OnMove(InputValue value)
    {
        _inputVector = value.Get<Vector2>();
    }
}