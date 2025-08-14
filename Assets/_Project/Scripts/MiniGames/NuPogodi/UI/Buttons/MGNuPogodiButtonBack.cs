using UnityEngine;

public class MGNuPogodiButtonBack : MonoBehaviour
{
    [SerializeField, Header("Вставить объекты, которые нужно активировать при нажатии")]
    private GameObject[] activeObj;

    [SerializeField, Header("Вставить объекты, которые нужно ДЕактивировать при нажатии")]
    private GameObject[] deactiveObj;

    public void Back()
    {
        foreach (GameObject obj in activeObj)
        {
            obj?.SetActive(true);
        }
        foreach (GameObject obj in deactiveObj)
        {
            obj?.SetActive(false);
        }
    }
}
