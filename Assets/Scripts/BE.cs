using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE : MonoBehaviour
{
    private void OnMouseEnter()  //마우스가 오브젝트 위에 있을 때
    {
        transform.localScale = new Vector3(1.1f, 1.2f, 1.2f);
    }

    private void OnMouseExit()
    {
        transform.localScale = new Vector3(0.9f, 1, 1);
    }
}
