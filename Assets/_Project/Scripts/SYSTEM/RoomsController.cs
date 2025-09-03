using UnityEngine;

public class RoomsController : MonoBehaviour
{
    [SerializeField, Header("�������� ��������� �������")]
    private GameObject startRoom;


    public static RoomsController Instance;

    //������
    private bool isFirstStart = true;


    //��� ������
    [SerializeField, Header("�������� �����")]
    private bool isTest = false;
    [SerializeField, Header("����� ���������� � �����")]
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
            Debug.Log("��� �� ������ ������");
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
