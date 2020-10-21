using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
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

