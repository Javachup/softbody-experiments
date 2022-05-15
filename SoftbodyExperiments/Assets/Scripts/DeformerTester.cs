using UnityEngine;

public class DeformerTester : MonoBehaviour
{
    [SerializeField] MeshDeformer meshDeformer;
    [SerializeField] float force = 5f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            DoTheDeform();
    }

    private void DoTheDeform()
    {
        meshDeformer.AddDeformingForce(transform.position, force);
    }
}
