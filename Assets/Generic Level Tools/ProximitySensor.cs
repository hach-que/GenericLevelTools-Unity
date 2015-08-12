using UnityEngine;

public class ProximitySensor : MonoBehaviour
{
    public ProximityTarget[] Targets = new ProximityTarget[0];

    public void OnTriggerEnter(Collider collider)
    {
        foreach (var target in Targets)
        {
            target.ObjectEnterProximity(collider.gameObject);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        foreach (var target in Targets)
        {
            target.ObjectExitProximity(collider.gameObject);
        }
    }
}
