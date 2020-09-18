
using UnityEngine;

public class GLTranslationExample : MonoBehaviour
{
    public Material material = null;
    public int circleCount = 16;

    const int circleResoultion = 64;


    void OnRenderObject()
    {
        material.SetPass(0);

        Random.InitState(0);
        for (int i = 0; i < circleCount; i++) {
            GL.PushMatrix(); //gem nuværende state
            float y = Random.value;
            GL.MultMatrix(Matrix4x4.Translate(new Vector3(i, y, 0)));
            GLCircle(0.5f);
            GL.PopMatrix();

        }
  
    }


    void GLCircle( float radius)
    {
        //filled in circle
        GL.Begin(GL.TRIANGLE_STRIP);
        for (int i = 0; i < circleResoultion; i = i + 1){
            float t = Mathf.InverseLerp(0, circleResoultion - 1, i);
            float angle = t * Mathf.PI * 2;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            GL.Vertex3(x, y, 0);
            GL.Vertex3(0, 0, 0);

        }
        GL.End();
    }
}
