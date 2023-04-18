using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    public bool IsOpen = false;
    [SerializeField]
    float Speed = 1f;
    [SerializeField]
    float RotationAmount = 90f;

    Vector3 StartRotation;
    Vector3 StartPosition;
    Vector3 Forward;

    Coroutine AnimationCoroutine;

    void Awake()
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

    IEnumerator DoRotationOpen(float ForwardAmount)
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

        for (float time = 0; time < 1; time += Time.deltaTime * Speed)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
        }
    }

    void Close()
    {
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }

        AnimationCoroutine = StartCoroutine(DoRotationClose());
    }

    IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;

        for (float time = 0; time < 1; time += Time.deltaTime * Speed)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
        }
    }
}
