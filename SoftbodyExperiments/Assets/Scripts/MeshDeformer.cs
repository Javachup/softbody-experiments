using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{
    [SerializeField] float springForce = 3f;
    [SerializeField] float dampening = 3f;

    Mesh mesh;
    Vector3[] originalVerts;
    Vector3[] deformedVerts;
    Vector3[] vertVelocity;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        originalVerts = mesh.vertices;
        deformedVerts = mesh.vertices;
        vertVelocity = new Vector3[deformedVerts.Length];
    }

    private void Update()
    {
        // Update Position 
        for (int i = 0; i < deformedVerts.Length; i++)
        {
            AddSpringForceToVertex(i);
            AddDampeningForceToVertex(i);
            deformedVerts[i] += vertVelocity[i] * Time.deltaTime;
        }

        UpdateMeshVerts();
    }

    public void AddDeformingForce(Vector3 position, float force)
    {
        position = transform.InverseTransformPoint(position);

        for (int i = 0; i < deformedVerts.Length; i++)
        {
            AddForceToVertex(i, position, force);
        }
    }

    private void AddForceToVertex(int index, Vector3 position, float force)
    {
        Vector3 posToVert = deformedVerts[index] - position;
        force /= (posToVert.sqrMagnitude + 1f); // Inverse Square Law 
        float deltaSpeed = force * Time.deltaTime; // F = ma where m = 1 
        vertVelocity[index] += deltaSpeed * posToVert.normalized;
    }

    private void AddSpringForceToVertex(int index)
    {
        Vector3 displacement = deformedVerts[index] - originalVerts[index];
        vertVelocity[index] -= springForce * Time.deltaTime * displacement;
    }

    private void AddDampeningForceToVertex(int index)
    {
        vertVelocity[index] *= 1 - dampening * Time.deltaTime;
    }

    private void UpdateMeshVerts()
    {
        mesh.vertices = deformedVerts;
        mesh.RecalculateNormals();
    }
}
