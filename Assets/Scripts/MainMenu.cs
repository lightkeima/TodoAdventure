using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField characterName;
    public GameObject characterList;
    public GameObject content;
    public Transform SpawnPoint;
    public GameObject itemPrefab;
    public RectTransform con;
    public Button addBtn;
    public Button createPlayBtn;
    public SelectedPlayer selectedPlayer;

    public struct Character
    {
        public int id;
        public string name;
        public string create;
        public int selectedSprite;
    }
    private int currentId = 0;
    public List<Character> characters = new List<Character>();
    void Awake()
    {
        CharacterLoading();
        con.sizeDelta = new Vector2(0, (characters.Count) * 70);
        addBtn.onClick.AddListener(AddCharacter);
        createPlayBtn.onClick.AddListener(AddCharacter);
    }
    void Start()
    {
        LoadCharacterList();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void CharacterLoading()
    {
        string path = Application.dataPath + "/Player/player.txt";
        if (!File.Exists(path))
        {
            StreamWriter wr = new StreamWriter(path);
            System.Random a = new System.Random();
            wr.WriteLine(a.Next(16));
            wr.WriteLine("0");
            wr.Close();
        }
        StreamReader sr = new StreamReader(path);
        currentId = int.Parse(sr.ReadLine());
        int numberOfCharacter = int.Parse(sr.ReadLine());
        while (numberOfCharacter-- != 0)
        {
            Character character = new Character();
            character.id = int.Parse(sr.ReadLine());
            character.name = sr.ReadLine();
            character.create = sr.ReadLine();
            character.selectedSprite = LoadSelectedSprite(character.id);
            characters.Add(character);
        }
        sr.Close();
    }
    public void AddCharacter()
    {
        Character character = new Character();
        character.id = ++currentId;
        character.name = characterName.text;
        character.create = DateTime.Now.ToString("HH:mm dd/MM/yyy");
        characters.Insert(0, character);
        string path = Application.dataPath + "/Player/player.txt";
        StreamWriter wr = new StreamWriter(path);
        wr.WriteLine(currentId);
        wr.WriteLine(characters.Count);
        foreach (Character _character in characters)
        {
            wr.WriteLine(_character.id);
            wr.WriteLine(_character.name);
            wr.WriteLine(_character.create);
        }
        wr.Close();
        selectedPlayer.SelectedPlayerId = character.id.ToString();
        selectedPlayer.PlayerName = character.name;
        RemoveAllChild();
        LoadCharacterList();
    }
    public void LoadCharacterList()
    {
        con.sizeDelta = new Vector2(0, (characters.Count) * 75);
        for (int i = 0; i < characters.Count; ++i)
        {
            Vector3 pos = new Vector3(0, -(i + 1) * 75, 0);
            GameObject item = (GameObject)Instantiate(itemPrefab, pos, SpawnPoint.rotation);
            item.transform.SetParent(content.transform.GetChild(0), false);
            item.transform.GetChild(0).gameObject.GetComponent<Text>().text = characters[i].name;
            item.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Create at:" + characters[i].create;
            item.transform.GetChild(2).gameObject.transform.GetChild(characters[i].selectedSprite).gameObject.SetActive(true);
            item.name = characters[i].id.ToString();
            // item.transform.localPosition = new Vector3(0, , 0);
        }
    }

    public void RemoveAllChild()
    {
        foreach (Transform child in content.transform.GetChild(0))
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public int LoadSelectedSprite(int playerId)
    {
        int sp;
        string path = Application.dataPath + "/Players/" + playerId + ".txt";
        if (!File.Exists(path))
        {
            if (!Directory.Exists(Application.dataPath + "/Players"))
            {
                Directory.CreateDirectory(Application.dataPath + "/Players");

            }
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine("0");
            sw.WriteLine("0");
            sw.Close();
        }
        StreamReader sr = new StreamReader(path);
        sp = int.Parse(sr.ReadLine());
        sr.Close();
        return sp;
    }
}
