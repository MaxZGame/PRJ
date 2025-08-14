using UnityEngine;

public class GameManager : MonoBehaviour
{
    private RoomsController roomsController;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        roomsController.FirstStart();
    }

    private void Init()
    {
        roomsController = GetComponent<RoomsController>();
    }
}
