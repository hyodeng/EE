using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFactory
{
    static int skillCount = 0;
    public static GameObject SkillCast(CharacterType code)
    {
        GameObject obj = new GameObject();
        Skill skill = obj.AddComponent<Skill>();

        //skill.data = GameManager.Inst.SkillData[code];
        string[] skillName = skill.data.skillName.Split("_");
        obj.name = $"{skillName[1]}_{skillCount}";
        obj.layer = LayerMask.NameToLayer("Skill");
        SphereCollider col = obj.AddComponent<SphereCollider>();
        col.radius = 0.5f;
        col.isTrigger = true;
        skillCount++;


        return obj;
    }
}
