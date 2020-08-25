using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{

    public GameObject attackSound;
    public List<GameObject> playerAttackedAnim;
    public List<GameObject> monsterAttackedAnim;

    public List<GameObject> backgrounds;
    //UI
    public GameObject controllPanel;
    public Text descriptionText;
    public Button attackBtn;
    public Button defBtn;
    public Button skillsBtn;
    public Button itemBtn;
    public Button escapeBtn;

    public Text pHp;
    public Text pMp;
    public Text eHp;
    public Text eMp;

    bool playerDef;
    bool player_turn;
    bool enemy_turn;
    public Player player;
    public Monster monster;
    // Player
    public int p_current_hp;
    public int p_current_mp;
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
    public int shield = 0;
    //Bonus elemental damage + percent
    float fire_damage = 0;
    float water_damage = 0;
    float wind_damage = 0;
    float earth_damage = 0;
    float darkness_damage = 0;

    // Enemy
    public int monsterAttactAnimationIndex;
    public int monsterBackgourndIndex;
    public int e_current_hp;
    public int e_current_mp;
    public int e_hp = 20;
    public int e_mp = 20;
    public int e_physical_damage = 4;
    public int e_magical_damage = 4;
    public int e_armor = 0;
    public float e_fire_resistance = 0;
    public float e_water_resistance = 0;
    public float e_earth_resistance = 0;
    public float e_wind_resistance = 0;
    public float e_darkness_resistance = 0;
    public float e_magic_resistance = 0;
    // Start is called before the first frame update
    public int e_shield = 0;
    public int damage;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        monster = GameObject.Find("Enemy").GetComponent<Monster>();
    }
    void Start()
    {
        player_turn = true;
        enemy_turn = false;
        p_current_hp = player.hp;
        p_current_mp = player.mp;
        hp = player.hp;
        mp = player.mp;
        physical_damage = player.physical_damage;
        magical_damage = player.magical_damage;
        armor = player.armor;
        fire_resistance = player.fire_resistance;
        water_resistance = player.water_resistance;
        earth_resistance = player.earth_resistance;
        wind_resistance = player.wind_resistance;
        darkness_resistance = player.darkness_resistance;
        magic_resistance = player.magic_resistance;
        shield = 0;
        //Bonus elemental damage + percent
        fire_damage = 0;
        water_damage = 0;
        wind_damage = 0;
        earth_damage = 0;
        darkness_damage = 0;
        // Enemy
        monsterAttactAnimationIndex = monster.attackAnimIndex;
        monsterBackgourndIndex = monster.bgid;
        e_current_hp = monster.hp;
        e_current_mp = monster.mp;
        e_hp = monster.hp;
        e_mp = monster.mp;
        e_physical_damage = monster.physical_damage;
        e_magical_damage = monster.magical_damage;
        e_armor = monster.armor;
        e_fire_resistance = monster.fire_resistance;
        e_water_resistance = monster.water_resistance;
        e_earth_resistance = monster.earth_resistance;
        e_wind_resistance = monster.wind_resistance;
        e_darkness_resistance = monster.darkness_resistance;
        e_magic_resistance = monster.magic_resistance;
        // Start is called before the first frame update
        e_shield = 0;
        attackBtn.onClick.AddListener(OnAttackButtonClick);
        defBtn.onClick.AddListener(OnDefButtonClick);
        //skillsBtn.text;
        //itemBtn;
        escapeBtn.onClick.AddListener(OnEscapeButtonClick);
        //descriptionText.text = monster.mosnterName + "approached!";
        UpdateHPMPPanel();
        foreach (GameObject background in backgrounds)
        {
            background.SetActive(false);
        }
        backgrounds[monsterBackgourndIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (player_turn)
        {
            controllPanel.SetActive(true);
        }
        else controllPanel.SetActive(false);
        if (enemy_turn) StartCoroutine(MonsterAttack());
    }
    /*
    public void Action(
     int p_current_hp,
     int p_current_mp,
     int hp = 20,
     int mp = 20,
     int physical_damage = 4,
     int magical_damage = 4,
     int armor = 0,
     float fire_resistance = 0,
     float water_resistance = 0,
     float earth_resistance = 0,
     float wind_resistance = 0,
     float darkness_resistance = 0,
     float magic_resistance = 0,
     int e_current_hp,
     int e_current_mp,
     int e_hp = 20,
     int e_mp = 20,
     int e_physical_damage = 4,
     int e_magical_damage = 4,
     int e_armor = 0,
     float e_fire_resistance = 0,
     float e_water_resistance = 0,
     float e_earth_resistance = 0,
     float e_wind_resistance = 0,
     float e_darkness_resistance = 0)
    {
    }
    */
    void UpdateHPMPPanel()
    {
        if (p_current_hp >= 0)
            pHp.text = p_current_hp + "/" + hp;
        else pHp.text = "0/" + hp;

        if (p_current_mp >= 0)
            pMp.text = p_current_mp + "/" + mp;
        else pMp.text = "0/" + mp;

        if (e_current_hp >= 0)
            eHp.text = e_current_hp + "/" + e_hp;
        else eHp.text = "0/" + e_hp;

        if (e_current_mp >= 0)
            eMp.text = e_current_mp + "/" + e_mp;
        else
            eMp.text = "0/" + e_mp;

    }
    public int DealDamge(bool player, float weight_physical, float weight_magical_damage,
    int fire_damage, int water_damage, int wind_damage, int earth_damage, int darkness_damage)
    {
        if (player)
        {
            float _physical_damage = physical_damage * weight_physical - e_armor;
            if (_physical_damage < 0) _physical_damage = 0;
            float _magical_damage = magical_damage * weight_magical_damage - e_magic_resistance;
            if (_magical_damage < 0) _magical_damage = 0;
            damage = (int)(_physical_damage * weight_physical +
              _magical_damage * weight_magical_damage
              + fire_damage * ((100 + this.fire_damage) / 100) * (100 - e_fire_resistance) / 100
              + water_damage * ((100 + this.water_damage) / 100) * (100 - e_water_resistance) / 100
              + wind_damage * ((100 + this.wind_damage) / 100) * (100 - e_wind_resistance) / 100
              + earth_damage * ((100 + this.earth_damage) / 100) * (100 - e_earth_resistance) / 100
               + darkness_damage * ((100 + this.darkness_damage) / 100) * (100 - e_darkness_resistance) / 100);
            e_current_hp -= damage;
            return damage;
        }
        else
        {
            float _physical_damage = e_physical_damage * weight_physical - armor;
            if (_physical_damage < 0) _physical_damage = 0;
            float _magical_damage = e_magical_damage * weight_magical_damage - magic_resistance;
            if (_magical_damage < 0) _magical_damage = 0;
            damage = (int)(_physical_damage * weight_physical +
              _magical_damage * weight_magical_damage
              + fire_damage * (100 - fire_resistance) / 100
              + water_damage * (100 - water_resistance) / 100
              + wind_damage * (100 - wind_resistance) / 100
              + earth_damage * (100 - earth_resistance) / 100
               + darkness_damage * (100 - darkness_resistance) / 100);
            if (playerDef) p_current_hp -= (int)(damage * 0.3);
            else p_current_hp -= damage;
            return damage;
        }
    }
    public void Heal(bool player, int hp, int mp, int hppercent, int mppercent)
    {
        if (player)
        {
            p_current_hp = p_current_hp + hp + this.hp * hppercent;
            p_current_mp = p_current_mp + mp + this.mp * mppercent;

        }
        else
        {
            e_current_hp = p_current_hp + hp + this.hp * hppercent;
            e_current_mp = p_current_mp + mp + this.mp * mppercent;
        }
    }
    /*
    public void Buff(bool player,
    float fire_resistance, float water_resistance, float wind_resistance, float earth_resistance, float darkness_resistance,
    float fire_damage, float water_damage, float wind_damage, float earth_damage, float darkness_damage
    )
    {

    }
    public void DeBuff(bool player,
    float fire_resistance, float water_resistance, float wind_resistance, float earth_resistance, float darkness_resistance,
    float fire_damage, float water_damage, float wind_damage, float earth_damage, float darkness_damage
    )
    {

    }

    */
    IEnumerator MonsterAttack()
    {
        descriptionText.text = "Enemy attacked you deal " + DealDamge(false, 1, 0, 0, 0, 0, 0, 0).ToString() + " damage";
        UpdateHPMPPanel();
        playerDef = false;
        enemy_turn = false;
        playerAttackedAnim[monsterAttactAnimationIndex].SetActive(true);
        attackSound.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        playerAttackedAnim[monsterAttactAnimationIndex].SetActive(false);
        attackSound.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        if (p_current_hp <= 0)
        {
            descriptionText.text = "You lose!";
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Village");
        }
        player_turn = true;
    }
    IEnumerator Attack()
    {
        descriptionText.text = "You deal " + DealDamge(true, 1, 0, 0, 0, 0, 0, 0).ToString() + " damage to emeny";
        UpdateHPMPPanel();
        player_turn = false;
        monsterAttackedAnim[3].SetActive(true);
        attackSound.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        monsterAttackedAnim[3].SetActive(false);
        attackSound.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        if (e_current_hp <= 0)
        {
            descriptionText.text = "Enemy defeated!";
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Town");
        }
        enemy_turn = true;
    }
    IEnumerator Def()
    {
        descriptionText.text = "You are defending!";
        playerDef = true;
        player_turn = false;
        yield return new WaitForSeconds(1.5f);
        enemy_turn = true;
    }
    IEnumerator Escape()
    {
        descriptionText.text = "You run for your life!";
        playerDef = false;
        enemy_turn = false;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("TOWN");
    }
    void OnAttackButtonClick()
    {
        StartCoroutine(Attack());
    }
    void OnDefButtonClick()
    {
        StartCoroutine(Def());
    }
    void OnEscapeButtonClick()
    {
        StartCoroutine(Escape());
    }
}
