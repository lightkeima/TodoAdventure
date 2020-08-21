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
  public List<TodoItem> doneList = new List<TodoItem>();
  public List<TodoItem> todoList = new List<TodoItem>();

  public Player(string name, int str, int agi, int intel){
   this.name = name;
   this.str = str;
   this.agi = agi;
   this.intel = intel;
  }

  public void UpdateStat(int str, int agi, int intel){ 
    this.str += str;
    this.agi += agi;
    this.intel += agi;
  }
  
  public void CalculateStat(){
    this.hp = 20 + this.str * 5;
    this.mp = 20 + this.intel * 5;
    this.physical_damage = 4 + (int)(0.8 * this.str + 0.3 * this.agi);
    this.magical_damage = 4 + 1*this.intel;
    this.armor = 0 + (int)(this.agi * 0.2 + 0.2 * this.str); 
    this.magic_resistance = (int)(0.4*this.intel + 0.1*this.agi); 
  }
  
  public void LoadTodoList(bool load_done = false){
    string path = Application.dataPath + (load_done)?"/Todo/done.txt":"/Todo/todo.txt";
    //Debug.Log(Application.dataPath);
    StreamReader sr = new StreamReader(path);
    int number_of_item = int.Parse(sr.ReadLine());
    while(number_of_item-- != 0){
      string title = sr.ReadLine();
      string description  = sr.ReadLine();
      int h = int.Parse(sr.ReadLine());
      int min = int.Parse(sr.ReadLine());
      int d = int.Parse(sr.ReadLine());
      int m = int.Parse(sr.ReadLine());
      int y = int.Parse(sr.ReadLine());
      int isDaily = int.Parse(sr.ReadLine());
      int number_of_reward = int.Parse(sr.ReadLine());
      Dictionary<string,int> bonus = new Dictionary<string, int>();
      while(number_of_reward-- != 0){
        string type = sr.ReadLine();
        int number = int.Parse(sr.ReadLine());
        bonus.Add(type, number);
      }
      if(load_done){
if(isDaily == 0){
        this.doneList.Add(new TodoItem(title, description, new DateTime(y,m,d,h,min,0),bonus));
      }
      else {
        this.doneList.Add(new TodoItem(title, description, bonus));
      }
      }
      else{
      if(isDaily == 0){
        this.todoList.Add(new TodoItem(title, description, new DateTime(y,m,d,h,min,0),bonus));
      }
      else {
        this.todoList.Add(new TodoItem(title, description, bonus));
      }
      }
    }
    sr.Close();
  }

  void Awake(){
    LoadTodoList(true);
    LoadTodoList();
  }
}
