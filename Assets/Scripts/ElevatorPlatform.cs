using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    public float moveDistance = 5f;
    public float moveSpeed = 2f;
    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingUp = true;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * moveDistance;
    }

    void Update()
    {
        Vector3 direction = movingUp ? Vector3.up : Vector3.down;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (movingUp && transform.position.y >= targetPos.y)
            movingUp = false;
        else if (!movingUp && transform.position.y <= startPos.y)
            movingUp = true;
    }
}
