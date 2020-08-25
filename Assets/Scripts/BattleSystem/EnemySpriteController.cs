
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteController : MonoBehaviour
{
    private int monsterSprite;
    public List<GameObject> monsterSprites;
    void Start(){
        monsterSprite = GameObject.Find("Enemy").GetComponent<Monster>().monsterSprite;
        select();
    }
    public void select()
    // Start is called before the first frame update    void select()
    {
        foreach (GameObject sprite in monsterSprites)
        {
            sprite.SetActive(false);
        }
        monsterSprites[monsterSprite].SetActive(true);
    }
}
