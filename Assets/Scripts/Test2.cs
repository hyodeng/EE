using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    public EnemyStage1 enemyStage1;
    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            enemyStage1.TakeDamage(20);
        }
    }
}
