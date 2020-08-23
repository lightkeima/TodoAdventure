using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodoItem
{
    public string title = "";
    public string description = "";
    public DateTime duetime;

    //Daily Item
    public bool isDaily = false;
    public int numberOfDone = 0;

    public Dictionary<string, int> bonus = new Dictionary<string, int>();

    public TodoItem(string title, string description)
    {
        this.title = title;
        this.description = description;
        this.duetime = duetime;
        this.isDaily = true;
        this.numberOfDone = numberOfDone;
        this.bonus = bonus;
    }

    public TodoItem(string title, string description, DateTime duetime)
    {
        this.title = title;
        this.description = description;
        this.duetime = duetime;
        this.isDaily = false;
        this.bonus = bonus;
    }


    public TodoItem(string title, string description, Dictionary<string, int> bonus)
    {
        this.title = title;
        this.description = description;
        this.duetime = duetime;
        this.isDaily = true;
        this.numberOfDone = numberOfDone;
        this.bonus = bonus;
    }

    public TodoItem(string title, string description, DateTime duetime, Dictionary<string, int> bonus)
    {
        this.title = title;
        this.description = description;
        this.duetime = duetime;
        this.isDaily = false;
        this.bonus = bonus;
    }

    public string Summary()
    {
        string summary = title + "\n" + description + "\n" + duetime.ToString("yyyyMMddHHmm") + "\n";
        if (isDaily) summary += "1\n";
        else summary += "0\n";

        foreach (KeyValuePair<string, int> entry in bonus)
        {
          summary += entry.Key + "\n" + entry.Value.ToString() + "\n";
        }
        Debug.Log(summary);
        return summary;
    }
}
