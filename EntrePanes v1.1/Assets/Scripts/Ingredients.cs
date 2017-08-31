using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Ingredients : MonoBehaviour {

    // Use this for initialization
    #region Variables
    List<GameObject> ingredientes = new List<GameObject>();

    int cantIngr, contador;
    [HideInInspector]
    public UnityEngine.UI.Image img;
    public static bool sigueJugando=true;//Si sigue jugando o frena todo el juego
    public int frecuenciaDeCaida = 200;
    public float velocidadDeCaida = 0.5f;
    #endregion
    void Start () {

        #region Seteo de Fondo
        Sprite[] fondos = Resources.LoadAll<Sprite>("sprites");
        int fondoR = UnityEngine.Random.Range(0, fondos.Length);
        img.sprite = fondos[fondoR];
        #endregion

        contador = frecuenciaDeCaida / 2;                                           // El contador comienza a la mitad de la frecuencia para que no empiece
                                                                                    // Inmediatamente el juego, ni tenga que esperar demasiado.
        #region Lista de Ingredientes
        ingredientes = listarIngredientes(true);
        #endregion

    }
	
	// Update is called once per frame
	void Update () {
        if (sigueJugando)
        {
            if (contador >= frecuenciaDeCaida){     // Si paso X tiempo lanzo un nuevo ingrediente
                lanzarIngrediente(ingredientes);    // Llamo a la funcion encargada de Lanzar el Ingrediente...
                contador = 0;                       // Reestablezco el contador
            }
            else
                contador++;                         // Sino, paso el tiempo, incremento el contador
        }
        else
        {
            //Elimino todos los GameObjects que no sean Escenario ni parte de la Hamburguesa
            UnityEngine.GameObject[] todos = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach(GameObject descartar in todos)
            {
                if (descartar.name!="Pan"&&descartar.tag!="SeQueda"&&descartar.tag!="Hamburguesa"&&descartar.tag!="Ingrediente")
                    Destroy(descartar);
            }
        }
    }

    #region Funciones
    void lanzarIngrediente(List<GameObject> Ing)
    {
        #region SubVariables
        GameObject clon;
        int ingRan = UnityEngine.Random.Range(0, 12),    // ingRan = Elegir un ingrediente Random
            xRan = UnityEngine.Random.Range(-10, 10);   // xRan = Posicion random para el Ingrediente |ACA TENEMOS QUE METER LA TOLERANCIA|
        Vector2 pos = new Vector2(xRan, 12f);            // Vector de posicion de aparición del Ingrediente
        #endregion
        #region Balanceo de Random
        if (ingRan >= 8 && ingRan <= 20)                 // Aumento las probabilidades de que salgan panes superiores
            ingRan = 4;
        #endregion
        #region Llamada y Seteo de Clones
        clon = Instantiate(ingredientes[ingRan], pos, Quaternion.identity) as GameObject;// Instancio el ingrediente como Clon, ACA CAMBIAR EL 0 POR INGRAN
        clon.AddComponent<Rigidbody2D>();                                                // y como Rigidbody para que tenga gravedad
        clon.GetComponent<Rigidbody2D>().drag = velocidadDeCaida;                        // Establezco la gravedad en base a la Velocidad Elegida
        clon.GetComponent<Rigidbody2D>().gravityScale = 1;                               // Activo la gravedad en si
        #endregion
    }
    public static List<GameObject> listarIngredientes(bool PanSuperior) //Si tengo en cuenta o No el pan superior
    {
        List<GameObject> ings = new List<GameObject>();
        int cant = GameObject.FindGameObjectsWithTag("Ingrediente").Length; // Cuento la cantidad de Ingredientes
        if(PanSuperior)
        {
            for (int i = 0; i < cant; i++)
                ings.Add(GameObject.FindGameObjectsWithTag("Ingrediente")[i]);  // Agrego a una List cada Ingrediente
            
        }
        else
        {
            for (int i = 0; i < cant; i++)
            {
                if(GameObject.FindGameObjectsWithTag("Ingrediente")[i].name!="PanSuperior")
                    ings.Add(GameObject.FindGameObjectsWithTag("Ingrediente")[i]);  // Agrego a una List cada Ingrediente
            }
        }
        return ings;
    }
    #endregion
}
