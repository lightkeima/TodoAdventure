using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName = "Default";
    public string playerId;
    public int str;
    public int agi;
    public int intel;
    public int selectedSprite;
    public int gold = 0;
    public int hp = 20;
    public int mp = 20;
    public int physical_damage = 4;
    public int magical_damage = 4;
    public int armor = 0;
    public float fire_resistance = 0;
    public float water_resistance = 0;
    public float earth_resistance = 0;
    public float wind_resistance = 0;
    public float darkness_resistance = 0;
    public float magic_resistance = 0;

    // TodoItem
    public List<TodoItem> doneList;
    public List<TodoItem> todoList;
    public List<int> defeatList;
    public bool stat_change = false;
    void Awake()
    {
        SelectedPlayer selectedPlayer = GameObject.Find("PlayerData").GetComponent<SelectedPlayer>();
        playerId = selectedPlayer.SelectedPlayerId;
        playerName = selectedPlayer.PlayerName;
        this.doneList = new List<TodoItem>();
        this.todoList = new List<TodoItem>();
        this.defeatList = new List<int>();
        LoadTodoList(true);
        LoadTodoList(false);
        CalculateAttributes();
        CalculateStat();
        LoadGoldAndSprite();
        LoadDefeatList();
    }
    public void UpdateStat(int str, int agi, int intel)
    {
        this.str += str;
        this.agi += agi;
        this.intel += agi;
    }

    public void CalculateAttributes()
    {
        foreach (var item in doneList)
        {
            if (item.bonus.ContainsKey("strength"))
                str += item.bonus["strength"];
            if (item.bonus.ContainsKey("agility"))
                agi += item.bonus["agility"];
            if (item.bonus.ContainsKey("intelligence"))
                intel += item.bonus["intelligence"];
        }
    }

    public void CalculateStat()
    {
        this.hp = 20 + this.str * 5;
        this.mp = 20 + this.intel * 5;
        this.physical_damage = 4 + (int)(0.8 * this.str + 0.3 * this.agi);
        this.magical_damage = 4 + 1 * this.intel;
        this.armor = 0 + (int)(this.agi * 0.2 + 0.2 * this.str);
        this.magic_resistance = (int)(0.4 * this.intel + 0.1 * this.agi);
        stat_change = true;
    }

    public void LoadTodoList(bool load_done)
    {

        string path = Application.dataPath;
        if (load_done) path += "/Todo/" + playerId + "_done.txt";
        else path += "/Todo/" + playerId + "_todo.txt";
        if (!File.Exists(path))
        {
            if (!Directory.Exists(Application.dataPath + "/Todo"))
            {
                Directory.CreateDirectory(Application.dataPath + "/Todo");

            }
            StreamWriter wr = new StreamWriter(path);
            wr.WriteLine("0");
            wr.Close();
        }
        StreamReader sr = new StreamReader(path);
        int number_of_item = int.Parse(sr.ReadLine());
        while (number_of_item-- != 0)
        {
            string title = sr.ReadLine();
            string description = sr.ReadLine();
            string duetime = sr.ReadLine();
            int isDaily = int.Parse(sr.ReadLine());
            int number_of_reward = int.Parse(sr.ReadLine());
            Dictionary<string, int> bonus = new Dictionary<string, int>();
            while (number_of_reward-- != 0)
            {
                string type = sr.ReadLine();
                int number = int.Parse(sr.ReadLine());
                bonus.Add(type, number);
            }
            if (load_done == true)
            {
                if (isDaily == 0)
                {
                    DateTime dt = new DateTime();
                    dt = DateTime.ParseExact(duetime, "yyyyMMddHHmm", null);
                    this.doneList.Add(new TodoItem(title, description, dt, bonus));
                }
                else
                {
                    this.doneList.Add(new TodoItem(title, description, bonus));
                }
            }
            else
            {
                if (isDaily == 0)
                {
                    DateTime dt = new DateTime();
                    dt = DateTime.ParseExact(duetime, "yyyyMMddHHmm", null);
                    this.todoList.Add(new TodoItem(title, description, dt, bonus));
                }
                else
                {
                    this.todoList.Add(new TodoItem(title, description, bonus));
                }
            }
        }
        sr.Close();
    }
    public void SaveTodo(bool load_done)
    {
        List<TodoItem> list;
        string path = Application.dataPath;
        if (!Directory.Exists(Application.dataPath + "/Todo"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Todo");

        }
        if (load_done)
        {
            list = doneList;
            path += "/Todo/" + playerId + "_done.txt";
        }
        else
        {
            list = todoList;
            path += "/Todo/" + playerId + "_todo.txt";
        }
        StreamWriter wr = new StreamWriter(path);
        wr.WriteLine(list.Count.ToString());
        for (int i = 0; i < list.Count; ++i)
        {
            wr.WriteLine(list[i].title);
            wr.WriteLine(list[i].description);
            wr.WriteLine(list[i].duetime.ToString("yyyyMMddHHmm"));
            int isDaily = list[i].isDaily ? 1 : 0;
            wr.WriteLine(isDaily.ToString());
            wr.WriteLine(list[i].bonus.Count.ToString());
            foreach (KeyValuePair<string, int> item in list[i].bonus)
            {
                wr.WriteLine(item.Key);
                wr.WriteLine(item.Value.ToString());
            }
        }
        wr.Close();
    }
    public void SummaryTodoList()
    {
        foreach (var item in todoList)
        {
            item.Summary();
        }
    }
    public void MarkDone(int index)
    {
        doneList.Add(todoList[index]);
        if (todoList[index].bonus.ContainsKey("gold"))
            this.gold += todoList[index].bonus["gold"];
        if (todoList[index].bonus.ContainsKey("strength"))
            str += todoList[index].bonus["strength"];
        if (todoList[index].bonus.ContainsKey("agility"))
            agi += todoList[index].bonus["agility"];
        if (todoList[index].bonus.ContainsKey("intelligence"))
            intel += todoList[index].bonus["intelligence"];
        todoList.RemoveAt(index);
        SaveTodo(true);
        SaveTodo(false);
        CalculateStat();
    }
    public void SaveGoldAndSprite()
    {
        string path = Application.dataPath + "/Players/" + playerId + ".txt";
        StreamWriter sw = new StreamWriter(path);
        sw.WriteLine(selectedSprite);
        sw.WriteLine(gold);
        sw.Close();
    }
    public void LoadGoldAndSprite()
    {
        string path = Application.dataPath + "/Players/" + playerId + ".txt";
        if (!File.Exists(path))
        {
            if (!Directory.Exists(Application.dataPath + "/Players"))
            {
                Directory.CreateDirectory(Application.dataPath + "/Players");

            }
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine("0");
            sw.WriteLine("0");
            sw.Close();
        }
        StreamReader sr = new StreamReader(path);
        selectedSprite = int.Parse(sr.ReadLine());
        gold = int.Parse(sr.ReadLine());
        sr.Close();
    }
    public void LoadDefeatList()
    {
        string path = Application.dataPath + "/Players/" + playerId + "_defeatList.txt";
        if (!Directory.Exists(Application.dataPath + "/Players"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Players");

        }
        if (!File.Exists(path))
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine("0");
            sw.Close();
        }
        StreamReader sr = new StreamReader(path);
        int n = int.Parse(sr.ReadLine());
        for (int i = 0; i < n; ++i)
        {
            defeatList.Add(int.Parse(sr.ReadLine()));
        }
        sr.Close();
    }

    public void SaveDefeatList()
    {
        string path = Application.dataPath + "/Players/" + playerId + "_defeatList.txt";
        if (!Directory.Exists(Application.dataPath + "/Players"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Players");

        }
        StreamWriter wr = new StreamWriter(path);
        wr.WriteLine(defeatList.Count.ToString());
        for (int i = 0; i < defeatList.Count; ++i)
        {
            wr.WriteLine(defeatList[i]);
        }
        wr.Close();
    }
}

