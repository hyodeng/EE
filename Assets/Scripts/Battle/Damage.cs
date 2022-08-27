using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage : MonoBehaviour
{
    TextMeshPro text;
    private void Start()
    {
        text = GetComponent<TextMeshPro>();
    }
    void Update()
    {
        text.alpha -= Time.deltaTime * 2f;
        if(text.alpha == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
