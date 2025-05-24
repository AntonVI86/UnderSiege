using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private MeshRenderer targetObject; // ������ � Renderer
    [SerializeField] private Image uiImage; // UI Image ��� ��������� �����



    void Update()
    {
        // ���������, ���� �� ������ ������� Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("Click");
            // �������� ���� ���������
            Color materialColor = targetObject.sharedMaterials[0].color;

            // ������ ���� � UI Image
            uiImage.color = materialColor;
        }
    }
}