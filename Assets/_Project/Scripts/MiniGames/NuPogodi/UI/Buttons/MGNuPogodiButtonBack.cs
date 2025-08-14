using UnityEngine;

public class MGNuPogodiButtonBack : MonoBehaviour
{
    [SerializeField, Header("�������� �������, ������� ����� ������������ ��� �������")]
    private GameObject[] activeObj;

    [SerializeField, Header("�������� �������, ������� ����� �������������� ��� �������")]
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
