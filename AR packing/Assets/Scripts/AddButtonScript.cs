using Assets.Scenes.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButtonScript : MonoBehaviour
{

    public static AddButtonScript instance;
    public Dropdown dropdownXY;
    public Dropdown dropdownObject;
    public RectTransform menu;
    public InputField inputField;
    public InputField countInputField;
    public Text debugText;
    List<string> objectsInDropdown = new List<string>();
    public static Dictionary<string, Dimensions> objects = new Dictionary<string, Dimensions>();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one AddButtonScript is active");
            return;
        }

        instance = this;
    }

    public void AddObjectToList()
    {
        foreach (var key in objects.Keys)
        {
            if (dropdownObject.options[dropdownObject.value].text == key)
            {
                if (dropdownXY.value == 0)
                {
                    objects[key].x = Int32.Parse(inputField.text) * 10;
                }
                else if (dropdownXY.value == 1)
                {
                    objects[key].y = Int32.Parse(inputField.text) * 10;
                }

                objects[key].count = Int32.Parse(countInputField.text);

            }
        }
        


        debugText.text += "XX" + " : " + objects[dropdownObject.options[dropdownObject.value].text].x.ToString() + " : " + objects[dropdownObject.options[dropdownObject.value].text].y.ToString() + objects[dropdownObject.options[dropdownObject.value].text].count.ToString() + "\n";


        menu.gameObject.SetActive(false);
    }
    int counter = 1;


    public void AddNewObjectToDictionary()
    {
        Dimensions dim = new Dimensions();
        dim.x = 0;
        dim.y = 0;
        dim.count = 1;

        if (counter == 1)
        {
            objects.Add("Object " + (objectsInDropdown.Count + 1), dim);

            objectsInDropdown.Add("Object " + (objectsInDropdown.Count + 1));
            dropdownObject.ClearOptions();
            dropdownObject.AddOptions(objectsInDropdown);
            dropdownObject.value = objectsInDropdown.Count;
            dropdownXY.value = 0;
            counter = 0;
            //countInputField.text = "";
            countInputField.text = "1";

        }
        else
        {
            dropdownXY.value = 1;
            countInputField.text = objects[dropdownObject.options[dropdownObject.value].text].count.ToString();
            counter++;
        }
    }

    public Dictionary<string, Dimensions> GetObjects()
    {
        return objects;
    }
}
