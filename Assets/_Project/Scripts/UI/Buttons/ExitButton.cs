using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void Exit()
    {
        SFX_Main.Instance.PlayAudio("Select"); //���� ��� ������� �� ��� �������
        Application.Quit();
    }
}
