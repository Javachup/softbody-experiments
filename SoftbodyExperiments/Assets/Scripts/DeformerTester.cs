using UnityEngine;

public class DeformerTester : MonoBehaviour
{
    [SerializeField] MeshDeformer meshDeformer;

    private void Update()
    {
        DoTheDeform();
    }

    private void DoTheDeform()
    {
        meshDeformer.Deform(transform.position, transform.localScale.x * .5f);
    }
}
