using System;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public PathNode NextNode;
    public PathNode StartNode;
    public float MoveSpeed = 0.3f;
    public float RotateDegreesSpeed = 12f;

    public Behaviour[] EnableComponentsWhenFinishedBeforeDelay;
    public Behaviour[] EnableComponentsWhenFinishedAfterDelay;
    public GameObject[] EnableGameObjectsWhenFinishedAfterDelay;
    public int EnableDelaySeconds;

    private bool DidStartCoroutine;

    public bool DisableAfterStart;

    public void Start()
    {
        transform.position = StartNode.transform.position;

        if (StartNode.Proceed)
        {
            NextNode = StartNode.Next;
        }
        else
        {
            NextNode = StartNode;
        }

        if (NextNode != null && RotateDegreesSpeed > 0)
        {
            transform.rotation = Quaternion.LookRotation(NextNode.transform.position - transform.position, Vector3.up);
        }

        if (DisableAfterStart)
        {
            enabled = false;
        }
    }

    public void Update()
    {
        if (NextNode == null)
        {
            if (!DidStartCoroutine)
            {
                StartCoroutine(EnableComponents().GetEnumerator());
                DidStartCoroutine = true;
            }

            return;
        }

        if (RotateDegreesSpeed > 0)
        {
            var towards = NextNode.transform.position - transform.position;
            if (Math.Abs(towards.magnitude) > 0f)
            {
                var targetRotation = Quaternion.LookRotation(towards, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotateDegreesSpeed);
            }
        }

        var direction = NextNode.transform.position - transform.position;
        var moveAmount = direction.normalized*MoveSpeed;

        if (direction.magnitude <= MoveSpeed)
        {
            if (NextNode.Proceed)
            {
                NextNode = NextNode.Next;
            }
        }
        else
        {
            transform.position += moveAmount;
        }
    }

    private IEnumerable<YieldInstruction> EnableComponents()
    {
        foreach (var component in EnableComponentsWhenFinishedBeforeDelay)
        {
            if (!component.enabled)
            {
                component.enabled = true;
            }
        }

        yield return new WaitForSeconds(EnableDelaySeconds);

        foreach (var component in EnableComponentsWhenFinishedAfterDelay)
        {
            if (!component.enabled)
            {
                component.enabled = true;
            }
        }

        foreach (var gameObject in EnableGameObjectsWhenFinishedAfterDelay)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
