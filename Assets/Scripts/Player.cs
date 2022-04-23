using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// https://gist.github.com/ditzel/68be36987d8e7c83d48f497294c66e08
public class MathParabola
{
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
        var mid = Vector3.Lerp(start, end, t);
        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}

public class Player : MonoBehaviour
{
    enum MovingState
    {
        None,
        Normal,
        Fake,
        Falling
    }

    enum BlockType
    {
        Air,
        Obstacle,
        Death
    }

    // [SerializeField, Tooltip("Player speed")]
    private const float speed = 3.5f;

    // When fake animation should reverse
    private const float fakeAnimationCollisionPoint = 0.25f;

    [SerializeField, Tooltip("Player index")]
    private int playerIndex;

    private MovingState state = MovingState.None;
    private float animation;

    private Vector3 gameStartingPosition;

    private Vector3 targetPosition;
    private Vector3 initialPosition;

    private GameController gameController;

    [SerializeField]
    AudioSource jump;

    [SerializeField]
    AudioSource badJump;

    void Awake()
    {
        targetPosition = new Vector3(
            RoundToNearestMultiple(transform.position.x, 2), 
            Mathf.RoundToInt(transform.position.y),
            RoundToNearestMultiple(transform.position.z, 2)
        );
        gameStartingPosition = targetPosition;
        transform.position = targetPosition;

        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.RegisterPlayer(playerIndex, this);
    }

    // Update is called once per frame
    void Update()
    {
        switch(state) {
            case MovingState.None:
                initialPosition = transform.position;
                animation = 0f;

                if (Input.GetKey(KeyCode.W))
                {
                    Move(Vector3Int.forward * 2);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Move(Vector3Int.back * 2);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    Move(Vector3Int.left * 2);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    Move(Vector3Int.right * 2);
                }
                break;
            case MovingState.Normal:
                animation += Time.deltaTime * speed;

                if (animation > 1.0f)
                {
                    jump.Play();
                    transform.position = targetPosition;
                    state = MovingState.None;
                    animation = 0.0f;
                } else {
                    transform.position = MathParabola.Parabola(initialPosition, targetPosition, 1f, animation);
                }
                break;
            case MovingState.Fake:
                animation += Time.deltaTime * speed;

                if (animation > 1.0f)
                {
                    badJump.Play();
                    transform.position = initialPosition;
                    targetPosition = initialPosition;
                    state = MovingState.None;
                    animation = 0.0f;
                } else {
                    // Clever Jacek's math
                    transform.position = MathParabola.Parabola(initialPosition, targetPosition, 1f, fakeAnimationCollisionPoint - Mathf.Abs(animation - 0.5f) * 2 * fakeAnimationCollisionPoint);
                }

                break;
            case MovingState.Falling:
                animation += Time.deltaTime * speed;

                if (animation > 2.0f)
                {
                    state = MovingState.None;
                    animation = 0.0f;
                    gameController.OnDeath();
                } else {
                    transform.position = MathParabola.Parabola(initialPosition, targetPosition, 1f, animation);
                }
                break;
        }
    }

    public void Reset() {
        transform.position = gameStartingPosition;
        targetPosition = gameStartingPosition;
    }

    private int RoundToNearestMultiple(float value, int multiple)
    {
        return (int)Mathf.Round(value / multiple) * multiple;
    }

    private BlockType GetBlockType(Vector3 position)
    {
        if (Physics.CheckSphere(position, 0.5f, 1 << LayerMask.NameToLayer("Level"))) {
            return BlockType.Obstacle;
        } else if (Physics.CheckSphere(position, 0.5f, 1 << LayerMask.NameToLayer("Death"))) {
            return BlockType.Death;
        } else {
            return BlockType.Air;
        }
    }

    private void Move(Vector3Int vector)
    {
        Vector3 newTargetPosition = targetPosition + vector;

        var targetBlockType = GetBlockType(newTargetPosition);

        switch (targetBlockType)
        {
            case BlockType.Air:
                state = MovingState.Normal;
                targetPosition = newTargetPosition;
                break;
            case BlockType.Obstacle:
                state = MovingState.Fake;
                targetPosition = newTargetPosition;
                break;
            case BlockType.Death:
                state = MovingState.Falling;
                targetPosition = newTargetPosition;
                break;
        }
    }
}
