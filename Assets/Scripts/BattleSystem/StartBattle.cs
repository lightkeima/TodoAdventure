using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartBattle : MonoBehaviour
{
    public Monster enemy;
    public int attackAnimIndex;
    public int bgid;
    public int mid;
    public string monsterName;
    public int hp;
    public int mp;
    public int physical_damage;
    public int magical_damage;
    public int armor;
    public float fire_resistance;
    public float water_resistance;
    public float earth_resistance;
    public float wind_resistance;
    public float darkness_resistance;
    public float magic_resistance;
    public int monsterSprite;
    void Start(){
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnStartBattleButtonClick);
    }
    void SetEnemyStat()
    {
        enemy.bgid = bgid;
        enemy.mid = this.mid;
        enemy.attackAnimIndex = this.attackAnimIndex;
        enemy.monsterName = this.monsterName;
        enemy.hp = this.hp;
        enemy.mp = this.mp;
        enemy.physical_damage = this.physical_damage;
        enemy.magical_damage = this.magical_damage;
        enemy.armor = this.armor;
        enemy.fire_resistance = this.fire_resistance;
        enemy.water_resistance = this.water_resistance;
        enemy.earth_resistance = this.earth_resistance;
        enemy.wind_resistance = this.wind_resistance;
        enemy.darkness_resistance = this.darkness_resistance;
        enemy.magic_resistance = this.magic_resistance;
        enemy.monsterSprite = this.monsterSprite;
    }
    public void OnStartBattleButtonClick()
    {
        SetEnemyStat();
        SceneManager.LoadScene("BattleScene");
    }
}
