using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class CubeMapper : MonoBehaviour
{
    public int UVDivide = 1;

    public bool FlipHorizontal = false;

    public bool FlipVertical = false;

    public Mesh OriginalMesh;

    public void Start()
    {
        UpdateUVs();
    }

    public void Update()
    {
        if (!Application.isEditor || Application.isPlaying)
        {
            return;
        }

        UpdateUVs();
    }

    private void UpdateUVs()
    {
        var mf = this.GetComponent<MeshFilter>();
        Mesh mesh = null;
        if (mf != null)
        {
#if UNITY_EDITOR
            if (mf.sharedMesh == null && OriginalMesh != null)
            {
                mf.sharedMesh = OriginalMesh;
            }

            if (mf.sharedMesh == null)
            {
                Debug.LogWarning("No OriginalMesh to restore when loading cube from prefab!");
                return;
            }

            if (mf.sharedMesh.name == "Cube")
            {
                OriginalMesh = mf.sharedMesh;
            }

            if (mf.sharedMesh.name != "Cube" + mf.gameObject.GetInstanceID())
            {
                mesh = Mesh.Instantiate(mf.sharedMesh);
                mesh.name = "Cube" + mf.gameObject.GetInstanceID();
                mf.mesh = mesh;
            }
            else
            {
                mesh = mf.sharedMesh;
            }
#else
            mesh = mf.mesh;
#endif
        }

        if (mesh == null || mesh.uv.Length != 24)
        {
#if !UNITY_EDITOR
            Debug.Log("Script needs to be attached to built-in cube");
#endif
            return;
        }

        var uvs = mesh.uv;
        var uvd = UVDivide;

        // Front
        uvs[0] = new Vector2(FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, FlipVertical ? (transform.localScale.y / uvd) : 0.0f);
        uvs[1] = new Vector2(!FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, FlipVertical ? (transform.localScale.y / uvd) : 0.0f);
        uvs[2] = new Vector2(FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, !FlipVertical ? (transform.localScale.y / uvd) : 0.0f);
        uvs[3] = new Vector2(!FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, !FlipVertical ? (transform.localScale.y / uvd) : 0.0f);

        // Top
        uvs[8] = new Vector2(FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, FlipVertical ? (transform.localScale.z / uvd) : 0.0f);
        uvs[9] = new Vector2(!FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, FlipVertical ? (transform.localScale.z / uvd) : 0.0f);
        uvs[4] = new Vector2(FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, !FlipVertical ? (transform.localScale.z / uvd) : 0.0f);
        uvs[5] = new Vector2(!FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, !FlipVertical ? (transform.localScale.z / uvd) : 0.0f);

        // Back
        uvs[10] = new Vector2(FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, !FlipVertical ? (transform.localScale.y / uvd) : 0.0f);
        uvs[11] = new Vector2(!FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, !FlipVertical ? (transform.localScale.y / uvd) : 0.0f);
        uvs[6] = new Vector2(FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, FlipVertical ? (transform.localScale.y / uvd) : 0.0f);
        uvs[7] = new Vector2(!FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, FlipVertical ? (transform.localScale.y / uvd) : 0.0f);

        // Bottom
        uvs[12] = new Vector2(FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, FlipVertical ? (transform.localScale.z / uvd) : 0.0f);
        uvs[14] = new Vector2(!FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, FlipVertical ? (transform.localScale.z / uvd) : 0.0f);
        uvs[15] = new Vector2(FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, !FlipVertical ? (transform.localScale.z / uvd) : 0.0f);
        uvs[13] = new Vector2(!FlipHorizontal ? (transform.localScale.x / uvd) : 0.0f, !FlipVertical ? (transform.localScale.z / uvd) : 0.0f);

        // Left
        uvs[16] = new Vector2(0.0f, 0.0f);
        uvs[18] = new Vector2(transform.localScale.z / uvd, 0.0f);
        uvs[19] = new Vector2(0.0f, transform.localScale.y / uvd);
        uvs[17] = new Vector2(transform.localScale.z / uvd, transform.localScale.y / uvd);

        // Right        
        uvs[20] = new Vector2(0.0f, 0.0f);
        uvs[22] = new Vector2(transform.localScale.z / uvd, 0.0f);
        uvs[23] = new Vector2(0.0f, transform.localScale.y / uvd);
        uvs[21] = new Vector2(transform.localScale.z / uvd, transform.localScale.y / uvd);

        mesh.uv = uvs;
    }
}
