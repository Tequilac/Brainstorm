using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float _jumpForce = 300f;
    private bool _shouldJump;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    // Reading GetKeyDown in FixedUpdate is unreliable (expand for details) 
    // It's possible for 2 updates have passed before a FixedUpdate call, which could lead to missing the KeyDown
    // Example.
    // Update() #1 - User Pressed Space
    // Update() #2 - User still Holding Space
    // FixedUpdate() - GetKeyDown is false - which is why we don't read GetKeyDown in FixedUpdate()
    // - Note: GetKey() returns true for every frame and would still be fine to use,
    //         but in this case we don't want to add force every frame they hold space 
    private void Update()
    {
        if (_shouldJump == false)
        {
            _shouldJump = Input.GetKeyDown(KeyCode.Space);
        }
    }

    private void FixedUpdate()
    {
        if (_shouldJump)
        {
            _rigidbody.AddForce(_jumpForce * Vector3.up, ForceMode.Impulse);
            _shouldJump = false;
        }
    }
}