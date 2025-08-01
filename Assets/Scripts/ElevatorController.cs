using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform platform;
    public Transform doorLeft, doorRight;
    public float doorOpenDistance = 1f;
    public float moveSpeed = 2f;
    public float elevatorHeight = 5f;

    private Vector3 startPos, endPos;
    private Vector3 doorLeftClosed, doorRightClosed;
    private Vector3 doorLeftOpen, doorRightOpen;
    private bool playerInside = false;
    private bool doorsOpened = false;
    private bool movingUp = false;

    void Start()
    {
        startPos = platform.position;
        endPos = startPos + Vector3.up * elevatorHeight;

        doorLeftClosed = doorLeft.localPosition;
        doorRightClosed = doorRight.localPosition;

        doorLeftOpen = doorLeftClosed + Vector3.left * doorOpenDistance;
        doorRightOpen = doorRightClosed + Vector3.right * doorOpenDistance;
    }

    void Update()
    {
        if (playerInside && !doorsOpened)
        {
            OpenDoors();
        }

        if (doorsOpened && !movingUp)
        {
            Invoke("StartElevator", 2f); // wait 2s then move
        }

        if (movingUp)
        {
            platform.position = Vector3.MoveTowards(platform.position, endPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(platform.position, endPos) < 0.01f)
            {
                movingUp = false;
                CloseDoors();
            }
        }
    }

    void OpenDoors()
    {
        doorLeft.localPosition = Vector3.MoveTowards(doorLeft.localPosition, doorLeftOpen, moveSpeed * Time.deltaTime);
        doorRight.localPosition = Vector3.MoveTowards(doorRight.localPosition, doorRightOpen, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(doorLeft.localPosition, doorLeftOpen) < 0.01f &&
            Vector3.Distance(doorRight.localPosition, doorRightOpen) < 0.01f)
        {
            doorsOpened = true;
        }
    }

    void CloseDoors()
    {
        doorLeft.localPosition = doorLeftClosed;
        doorRight.localPosition = doorRightClosed;
    }

    void StartElevator()
    {
        movingUp = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered elevator trigger");
            playerInside = true;
        }
    }
}
