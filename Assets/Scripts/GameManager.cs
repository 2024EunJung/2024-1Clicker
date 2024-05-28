using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
<<<<<<< HEAD
using UnityEditor.Experimental.GraphView;
=======
using UnityEngine.SceneManagement;
>>>>>>> Develop

public class GameManager : MonoBehaviour
{
    public long money;
    public long moneyIncreaseAmount;
    public Text textMoney;

    public GameObject prefabMoney;

    public long moneyIncreaseLevel; //클릭 당 단가 업그레이드 레벨
    public long moneyIncreasePrice; //업그레이드 가격
    public Text textPrice; //표시할 텍스트

    public Button buttonPrice; //단가 업그레이드 버튼

    public int employeeCount; //직원 수 (레벨)
    public Text textRecruit; //직원 고용 패널의 텍스트

    public Button buttonRecruit; //직원 고용 패널 버튼

    public int width; //가로 최대 직원 수
    public float space; //직원 간격
    public GameObject prefabEmployee; //직원프리팹

    public Text textPerson;

    public float spaceFloor; //바닥의 간격
    public int floorCapacity; //바닥이 수용 가능한 인원수
    public int currentFloor; //현재 바닥의 수
    public GameObject prefabFloor; //바닥 프리팹

<<<<<<< HEAD
    public Text resultText;

    public Animator Background;
    public float backtime;

=======
    public int BuildingLevel;
    public long Capacity;
    public long BuildingPrice;
    public Text textBuilding;

    public Button buttonBuilding;
>>>>>>> Develop

    // Start is called before the first frame update
    void Start()
    {

        string path = Application.persistentDataPath + "/save.xml";
        if (System.IO.File.Exists(path))
        {
            Load();
            FillEmployee();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoneyIncrease();
        ShowInfo();
        UpdatePanelText();
        ButtonActiveCheck();
        UpdateRecruitPanelText();
<<<<<<< HEAD
        ButtonRecuritActiveCheck();
        CreatFloor();
        Vack();
=======
        ButtonRecruitActiveCheck();
        UpdateBuildingPanelText();
        ButtonBuildingActiveCheck();
        CreateFloor();
>>>>>>> Develop
    }

    void MoneyIncrease()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                money += moneyIncreaseAmount;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(prefabMoney, mousePosition, Quaternion.identity);
            }
        }
    }

    void ShowInfo()
    {
        if (money == 0)
        {
            textMoney.text = "0원";
        }
        else
        {
            textMoney.text = money.ToString("###,###") + "원";
        }
        if (employeeCount == 0)
        {
            textPerson.text = "0명";
        }
        else
        {
            textPerson.text = employeeCount + "명";
        }
    }

    void UpdatePanelText()
    {
        textPrice.text = "Lv." + moneyIncreaseLevel + "단가 상승\n\n";
        textPrice.text += "외주 당 단가>\n";
        textPrice.text += moneyIncreaseAmount.ToString("###,###") + " 원\n";
        textPrice.text += "업그레이드 가격>\n";
        textPrice.text += moneyIncreasePrice.ToString("###,###") + " 원";
    }

    public void UpgradePrice()
    {
        if(money >= moneyIncreasePrice)
        {
            money -= moneyIncreasePrice;
            moneyIncreaseLevel += 1;
            moneyIncreaseAmount += moneyIncreaseLevel * 100;
            moneyIncreasePrice += moneyIncreaseLevel * 500;
        }
    }

    void ButtonActiveCheck()
    {
        if(money >= moneyIncreasePrice)
        {
            buttonPrice.interactable = true;
        }
        else
        {
            buttonPrice.interactable = false;
        }
    }

    void UpdateRecruitPanelText()
    {
        textRecruit.text = "Lv." + employeeCount + "단가 상승\n\n";
        textRecruit.text += "직원 1초 당 단가>\n";
        textRecruit.text += AutoWork.autoMoneyIncreaseAmount.ToString("###,###") + " 원\n";
        textRecruit.text += "업그레이드 가격>\n";
        textRecruit.text += AutoWork.autoIncreasePrice.ToString("###,###") + " 원\n";
        textRecruit.text += "고용 가능 직원>\n";
        textRecruit.text += Capacity.ToString() + " 명";
    }

    void ButtonRecruitActiveCheck()
    {
        if (money >= AutoWork.autoIncreasePrice && Capacity > employeeCount)
        {
            buttonRecruit.interactable = true;
        }
        else
        {
            buttonRecruit.interactable = false;
        }
    }

    void UpdateBuildingPanelText()
    {
        textBuilding.text = "Lv." + BuildingLevel + "건물 업그레이드\n\n";
        textBuilding.text += "건물 수용 인원>\n";
        textBuilding.text += Capacity.ToString() + " 명\n";
        textBuilding.text += "업그레이드 가격>\n";
        textBuilding.text += BuildingPrice.ToString("###,###") + " 원";
    }

    void ButtonBuildingActiveCheck()
    {
        if (money >= BuildingPrice)
        {
            buttonBuilding.interactable = true;
        }
        else
        {
            buttonBuilding.interactable = false;
        }
    }

    void CreateEmployee()
    {
        if (Capacity > employeeCount)
        {
            Vector2 bossSpot = GameObject.Find("Boss").transform.position;
            float spotX = bossSpot.x + (employeeCount % width) * space;
            float spotY = bossSpot.y - (employeeCount / width) * space;

            Instantiate(prefabEmployee, new Vector2(spotX, spotY), Quaternion.identity);
        }
    }

    public void Recruit()
    {
        if(money >= AutoWork.autoIncreasePrice)
        {
            money -= AutoWork.autoIncreasePrice;
            employeeCount += 1;
            AutoWork.autoMoneyIncreaseAmount += moneyIncreaseLevel * 10;
            AutoWork.autoIncreasePrice += employeeCount * 500;

            CreateEmployee();
        }
    }

    public void UpgradeBuilding()
    {
        if (money >= BuildingPrice)
        {
            money -= BuildingPrice;
            BuildingLevel += 1;
            Capacity += 48;
            BuildingPrice += BuildingPrice * 500;
        }
    }

    void CreateFloor()
    {
        Vector2 bgPosition = GameObject.Find("Background").transform.position;

        float nextFloor = (employeeCount + 1) / floorCapacity;

        float spotX = bgPosition.x;
        float spotY = bgPosition.y;

        spotY -= spaceFloor * nextFloor;

        if (nextFloor >= currentFloor)
        {
            Instantiate(prefabFloor, new Vector2(spotX, spotY), Quaternion.identity);
            currentFloor += 1;
            Camera.main.GetComponent<CameraDrag>().limitMinY -= spaceFloor;
        }
    }

