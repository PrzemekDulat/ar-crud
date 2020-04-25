using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiateAssembler : MonoBehaviour
{
    public GameObject assembler;

    public void CreateNewAssembler()
    {
        Instantiate(assembler);
    }

    public void RemoveAllObjects()
    {
            var toBeDestroyed = GameObject.FindGameObjectsWithTag("placedRectangle");

            foreach (var item in toBeDestroyed)
            {
                Destroy(item);
            }

            Destroy(GameObject.FindGameObjectWithTag("placedPlane"));
            Destroy(GameObject.FindGameObjectWithTag("rectangle"));


    }
}
