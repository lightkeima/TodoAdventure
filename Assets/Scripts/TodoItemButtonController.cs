using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TodoItemButtonController : MonoBehaviour
{
    public GameObject content;
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
    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        content = FindObject(canvas, "Todo Content");
        this.gameObject.GetComponent<Button>().onClick.AddListener(SendMessage);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SendMessage()
    {
        content.SetActive(true);
        content.GetComponent<TodoContent>().SetTexts(int.Parse(this.gameObject.name));
    }

}
