using UnityEngine;
using System.Collections;

public class DeteccionIngredientes : MonoBehaviour
{

    #region Varaibles
    GameObject detector;
    float repos = 0.5f;
    #endregion
    // Use this for initialization
	void Start () {
        detector = this.gameObject;
	}
	
    void OnTriggerEnter2D(Collider2D collision)
    {
        #region Deteccion de Ingredientes
        GameObject ing = collision.gameObject;
        Rigidbody2D panRb = GameObject.Find("Pan").GetComponent<Rigidbody2D>();
        if (ing.tag == "Ingrediente"&&ing.transform.rotation.z==0)  //Si es un ingrediente y esta recto, es decir que no entra de costado
        {
            PanActions.PosicionPan(ing);                                                                        // Llamo a la Funcion de Reposicionamiento del Pan
            
            ing.GetComponent<Rigidbody2D>().gravityScale = 0;                                                   // Anulo la gravedad que usaba el Ingrediente
            ing.AddComponent<FixedJoint2D>();                                                                   // Le agrego un FixedJoint para que quede "agarrado" al pan
            ing.GetComponent<FixedJoint2D>().connectedBody = panRb;                                             // Establezco el pan como el lugar a donde agarrarse            
            ing.GetComponent<FixedJoint2D>().connectedAnchor = new Vector2(0, repos);
            ing.tag = "Hamburguesa";                                                                            // Indico que es parte de la Hamburguesa
            detector.transform.position = new Vector2(detector.transform.position.x, panRb.transform.position.y + repos+0.1f);// Reposiciono el Box Collider para arriba
            repos += 0.5f;
        }
        if (ing.name == "PanSuperior(Clone)")
        {
            Pedidos.HamburguesaTerminada(ing);
        }
        #endregion
    }
}
