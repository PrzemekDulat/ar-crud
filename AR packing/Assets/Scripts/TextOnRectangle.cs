using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOnRectangle : MonoBehaviour
{
    public Text nameText;

    void Start()
    {
        nameText.text = this.GetComponentInParent<RandomColor>().name;
    }

}
