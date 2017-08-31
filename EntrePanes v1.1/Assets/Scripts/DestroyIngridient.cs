using UnityEngine;
using System.Collections;

public class DestroyIngridient : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name!="Pan")
            Destroy(collision.gameObject);        
    }
}
