using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddTodo : MonoBehaviour
{
    public Button cancelBtn;
    public Button nextBtn;
    public Button doneBtn;

    public Text title;

    public Text description;

    public Text dd;

    public Text MM;

    public Text yyyy;

    public Text hh;

    public Text mm;

    public Text strValue;

    public Text agiValue;

    public Text intelValue;

    public Text goldValue;
    public Toggle dateToggle;
    public Toggle timeToggle;
    public Player player;
    public GameObject alert;
    public GameObject informationPanel;
    public GameObject rewardPanel;
    private TodoItem tempTodo;

    // Start is called before the first frame update
    void Start()
    {
        //cancelBtn = cancelBtn.GetComponent<Button>();
        //doneBtn = doneBtn.GetComponent<Button>();
        //nextBtn = nextBtn.GetComponent<Button>();
        cancelBtn.onClick.AddListener(OnCancelBtnClick);
        doneBtn.onClick.AddListener(OnDoneBtnClick);
        nextBtn.onClick.AddListener(OnNextBtnClick);
        //title = title.GetComponent<Text>();
        //description = description.GetComponent<Text>();
        //dd = dd.GetComponent<Text>();
        //MM = MM.GetComponent<Text>();
        //yyyy = yyyy.GetComponent<Text>();
        //hh = hh.GetComponent<Text>();
        //mm = mm.GetComponent<Text>();

        //strValue = strValue.GetComponent<Text>();
        //intelValue = intelValue.GetComponent<Text>();
        //agiValue = agiValue.GetComponent<Text>();
        //goldValue = goldValue.GetComponent<Text>();
        //timeToggle = timeToggle.GetComponent<Toggle>();
        //dateToggle = dateToggle.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCancelBtnClick()
    {
        this.title.text = "";
        this.description.text = "";
        this.dd.text = "";
        this.MM.text = "";
        this.yyyy.text = "";
        this.hh.text = "";
        this.mm.text = "";
        this.gameObject.SetActive(false);
    }
    void OnDoneBtnClick()
    {
        Dictionary<string, int> rewards = new Dictionary<string, int>();
        if (strValue.text != "" && strValue.text != "0") rewards.Add("strength", int.Parse(strValue.text));
        if (intelValue.text != "" && intelValue.text != "0") rewards.Add("intelligence", int.Parse(intelValue.text));
        if (agiValue.text != "" && agiValue.text != "0") rewards.Add("agility", int.Parse(agiValue.text));
        if (goldValue.text != "" && goldValue.text != "0") rewards.Add("gold", int.Parse(goldValue.text));
        tempTodo.bonus = rewards;
        player.todoList.Add(tempTodo);
        player.SummaryTodoList();
    }
    void OnNextBtnClick()
    {
        if (title.text == "")
        {
            alert.SetActive(true);
            alert.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Title can't be empty!";
        } else
        if (dateToggle.isOn)
        {
            if (timeToggle.isOn)
            {
                string y = yyyy.text;
                string M = MM.text;
                string d = dd.text;
                string h = hh.text;
                string m = mm.text;
                if (MM.text.Length == 1) M = "0" + MM.text;
                if (dd.text.Length == 1) d = "0" + dd.text;
                if (hh.text.Length == 1) h = "0" + hh.text;
                if (mm.text.Length == 1) m = "0" + mm.text;
                if (yyyy.text.Length == 1) y = "000" + yyyy.text;
                else if (yyyy.text.Length == 2) y = "00" + yyyy.text;
                else if (yyyy.text.Length == 3) y = "0" + yyyy.text;
                DateTime dt = new DateTime();
                string time = y + M + d + h + m;
                print(time);
                if (DateTime.TryParseExact(time, "yyyyMMddHHmm", null, System.Globalization.DateTimeStyles.None, out dt))
                {
                    tempTodo = new TodoItem(title.text, description.text, dt);
                    tempTodo.Summary();
                    informationPanel.SetActive(false);
                    rewardPanel.SetActive(true);

                }
                else
                {
                    alert.SetActive(true);
                    alert.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Datetime not valid!";
                }
            }
            else
            {
                string y = yyyy.text;
                string M = MM.text;
                string d = dd.text;
                if (MM.text.Length == 1) M = "0" + MM.text;
                if (dd.text.Length == 1) d = "0" + dd.text;
                if (yyyy.text.Length == 1) y = "000" + yyyy.text;
                else if (yyyy.text.Length == 2) y = "00" + yyyy.text;
                else if (yyyy.text.Length == 3) y = "0" + yyyy.text;
                DateTime dt = new DateTime();
                string time = y + M + d + "2359";
                if (DateTime.TryParseExact(time, "yyyyMMddHHmm", null, System.Globalization.DateTimeStyles.None, out dt))
                {
                    tempTodo = new TodoItem(title.text, description.text, dt);
                    informationPanel.SetActive(false);
                    rewardPanel.SetActive(true);
                }
                else
                {
                    alert.SetActive(true);
                    alert.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Datetime not valid!";
                }
            }
        }
        else
        {
            tempTodo = new TodoItem(title.text, description.text);
            informationPanel.SetActive(false);
            rewardPanel.SetActive(true);
        }
    }
}
