﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectCharacterButton : MonoBehaviour
{
    public GameObject selectedPlayerObject;
    public SelectedPlayer selectedPlayer;
    // Start is called before the first frame update
    void Start()
    {
        selectedPlayerObject = GameObject.Find("PlayerData");
        selectedPlayer = selectedPlayerObject.GetComponent<SelectedPlayer>();
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnClick(){
        selectedPlayer.SelectedPlayerId = this.gameObject.name;
        selectedPlayer.PlayerName = this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
    }
}
