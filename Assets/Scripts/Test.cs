using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test2 : MonoBehaviour
{
    public EnemyStage2 enemyStage2;
    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            enemyStage2.TakeDamage(20);
        }
    }
}
