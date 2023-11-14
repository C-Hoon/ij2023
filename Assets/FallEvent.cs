using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallEvent : MonoBehaviour
{
    public Transform installer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>();
            playerMove.ChangeHealth(PlayerMove.ChangeHealthType.DOWN);
            playerMove.transform.position = installer.position;
        }
    }
}
