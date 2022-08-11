using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRun : MonoBehaviour
{
    
    public float jump;
    public GameObject gameover = null;


 

    void Update()
    {
        Jump();
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            Rigidbody2D rigid = transform.GetComponent<Rigidbody2D>();


                rigid.AddForce(Vector3.up * jump);

            

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            Destroy(this.gameObject);

            gameover.SetActive(true);
        }
    }
}
       
