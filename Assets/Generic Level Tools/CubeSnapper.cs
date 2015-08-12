using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Transform))]
[ExecuteInEditMode]
public class CubeSnapper : MonoBehaviour
{

    public bool IgnoreScale = false;

	void Start ()
    {
        UpdatePositioning();
    }
	
	void Update ()
    {
        if (!Application.isEditor || Application.isPlaying)
        {
            return;
        }

	    UpdatePositioning();
	}

    private void UpdatePositioning()
    {
        transform.localPosition = new Vector3(
            Mathf.Round(transform.localPosition.x * 2) / 2f,
            Mathf.Round(transform.localPosition.y * 2) / 2f,
            Mathf.Round(transform.localPosition.z * 2) / 2f);
        if (!IgnoreScale)
        {
            transform.localScale = new Vector3(
                Mathf.Round(transform.localScale.x),
                Mathf.Round(transform.localScale.y),
                Mathf.Round(transform.localScale.z));
        }
    }
}
