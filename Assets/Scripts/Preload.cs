using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Preload : MonoBehaviour
{
    public List<GameObject> objects;
    void Awake()
    {
        foreach(GameObject obj in objects){
            DontDestroyOnLoad(obj);
        }
    }
    void LateUpdate()
    {
        SceneManager.LoadScene("Village");
    }
}
