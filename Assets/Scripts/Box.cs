using UnityEngine;

public class Box : MonoBehaviour
{
    enum MovingState
    {
        None,
        Moving,
        Falling
    }

    private MovingState state = MovingState.None;
    private Vector3 startPosition;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float animation;
    private const float speed = 3f;
    [SerializeField]
    AudioSource moveSound;

    public bool canMove(Vector3 position) {
        if (Physics.CheckSphere(position, 0.5f, 1 << LayerMask.NameToLayer("Level"))
            || Physics.CheckSphere(position, 0.5f, 1 << LayerMask.NameToLayer("Box"))) {
            return false;
        } else {
            return true;
        }
    }

    void Awake() {
        targetPosition = new Vector3(
            Utils.RoundToNearestMultiple(transform.position.x, 2), 
            Mathf.RoundToInt(transform.position.y),
            Utils.RoundToNearestMultiple(transform.position.z, 2)
        );
        startPosition = targetPosition;
    }

    public void Move(Vector3 newPosition) {
        targetPosition = newPosition;
        state = MovingState.Moving;
        moveSound.Play();
    }

    public void Reset() {
        transform.position = startPosition;
        targetPosition = startPosition;
        state = MovingState.None;
    }

    void Update() {
          switch(state) {
            case MovingState.None:
                initialPosition = transform.position;
                animation = 0f;
                break;
            case MovingState.Moving:
                animation += Time.deltaTime * speed;
                if (animation > 1.0f)
                {
                    transform.position = targetPosition;
                    animation = 0.0f;
                    if (Physics.CheckSphere(targetPosition, 0.5f, 1 << LayerMask.NameToLayer("Death"))) {
                        state = MovingState.Falling;
                        initialPosition = targetPosition;
                        targetPosition = targetPosition + Vector3.down * 5;
                    } else {
                        state = MovingState.None;
                    }
                } else {
                    transform.position = Vector3.Lerp(initialPosition, targetPosition, animation);
                }
                break;
            case MovingState.Falling:
                animation += Time.deltaTime * speed;

                if (animation > 2.0f)
                {
                    state = MovingState.None;
                    animation = 0.0f;
                } else {
                    transform.position = Vector3.Lerp(initialPosition, targetPosition, animation);
                }
                break;
          }
    }
}