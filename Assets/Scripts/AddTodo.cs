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

    public InputField title;

    public InputField description;

    public InputField dd;

    public InputField MM;

    public InputField yyyy;

    public InputField hh;

    public InputField mm;

    public InputField strValue;

    public InputField agiValue;

    public InputField intelValue;

    public InputField goldValue;
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
        cancelBtn.onClick.AddListener(OnCancelBtnClick);
        doneBtn.onClick.AddListener(OnDoneBtnClick);
        nextBtn.onClick.AddListener(OnNextBtnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCancelBtnClick()
    {
        this.title.Select();
        this.title.text = "";
        this.description.Select();
        this.description.text = "";
        this.dd.Select();
        this.dd.text = "";
        this.MM.Select();
        this.MM.text = "";
        this.yyyy.Select();
        this.yyyy.text = "";
        this.hh.Select();
        this.hh.text = "";
        this.mm.Select();
        this.mm.text = "";
        this.goldValue.Select();
        this.goldValue.text = "";
        this.strValue.Select();
        this.strValue.text = "";
        this.intelValue.Select();
        this.intelValue.text = "";
        this.agiValue.Select();
        this.agiValue.text = "";
        this.dateToggle.Select();
        this.dateToggle.isOn = false;
        this.timeToggle.Select();
        this.timeToggle.isOn = false;
        this.tempTodo = null;
        informationPanel.SetActive(true);
        rewardPanel.SetActive(false);
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
        player.todoList.Insert(0,tempTodo);
        player.SummaryTodoList();
        OnCancelBtnClick();
    }
    void OnNextBtnClick()
    {
        if (title.text == "")
        {
            alert.SetActive(true);
            alert.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Title can't be empty!";
        }
        else
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
