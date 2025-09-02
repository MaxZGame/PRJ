using UnityEngine;

public class NextRoom : MonoBehaviour
{
    [SerializeField, Header("�������� �������, ������� ����� ������������")]
    private GameObject[] activatedGO;

    [SerializeField, Header("�������� �������, ������� ����� ��������������")]
    private GameObject[] deactivatedGO;

    private void OnMouseDown()
    {
        if (gameObject.tag == "NextRoom")
        {
            Activated();
            Deactivated();
        }
    }

    public void Deactivated()
    {
        if (deactivatedGO != null & deactivatedGO.Length > 0)
        {
            foreach (GameObject go in deactivatedGO)
            {
                go?.SetActive(false);
            }
        }
        Inventory.Instance.isNextRoomStep = true;
        Inventory.Instance.OnDisableInventory();
        Inventory.Instance.isNextRoomStep = false;
    }

    public void Activated()
    {
        SFX_Main.Instance.PlayAudio("Select"); //���� ��� ������� �� ��� �������
        if (activatedGO != null && activatedGO.Length > 0)
        {
            foreach (GameObject go in activatedGO)
            {
                go?.SetActive(true);
            }
        }
    }
}
