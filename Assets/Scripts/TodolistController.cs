using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TodolistController : MonoBehaviour
{
    public GameObject playerObj;
    public Transform SpawnPoint;
    Player player;
    public GameObject content;
    int currentNumberOfItem = 0;
    public GameObject itemPrefab;
    public RectTransform con;
    void Awake()
    {
        player = this.playerObj.GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        con.sizeDelta = new Vector2(0, (player.todoList.Count) * 35);
        LoadTodoList();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNumberOfItem != player.todoList.Count)
        {
            RemoveAllChild();
            LoadTodoList();
        }
    }
    public void LoadTodoList()
    {
        currentNumberOfItem = player.todoList.Count;
        con.sizeDelta = new Vector2(0, (player.todoList.Count) * 35);
        for (int i = 0; i < player.todoList.Count; ++i)
        {
            Vector3 pos = new Vector3(0, -(i + 1) * 35, 0);
            GameObject item = (GameObject)Instantiate(itemPrefab, pos, SpawnPoint.rotation);
            item.transform.SetParent(content.transform.GetChild(0), false);
            item.transform.GetChild(0).gameObject.GetComponent<Text>().text = player.todoList[i].title;
            item.name = i.ToString();
            // item.transform.localPosition = new Vector3(0, , 0);
        }
    }
    public void RemoveAllChild()
    {
        foreach (Transform child in content.transform.GetChild(0))
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
