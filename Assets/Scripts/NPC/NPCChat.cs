using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCChat : MonoBehaviour
{
    public GameObject panel;
    public Text NPCName;
    public Text NPCMessage;
    public string textNPCName;
    public string textNPCMessage;
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown("e"))
        {
            NPCName.text = textNPCName;
            NPCMessage.text = textNPCMessage;
            panel.SetActive(true);
        }
    }
}
