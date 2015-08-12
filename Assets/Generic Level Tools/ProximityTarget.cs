using UnityEngine;

public abstract class ProximityTarget : MonoBehaviour
{
    public abstract void ObjectEnterProximity(GameObject obj);

    public abstract void ObjectExitProximity(GameObject obj);
}