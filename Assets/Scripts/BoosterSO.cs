using UnityEngine;

[CreateAssetMenu(menuName = "Create Booster", fileName = "NewBooster", order = 51)]
public class BoosterSO : ScriptableObject
{
    [SerializeField] private string _label;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _value;
    [SerializeField] private int _cost;

    public string Label => _label;
    public string Description => _description;
    public Sprite Icon => _icon;
    public float Value => _value;
    public int Cost => _cost;
}
