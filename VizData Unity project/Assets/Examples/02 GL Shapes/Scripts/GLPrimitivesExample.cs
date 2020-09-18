using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLPrimitivesExample : MonoBehaviour
{
    public Material material;

    void OnRenderObject()
    {
        material.SetPass(0);

        GL.wireframe = true;

        //Draw triangle
        //note to self: tegn altid med uret så normals vender den rigtige vej.
        GL.Begin(GL.TRIANGLES);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0, 1, 0);
        GL.Vertex3(1, 1, 0);
        GL.End();

        //draw quad
        GL.Begin(GL.QUADS);
        GL.Vertex3(2, 0, 0);
        GL.Vertex3(2, 1, 0);
        GL.Vertex3(3, 1, 0);
        GL.Vertex3(3, 0, 0);
        GL.End();

        //draw line with more than two points
        GL.Begin(GL.LINE_STRIP);
        GL.Vertex3(5 + 0.5f, -2, 0);
        GL.Vertex3(5 - 0.5f, -1, 0);
        GL.Vertex3(5 + 0.5f, 0, 0);
        GL.Vertex3(5 - 0.5f, 1, 0);
        GL.Vertex3(5 + 0.5f, 2, 0);
        GL.End();

        //Draw ALLLLL the lines
        GL.Begin(GL.LINES);
        GL.Vertex3(7 + 0.5f, -2, 0);
        GL.Vertex3(7 - 0.5f, -1, 0);
        GL.Vertex3(7 + 0.5f, 0, 0);
        GL.Vertex3(7 - 0.5f, 1, 0);
        GL.Vertex3(7 + 0.5f, 2, 0);
        GL.End();

        //send help
        GL.Begin(GL.TRIANGLE_STRIP);
        GL.Vertex3(9 + 0.5f, -2, 0);
        GL.Vertex3(9 - 0.5f, -1, 0);
        GL.Vertex3(9 + 0.5f, 0, 0);
        GL.Vertex3(9 - 0.5f, 1, 0);
        GL.Vertex3(9 + 0.5f, 2, 0);
        GL.End();

        GL.wireframe = false;
    }
}
