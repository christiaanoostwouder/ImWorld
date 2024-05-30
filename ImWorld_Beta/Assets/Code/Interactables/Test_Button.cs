using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Button : Interactable
{
    public GameObject globe;
    MeshRenderer mesh;
    public Color[] colors;
    private int colourIndex;
    
    void Start()
    {
        mesh = globe.GetComponent<MeshRenderer>();
        mesh.material.color = Color.red;
    }


    protected override void Interact()//Design interaction using code.
    {
        Debug.Log("Interacted with " + gameObject.name);
        colourIndex++;
        if(colourIndex > colors.Length - 1)
        {
            colourIndex = 0;
        }

        mesh.material.color = colors[colourIndex];       
    }
}
