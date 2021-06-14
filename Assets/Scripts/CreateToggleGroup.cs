using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateToggleGroup : MonoBehaviour
{
    [Range(1, 3)]
    public int buttonCount = 3;
    public GameObject buttonPrefab = null;

    private ToggleGroup toggleGroup = null;

    private void Awake()
    {
        
    }

    //used to instatiate in new buttons
    private void CreateButtons()
    {
        for (int i = 0; i < buttonCount; i++)
        {
            //create button and set the parent
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(transform);

            //set text
            string area = "Colour";
        }
    }
}
