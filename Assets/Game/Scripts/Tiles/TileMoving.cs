using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMoving : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyTile"))
        {
            Destroy(gameObject);
        }
    }
}
