using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodoItem
{
  private string title;
  private string description;
  private DateTime duetime;
  
  //Daily Item
  private bool isDaily;
  private int numberOfDone;

  private Dictionary<string,int> bonus;

  public TodoItem(string title, string description, Dictionary<string,int> bonus){
    this.title = title;
    this.description = description;
    this.duetime = duetime;
    this.isDaily = true;
    this.numberOfDone = numberOfDone;
    this.bonus = bonus;
  }

  public TodoItem(string title, string description, DateTime duetime, Dictionary<string, int> bonus){
    this.title = title;
    this.description = description;
    this.duetime = duetime;
    this.isDaily = false;
    this.bonus = bonus;
  }
}
