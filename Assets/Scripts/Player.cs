using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string name;
    public int str;
    public int agi;
    public int intel;

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

    void Awake()
    {

        this.doneList = new List<TodoItem>();
        this.todoList = new List<TodoItem>();
        LoadTodoList(true);
        LoadTodoList(false);
        Debug.Log(todoList.Count);
    }

    public Player(string name, int str, int agi, int intel, int gold = 0)
    {
        this.name = name;
        this.str = str;
        this.agi = agi;
        this.intel = intel;
        this.gold = gold;
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
    }

    public void LoadTodoList(bool load_done)
    {

        string path = Application.dataPath;
        if (load_done) path += "/Todo/done.txt";
        else path += "/Todo/todo.txt";
        Debug.Log(path);
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
                    dt = DateTime.ParseExact(duetime, "yyyyMMddhhmm", null);
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
    }

}

