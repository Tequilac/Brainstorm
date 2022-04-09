using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour {

    public float jumpVelocity = 7f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody _rigidbody;

    void Awake ()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _rigidbody.velocity = Vector3.up * jumpVelocity;
        } else if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (_rigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
