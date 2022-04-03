using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _jumpSpeed = 0.5f;
    [SerializeField] private float _gravity = 2f;

    CharacterController _characterController;
    private Vector3 _moveDirection;
    public Animator anim;

    void Awake() => _characterController = GetComponent<CharacterController>();

    void Update()
    {
        anim.SetFloat("Walk", Input.GetAxis("Vertical"));
        anim.SetFloat("Turn", Input.GetAxis("Horizontal"));

    }

        void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
        Vector3 transformDirection = transform.TransformDirection(inputDirection);

        Vector3 flatMovement = _moveSpeed * Time.deltaTime * transformDirection;

        _moveDirection = new Vector3(flatMovement.x, _moveDirection.y, flatMovement.z);

        if (PlayerJumped)
        {
            _moveDirection.y = _jumpSpeed;
        }
        else if (_characterController.isGrounded)
        {
            _moveDirection.y = 0f;
        }
        else
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }

        _characterController.Move(_moveDirection);
    }

    private bool PlayerJumped => _characterController.isGrounded && Input.GetKey(KeyCode.Space);
}