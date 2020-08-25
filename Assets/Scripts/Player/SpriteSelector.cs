using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSelector : MonoBehaviour
{
    public List<GameObject> sprites;
    Player selectedSprite;
    private int selected = 0;
    void Start()
    {
        selectedSprite = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
    {
        if (selected != selectedSprite.selectedSprite)
        {
            selected = selectedSprite.selectedSprite;
            select();
        }
    }
    void select()
    {
        foreach (GameObject sprite in sprites)
        {
            sprite.SetActive(false);
        }
        sprites[selected].SetActive(true);
    }
}
