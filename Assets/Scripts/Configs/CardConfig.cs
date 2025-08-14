using UnityEngine;

[CreateAssetMenu(fileName = "CardConfig", menuName = "Config/CardConfig")]
public class CardConfig : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public Question Question;
}