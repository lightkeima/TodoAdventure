using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWantedBoard : MonoBehaviour
{
    public GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown("e"))
        {
            GameObject gameObject = FindObject(GameObject.Find("Canvas"),"Wanted board");
            gameObject.SetActive(true);
        }
    }

}
