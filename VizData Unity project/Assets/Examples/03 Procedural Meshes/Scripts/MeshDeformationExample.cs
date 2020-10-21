
using UnityEngine;

public class MeshDeformationExample : MonoBehaviour
{

    public Material material;
    public Mesh originalMesh;

    [Header("Squiggle Settings")]
    [Range(0,20)]public float waveCount = 2; //enables control over level of deformation in wavyness
    [Range(0.1f, 20)] public float waveAmount = 0.1f; //enables control over how much we stretch the mesh
    [Range(0, 20)] public float waveSpeed = 1;

    Mesh _mesh;
    Vector3[] _originalVerticies;
    Vector3[] _derformedVerticies;

    float waveAngleOffset;


    void Awake()
    {

        _originalVerticies = originalMesh.vertices;
        int[] triangleindicies = originalMesh.triangles;

        _mesh = new Mesh();
        _mesh.vertices = _originalVerticies;
        _mesh.triangles = triangleindicies;
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        _derformedVerticies = new Vector3[_originalVerticies.Length];
        
    }

    void Update()
    {

        waveAngleOffset += Time.deltaTime * waveSpeed;

        for (int v = 0; v < _originalVerticies.Length; v++){
            Vector3 vertexPosition = _originalVerticies[v];

            //Squish the bunny
            float angle = vertexPosition.x * Mathf.PI * 2 * waveCount + waveAngleOffset;
            vertexPosition.y += Mathf.Sin(angle) * waveAmount;

            _derformedVerticies[v] = vertexPosition;

        }

        _mesh.vertices = _derformedVerticies;
        _mesh.RecalculateNormals();

        Graphics.DrawMesh(_mesh, transform.localToWorldMatrix, material, gameObject.layer);
    }


}
