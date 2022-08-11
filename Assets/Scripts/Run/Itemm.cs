using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemm : MonoBehaviour
{
    public float speed = 50.0f;
    Rigidbody2D rigid = null;
   


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rigid.velocity = (-transform.right * speed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }


}
