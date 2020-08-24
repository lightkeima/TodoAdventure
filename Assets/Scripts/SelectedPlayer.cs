using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPlayer : MonoBehaviour
{
    public string SelectedPlayerId;
    public string PlayerName;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
