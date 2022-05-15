using UnityEngine;

public class DeformerTester : MonoBehaviour
{
    [SerializeField] float force = 5f;
    [SerializeField] float offset = 0.1f;

    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            DoTheDeform();
    }

    private void DoTheDeform()
    {
        Ray inputRay = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(inputRay, out RaycastHit hit))
        {
            Debug.DrawLine(cam.transform.position, hit.point);

            MeshDeformer meshDeformer = hit.collider.GetComponent<MeshDeformer>();

            if (meshDeformer)
            {
                Vector3 position = hit.point + hit.normal * offset;
                meshDeformer.AddDeformingForce(position, force);
            }
        }
    }
}
