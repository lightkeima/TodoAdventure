using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TodoContent : MonoBehaviour
{
    private int index;
    public Text title;
    public Text description;
    public GameObject duetime;
    public Text reward1;
    public Text reward2;
    public Player player;
    public Button done;
    // Start is called before the first frame update
    public void SetTexts(int index)
    {
        this.index = index;
        title.text = player.todoList[index].title;
        description.text = player.todoList[index].description;
        if (player.todoList[index].noduetime != null)
        {
            duetime.SetActive(true);
            duetime.GetComponent<Text>().text = "Duetime:" + player.todoList[index].duetime.ToString("HH:mm dd/MM/yyyy");
        }
        else duetime.SetActive(false);
        List<string> key = new List<string>();
        List<string> value = new List<string>();
        foreach (KeyValuePair<string, int> item in player.todoList[index].bonus)
        {
            key.Add(item.Key);
            value.Add(item.Value.ToString());
        }
        if (key.Count == 1)
        {
            reward1.text = key[0] + " +" + value[0];
        }
        else if (key.Count == 2)
        {
            reward1.text = key[0] + " +" + value[0] + "\n" + key[1] + " +" + value[1];
        }
        else if (key.Count == 3)
        {
            reward1.text = key[0] + " +" + value[0] + "\n" + key[1] + " +" + value[1];
            reward2.text = key[2] + " +" + value[2];
        }
        else if (key.Count == 4)
        {
            reward1.text = key[0] + " +" + value[0] + "\n" + key[1] + " +" + value[1];
            reward2.text = key[2] + " +" + value[2] + "\n" + key[3] + " +" + value[3];
        }
        done.onClick.AddListener(OnDoneClick);
    }
    public void OnDoneClick()
    {
        if (this.gameObject.activeSelf)
        {
            Debug.Log(this.index);
            player.MarkDone(this.index);
            this.gameObject.SetActive(false);
        }
    }
}
