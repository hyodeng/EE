using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test3 : MonoBehaviour
{
    public EnemyStage3 enemyStage3;
    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            enemyStage3.TakeDamage(20);
        }
    }
}
