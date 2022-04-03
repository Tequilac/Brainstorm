using UnityEngine;

public class MoveWithForce : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    [SerializeField] private float _movementForce = 10f;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            _rigidbody.AddForce(_movementForce * Vector3.forward);
        
        if (Input.GetKey(KeyCode.S))
            _rigidbody.AddForce(_movementForce * Vector3.back);

        if (Input.GetKey(KeyCode.D))
            _rigidbody.AddForce(_movementForce * Vector3.right);
        
        if (Input.GetKey(KeyCode.A))
            _rigidbody.AddForce(_movementForce * Vector3.left);
    }
}