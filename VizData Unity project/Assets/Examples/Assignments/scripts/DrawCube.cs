using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class DrawCube : MonoBehaviour
{

    public int xSize, ySize, zSize;
    private Vector3[] vertices;
    private Mesh mesh;


    private void Awake()
    {
        Generate();
        OnDrawGizmos();
    }




    private void Generate()
    {
        //WaitForSeconds wait = new WaitForSeconds(0.5f);

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Cube";

        vertices = new Vector3[(xSize + 1)* (ySize + 1) * (zSize + 1)];

        for (int i = 0, y= 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int z = 0; z < zSize; z++, i++)
                {
                    vertices[i] = new Vector3(x, y, z);

                    mesh.vertices = vertices;
                    //yield return wait;

                    int[] triangles = new int[36];

                    //Side 1
                    triangles[0] = 0;
                    triangles[1] = 6;
                    triangles[2] = 2;

                    triangles[3] = 0;
                    triangles[4] = 4;
                    triangles[5] = 6;


                    //Side 2
                    triangles[6] = 2;
                    triangles[7] = 6;
                    triangles[8] = 7;

                    triangles[9] = 7;
                    triangles[10] = 3;
                    triangles[11] = 2;

                    //Side 3
                    triangles[12] = 3;
                    triangles[13] = 7;
                    triangles[14] = 5;

                    triangles[15] = 5;
                    triangles[16] = 1;
                    triangles[17] = 3;

                    //Side 4
                    triangles[18] = 1;
                    triangles[19] = 5;
                    triangles[20] = 4;

                    triangles[21] = 4;
                    triangles[22] = 0;
                    triangles[23] = 1;

                    //Bottom
                    triangles[24] = 2;
                    triangles[25] = 1;
                    triangles[26] = 0;

                    triangles[27] = 1;
                    triangles[28] = 2;
                    triangles[29] = 3;

                    //Top
                    triangles[30] = 6;
                    triangles[31] = 4;
                    triangles[32] = 5;

                    triangles[33] = 5;
                    triangles[34] = 7;
                    triangles[35] = 6;

                    mesh.triangles = triangles;

                    
                }
            }
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }






}
