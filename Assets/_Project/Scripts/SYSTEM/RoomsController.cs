using UnityEngine;

public class RoomsController : MonoBehaviour
{
    [SerializeField, Header("Вставьте стартовую комнату")]
    private GameObject startRoom;


    //Флажки
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
            Debug.Log("Это не первый запуск");
        }
    }
}
