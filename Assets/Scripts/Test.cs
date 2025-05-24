using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private MeshRenderer targetObject; // Объект с Renderer
    [SerializeField] private Image uiImage; // UI Image для изменения цвета



    void Update()
    {
        // Проверяем, была ли нажата клавиша Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("Click");
            // Получаем цвет материала
            Color materialColor = targetObject.sharedMaterials[0].color;

            // Меняем цвет у UI Image
            uiImage.color = materialColor;
        }
    }
}