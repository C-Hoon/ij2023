using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    GameObject ui;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ui = GameCore.Managers.Resource.Instantiate("UI/WorldSpace/InteractionButtonUI");
            Vector3 position = transform.position;
            position.y += 2f;
            ui.transform.position = position;
            ui.transform.parent = transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject);
            GameCore.Managers.Resource.Destroy(ui);
        }
    }
}
