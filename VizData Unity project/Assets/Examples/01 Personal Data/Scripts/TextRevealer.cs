//this is mine
using UnityEngine;

public class TextRevealer : MonoBehaviour
{

    public GameObject textObject = null;

    void Start()
    {
        textObject.SetActive(false);
    }

    void OnMouseEnter()
    {
        textObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        textObject.SetActive(false);
    }


}
