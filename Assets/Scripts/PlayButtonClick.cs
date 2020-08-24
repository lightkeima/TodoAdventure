using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButtonClick : MonoBehaviour
{
    public SelectedPlayer selectedPlayer;
    public void LoadScene()
    {
        SceneManager.LoadScene("_preload");
    }
}
