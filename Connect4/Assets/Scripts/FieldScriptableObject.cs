using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FieldScriptableobject", menuName = "ScriptableObjects/FieldScriptableObject", order = 1)]
public class FieldScriptableObject : ScriptableObject
{
    public GameObject field;
    public int rowAmount;
    public int colAmount;
    public int amountToWin;
}
