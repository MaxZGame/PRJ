using UnityEngine;

public class RoomsController : MonoBehaviour
{
    [SerializeField, Header("Вставьте стартовую комнату")]
    private GameObject startRoom;


    public static RoomsController Instance;

    //Флажки
    private bool isFirstStart = true;


    //ДЛЯ ТЕСТОВ
    [SerializeField, Header("Тестовый режим")]
    private bool isTest = false;
    [SerializeField, Header("Текст оповещения о тесте")]
    private GameObject testText;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsFirstStart()
    {
        return isFirstStart;
    }

    public void FirstStart()
    {
        if (isFirstStart && !isTest)
        {
            if (testText.activeSelf)
            {
                testText.SetActive(false);
            }
            startRoom.SetActive(true);
            isFirstStart = false;
        }
        else
        {
            if (isTest)
            {
                isFirstStart = false;
                if (!testText.activeSelf)
                {
                    testText.SetActive(true);
                }
            }
            Debug.Log("Это не первый запуск");
        }
    }

    private void OnValidate()
    {
        if (!isTest)
        {
            testText.gameObject.SetActive(false);
        }
        if (isTest)
        {
            testText.gameObject.SetActive(true);
        }
    }
}
