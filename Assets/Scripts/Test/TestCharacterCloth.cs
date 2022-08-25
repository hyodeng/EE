using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacterCloth : MonoBehaviour
{

    Customized custom;

    public void TestSetCloth()
    {

        custom = FindObjectOfType<Customized>();

        custom.SetParts(0, "Eye1.png");
        custom.SetParts(1, "Hair_1.png");
        custom.SetParts(3, "Armor_8.png");
        custom.SetParts(4, "Foot_5.png");
        custom.SetParts(5, "Sword_1.png");
        custom.SetParts(6, "Shield_1.png");
    }

}
