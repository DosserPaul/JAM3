using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Card_Id", menuName="Devcompagnie/Jam3/CardId", order = 1)]
public class CardId : ScriptableObject
{
    public Sprite Image;
    public FamilyId family;
    public string Name;
    public Color color;
}
