using System.Collections.Generic;
using UnityEngine;

public class RoomsController : MonoBehaviour
{
    [SerializeField, Header("�������� ��������� �������")]
    private GameObject startRoom;

    private Room[] rooms;

    public static RoomsController Instance;

    private string currentRoomId; // id �������� �������

    //������
    private bool isFirstStart = true;


    //��� ������
    [SerializeField, Header("�������� �����")]
    private bool isTest = false;
    [SerializeField, Header("����� ���������� � �����")]
    private GameObject testText;

    private string keyFirstStart = "������ ������"; //���� ���������� ������� ������ ������ ��� ���

    private void Awake()
    {
        Instance = this;
        rooms = gameObject.GetComponentsInChildren<Room>();
    }

    public bool IsFirstStart()
    {
        if (PlayerPrefs.HasKey(keyFirstStart) && !isTest)
        {
            int imitBool = PlayerPrefs.GetInt(keyFirstStart);
            if (imitBool == 0)
            {
                isFirstStart = true;
            }
            if (imitBool == 1)
            {
                isFirstStart = false;
            }
        }
        return isFirstStart;
    }

    public void RoomCurrentUpdate()
    {
        foreach (Room room in rooms)
        {
            if (room.gameObject.activeSelf)
            {
                currentRoomId = room.Id;
                Debug.Log($"{currentRoomId}");
            }
        }
    }

    public void FirstStart()
    {
        if (PlayerPrefs.HasKey(keyFirstStart) && !isTest)
        {
            int imitBool = PlayerPrefs.GetInt(keyFirstStart);
            if (imitBool == 0)
            {
                isFirstStart = true;
                Debug.Log($"{isFirstStart}");
            }
            if (imitBool == 1)
            {
                isFirstStart = false;
                Debug.Log($"{isFirstStart}");
            }
        }
        else
        {
            isFirstStart = true;
            Debug.Log($"{isFirstStart}");
        }

        if (isFirstStart && !isTest)
        {
            if (testText.activeSelf)
            {
                testText.SetActive(false);
            }
            if (!startRoom.activeSelf)
            {
                startRoom.SetActive(true);
            }
            PlayerPrefs.SetInt(keyFirstStart, 1);
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
            foreach (Room room in rooms)
            {
                if (currentRoomId == room.Id)
                {
                    if (!room.gameObject.activeSelf)
                    {
                        room.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (room.gameObject.activeSelf)
                    {
                        room.gameObject.SetActive(false);
                    }
                }
            }
        }
        RoomCurrentUpdate();
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