<<<<<<< HEAD
    void Vack()
    {
        backtime += Time.deltaTime;

        if (backtime > 0 && backtime <100) {
        Background.SetBool("isday", true);
        }

        if(backtime > 100 && backtime < 200)
        {
            Background.SetBool("isday", false);
        }
        else if(backtime > 200)
        {
            backtime = 0;
        }
=======
    public void GameReSet()
    {
        moneyIncreaseAmount = 10;
        moneyIncreaseLevel = 1;
        moneyIncreasePrice = 1000;

        employeeCount = 0;

        BuildingLevel = 1;
        Capacity = 0;
        BuildingPrice = 10000;

        SceneManager.LoadScene(0);
>>>>>>> Develop
    }

    void Save()
    {
        SaveData saveData = new SaveData();

        saveData.money = money;
        saveData.moneyIncreaseAmount = moneyIncreaseAmount;
        saveData.moneyIncreaseLevel = moneyIncreaseLevel;
        saveData.moneyIncreasePrice = moneyIncreasePrice;
        saveData.employeeCount = employeeCount;
        saveData.autoMoneyIncreaseAmount = AutoWork.autoMoneyIncreaseAmount;
        saveData.autoIncreasePrice = AutoWork.autoIncreasePrice;
<<<<<<< HEAD
        saveData.backtime = backtime;
=======
        saveData.BuildingLevel = BuildingLevel;
        saveData.Capacity = Capacity;
        saveData.BuildingPrice = BuildingPrice;
>>>>>>> Develop

        string path = Application.persistentDataPath + "/save.xml";
        XmlManager.XmlSave<SaveData>(saveData, path);
    }
    void Load()
    {
        SaveData saveData = new SaveData();

        string path = Application.persistentDataPath + "/save.xml";
        saveData = XmlManager.XmlLoad<SaveData>(path);

        money = saveData.money;
        moneyIncreaseAmount = saveData.moneyIncreaseAmount;
        moneyIncreaseLevel = saveData.moneyIncreaseLevel;
        moneyIncreasePrice = saveData.moneyIncreasePrice;
        employeeCount = saveData.employeeCount;
        AutoWork.autoMoneyIncreaseAmount = saveData.autoMoneyIncreaseAmount;
        AutoWork.autoIncreasePrice = saveData.autoIncreasePrice;
<<<<<<< HEAD
        backtime = saveData.backtime;
=======
        BuildingLevel = saveData.BuildingLevel;
        Capacity = saveData.Capacity;
        BuildingPrice = saveData.BuildingPrice;
>>>>>>> Develop
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    void FillEmployee()
    {
        GameObject[] employees = GameObject.FindGameObjectsWithTag("Employee");

        if(employeeCount != employees.Length)
        {
            for(int i = employees.Length + 1; i <= employeeCount; i++)
            {
                Vector2 bossSpot = GameObject.Find("Boss").transform.position;
                float spotX = bossSpot.x + (i % width) * space;
                float spotY = bossSpot.y - (i/width) * space;

                GameObject obj = Instantiate(prefabEmployee, new Vector2(spotX, spotY), Quaternion.identity);
            }
        }
    }
    public void OnClickButton()
    {
        // 0부터 1 사이의 랜덤한 값을 생성합니다.
        float randomValue = Random.value;

        // 50%의 확률로 성공 또는 실패를 결정합니다.
        if (randomValue < 0.5f)
        {
            resultText.text = "성공!";
            money = money * 2;

        }
        else
        {
            resultText.text = "실패!";
            money = 0;
        }
        Invoke("ResetText", 1f);
    }
    void ResetText()
    {
        resultText.text = "돈복사 버튼\r\n성공확률 50%";
    }
}
