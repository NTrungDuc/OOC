using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingPlayer : MonoBehaviour
{
    public Transform target;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Update()
    {
        transform.position = target.position;
    }
    void SortingOrder(Transform obstacle)
    {
        if (transform.position.y > obstacle.position.y)
        {
            spriteRenderer.sortingOrder = -1;
        }
        else
        {
            spriteRenderer.sortingOrder = 1;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckSorting"))
        {
            SortingOrder(collision.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.sortingOrder = 0;
    }
}
