using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class PathNode : ProximityTarget
{
    public PathNode Next;

    public bool WaitForProximitySensor;

    private bool IsTriggered;

    public bool Proceed
    {
        get { return !WaitForProximitySensor || IsTriggered; }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.1f);

        if (Next != null)
        {
            Gizmos.DrawLine(transform.position, Next.transform.position);
        }
    }

    public override void ObjectEnterProximity(GameObject obj)
    {
        IsTriggered = true;
    }

    public override void ObjectExitProximity(GameObject obj)
    {
    }
}
