using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColour : MonoBehaviour
{
    [SerializeField] private Material material;

    public void Green()
    {
        material.color = Color.green;
    }
    
    public void Red()
    {
        material.color = Color.red;
    }
    
    public void Blue()
    {
        material.color = Color.blue;
    }
}
