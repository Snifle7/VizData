
using UnityEngine;

public class GLOscilationExamply : MonoBehaviour
{

    public Material material = null;
    public int resolution = 64;

    [Header("Wave")]
    public float waveRevolutions = 6;

    [Header("Circle")]
    [Range(1,20)]public float circleRadius = 1;

    [Header("Spiral")]
    [Range(1, 20)] public float spiralRadiusMax = 1;

    [Header("Wavy Circle")]
    [Range(1, 20)] public float wavyCircleRadius = 5;
    public float wavyCircleWavyness = 0.5f;
    public int wavyCircleWaveRevolutions = 6;

    [Header("Distorted Circle")]
    [Range(1, 20)] public float distortedCircleRadius = 10;
    public float distortedCircleRandomness = 0.5f;



    void OnRenderObject()
    {
        material.SetPass(0);

        //soundwaves
        GL.Begin(GL.LINE_STRIP);
        for (int i = 0; i < resolution; i=i+1){
            float t = Mathf.InverseLerp(0, resolution - 1, i);
            float x = t * 10;
            float angle = t * Mathf.PI * 2 * waveRevolutions;
            float y = Mathf.Sin(angle) * 0.5f;
            GL.Vertex3(x, y, 0);

        }
        GL.End();

        //circles and stuff
        GL.Begin(GL.LINE_STRIP);
        for (int i = 0; i < resolution; i = i + 1) {
            float t = Mathf.InverseLerp(0, resolution - 1, i);
            float angle = t * Mathf.PI * 2;
            float x = Mathf.Cos(angle) * circleRadius;
            float y = Mathf.Sin(angle) * circleRadius;
            GL.Vertex3(x, y, 0);

        }
        GL.End();


        //Sprial. 
        GL.Begin(GL.LINE_STRIP);
        for (int i = 0; i < resolution; i = i + 1){
            float t = Mathf.InverseLerp(0, resolution - 1, i);
            float angle = t * Mathf.PI * 2;
            float radius = spiralRadiusMax * t; //fordi vi godt kan lide pæne ting så vi ikke skal gentage os
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            GL.Vertex3(x, y, 0);

        }
        GL.End();


        //squiggly circle
        GL.Begin(GL.LINE_STRIP);
        for (int i = 0; i < resolution; i = i + 1) {
            float t = Mathf.InverseLerp(0, resolution - 1, i);
            float circleAngle = t * Mathf.PI * 2;
            float waveAngle = t * Mathf.PI * 2 * wavyCircleWaveRevolutions;
            float waveAmplitude = Mathf.Sin(waveAngle) * wavyCircleWavyness;
            float radius = wavyCircleRadius + waveAmplitude;
            float x = Mathf.Cos(circleAngle) * radius;
            float y = Mathf.Sin(circleAngle) * radius;
            GL.Vertex3(x, y, 0);

        }
        GL.End();


        //noisy circle
        GL.Begin(GL.LINE_STRIP);
        Random.InitState(0); //start at 0 cause we want consistency
        for (int i = 0; i < resolution; i = i + 1) {
            float t = Mathf.InverseLerp(0, resolution - 1, i);
            float angle = t * Mathf.PI * 2;
            float radius = distortedCircleRadius + Random.value*distortedCircleRandomness;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            GL.Vertex3(x, y, 0);

        }
        GL.End();
    }
}
