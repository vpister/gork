using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class uiHelper : MonoBehaviour
{
    public GameObject textBox;

    public GameObject gameText;

    private WorldGenerator wm;
    // Start is called before the first frame update
    void Start()
    {
        textBox.GetComponent<TMP_InputField>().Select();
        textBox.GetComponent<TMP_InputField>().ActivateInputField();

        textBox.GetComponent<TMP_InputField>().onEndEdit.AddListener(inputText);

        wm = GameObject.Find("WorldManager").GetComponent<WorldGenerator>();
        

    }

    public void inputText(string cmd)
    {
        EventSystem.current.SetSelectedGameObject(textBox, null);
        Debug.Log(cmd);
        string[] parts = cmd.Split(" ");

        TextMeshProUGUI text = gameText.GetComponent<TextMeshProUGUI>();

        if(parts[0].Equals("look"))
        {
            if(parts[1].Equals("up"))
            {
                text.text += wm.look(wm.heroRef.parent.x, wm.heroRef.parent.y+1) + "\n";
            }

            if (parts[1].Equals("down"))
            {
                text.text += wm.look(wm.heroRef.parent.x, wm.heroRef.parent.y - 1) + "\n";
            }

            if (parts[1].Equals("right"))
            {
                text.text += wm.look(wm.heroRef.parent.x+1, wm.heroRef.parent.y) + "\n";
            }

            if (parts[1].Equals("left"))
            {
                text.text += wm.look(wm.heroRef.parent.x - 1, wm.heroRef.parent.y) + "\n";
            }
        }

        if (parts[0].Equals("move"))
        {
            Debug.Log(wm.heroRef.parent.x + "," + wm.heroRef.parent.y);
            if (parts[1].Equals("up"))
            {
                wm.moveOccupant(wm.heroRef.parent.x, wm.heroRef.parent.y, wm.heroRef.parent.x, wm.heroRef.parent.y + 1);
            }

            if (parts[1].Equals("down"))
            {
                wm.moveOccupant(wm.heroRef.parent.x, wm.heroRef.parent.y, wm.heroRef.parent.x, wm.heroRef.parent.y - 1);
            }

            if (parts[1].Equals("right"))
            {
                wm.moveOccupant(wm.heroRef.parent.x, wm.heroRef.parent.y, wm.heroRef.parent.x+1, wm.heroRef.parent.y);
            }

            if (parts[1].Equals("left"))
            {
                wm.moveOccupant(wm.heroRef.parent.x, wm.heroRef.parent.y, wm.heroRef.parent.x-1, wm.heroRef.parent.y);
            }
        }


        textBox.GetComponent<TMP_InputField>().text = "";
        textBox.GetComponent<TMP_InputField>().Select();
        textBox.GetComponent<TMP_InputField>().ActivateInputField();
    }
    // Update is called once per frame
    void Update()
    {
        EventSystem.current.SetSelectedGameObject(textBox, null);
    }
}
