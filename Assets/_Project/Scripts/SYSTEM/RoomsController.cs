using UnityEngine;

public class RoomsController : MonoBehaviour
{
    [SerializeField, Header("Вставьте стартовую комнату")]
    private GameObject startRoom;


    public static RoomsController Instance;

    //Флажки
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
            Debug.Log("Это не первый запуск");
        }
    }
}
