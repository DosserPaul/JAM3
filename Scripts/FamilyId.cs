using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Family_Id", menuName = "Devcompagnie/Jam3/FamilyId", order = 0)]
public class FamilyId : ScriptableObject
{
    public string familyName;
    public CardId[] members;
}
