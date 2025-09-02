using UnityEngine;

public class RoomsController : MonoBehaviour
{
    [SerializeField, Header("�������� ��������� �������")]
    private GameObject startRoom;


    public static RoomsController Instance;

    //������
    private bool isFirstStart = true;

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
        if (isFirstStart)
        {
            startRoom.SetActive(true);
            isFirstStart = false;
        }
        else
        {
            Debug.Log("��� �� ������ ������");
        }
    }
}
