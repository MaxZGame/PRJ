using UnityEngine;

public class RoomsController : MonoBehaviour
{
    [SerializeField, Header("�������� ��������� �������")]
    private GameObject startRoom;


    //������
    private bool isFirstStart = true;

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
