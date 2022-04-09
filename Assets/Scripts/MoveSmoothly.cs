using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSmoothly : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;

    public Animator anim;
    public float speed;
    public float rotationSpeed;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Walk", Input.GetAxis("Vertical"));
        anim.SetFloat("Turn", Input.GetAxis("Horizontal"));
        if (Input.GetKey(KeyCode.D))
            transform.position += Time.deltaTime * _moveSpeed * Vector3.right;

        if (Input.GetKey(KeyCode.A))
            transform.position += Time.deltaTime * _moveSpeed * Vector3.left;

        if (Input.GetKey(KeyCode.W))
            transform.position += Time.deltaTime * _moveSpeed * Vector3.forward;

        if (Input.GetKey(KeyCode.S))
            transform.position += Time.deltaTime * _moveSpeed * Vector3.back;
    }
}
