using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    public bool IsOpen = false;
    [SerializeField]
    private float Speed = 1f;
    [SerializeField]
    private float RotationAmount = 90f;

    private Vector3 StartRotation;
    private Vector3 StartPosition;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        // Since "Forward" actually is pointing into the door frame, choose a direction to think about as "forward"
        Forward = transform.right;
        StartPosition = transform.position;
    }

    public override bool OnInteract(GameObject player, InventoryItem item)
    {
        if (IsOpen)
        {
            Close();
        }
        else
        {
            Open(player.transform.position);
        }
        return true;
    }

    public void Open(Vector3 UserPosition)
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            float dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
            AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(
            new Vector3(
                0,
                StartRotation.y + (
                    ForwardAmount >= 0
                    ? RotationAmount
                    : -RotationAmount
                    ),
                0
            )
        );

        IsOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    public void Close()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(DoRotationClose());

        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
}