using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{
    Mesh mesh;
    Vector3[] originalVerts;
    Vector3[] deformedVerts;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        print(mesh.isReadable);

        originalVerts = mesh.vertices;
        deformedVerts = mesh.vertices;
    }

    public void Deform(Vector3 position, float radius)
    {
        for (int i = 0; i < deformedVerts.Length; i++)
        {
            Vector3 away = deformedVerts[i] - position;

            if (away.sqrMagnitude < radius * radius)
            {
                away.Normalize();

                deformedVerts[i] += away * .1f;
            }
        }

        SetDeformedVerts();
    }

    private void SetDeformedVerts()
    {
        mesh.vertices = deformedVerts;
        mesh.RecalculateNormals();
    }
}
