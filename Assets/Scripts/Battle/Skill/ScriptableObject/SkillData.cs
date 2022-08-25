using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Skill Data", menuName = "Scriptable Object/ Skill Data", order = 1)]
public class SkillData : ScriptableObject
{
    public uint id = 0;
    public string skillName = "½ºÅ³";
    public GameObject prefab;
    public uint cost = 0;
    public uint influenceRate = 0;
}
