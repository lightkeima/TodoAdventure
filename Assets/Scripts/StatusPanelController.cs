using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusPanelController : MonoBehaviour
{
    public GameObject playerObj;

    public List<GameObject> sprites;
    Player player;
    // Start is called before the first frame update
    void Awake()
    {
        player = this.playerObj.GetComponent<Player>();
    }

    void Start()
    {
        SetTexts();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf == true)
        {
            if (player.stat_change == true)
            {
                SetTexts();
                player.stat_change = false;
            }
            SetImage(player.selectedSprite);
        }
    }
    public void SetTexts()
    {
        GameObject panel1 = this.gameObject.transform.GetChild(1).gameObject;
        panel1.transform.GetChild(0).gameObject.GetComponent<Text>().text = player.playerName;
        panel1.transform.GetChild(3).gameObject.GetComponent<Text>().text = player.str.ToString();
        panel1.transform.GetChild(5).gameObject.GetComponent<Text>().text = player.agi.ToString();
        panel1.transform.GetChild(7).gameObject.GetComponent<Text>().text = player.intel.ToString();
        panel1.transform.GetChild(9).gameObject.GetComponent<Text>().text = player.magic_resistance.ToString();
        panel1.transform.GetChild(11).gameObject.GetComponent<Text>().text = player.fire_resistance.ToString();
        panel1.transform.GetChild(13).gameObject.GetComponent<Text>().text = player.water_resistance.ToString();
        panel1.transform.GetChild(15).gameObject.GetComponent<Text>().text = player.earth_resistance.ToString();
        panel1.transform.GetChild(17).gameObject.GetComponent<Text>().text = player.wind_resistance.ToString();
        panel1.transform.GetChild(19).gameObject.GetComponent<Text>().text = player.darkness_resistance.ToString();
        GameObject panel2 = this.gameObject.transform.GetChild(2).gameObject;
        panel2.transform.GetChild(1).gameObject.GetComponent<Text>().text = player.hp.ToString();
        panel2.transform.GetChild(3).gameObject.GetComponent<Text>().text = player.mp.ToString();
        panel2.transform.GetChild(5).gameObject.GetComponent<Text>().text = player.physical_damage.ToString();
        panel2.transform.GetChild(7).gameObject.GetComponent<Text>().text = player.magical_damage.ToString();
        panel2.transform.GetChild(9).gameObject.GetComponent<Text>().text = player.armor.ToString();
    }
    public void SetImage(int selected)
    {
        foreach (GameObject sprite in sprites)
        {
            sprite.SetActive(false);
        }
        sprites[selected].SetActive(true);
    }
}
