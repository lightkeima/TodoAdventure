using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int mid;
    public int attackAnimIndex;
    public int bgid;
    public string monsterName = "default";
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
    public int monsterSprite;
}
