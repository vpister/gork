using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class uiHelper : MonoBehaviour
{
    public GameObject textBox;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(textBox,null);
        textBox.GetComponent<InputField>().OnPointerClick(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
