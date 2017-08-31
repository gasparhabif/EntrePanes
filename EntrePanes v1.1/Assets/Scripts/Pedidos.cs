using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pedidos : MonoBehaviour {
    #region Variables
    [Header("Dificultad de la Hamburguesa")]
    [Range (0,30)]
    public int dif;
    public static Dictionary<string, int> pedido = new Dictionary<string, int>();         // Dictionary es igual al Map de Java
    static Dictionary<string, int> realizado = new Dictionary<string, int>();
    public static List<GameObject> ingredientes = new List<GameObject>();
    #endregion
    #region Orden de Ingredientes
    /*
     * 0=Queso
     * 1=Cebolla
     * 2=Tomate
     * 3=Lechuga
     * 4=Huevo
     * 5=Panceta
     * 6=Paty
     */
    #endregion
    // Use this for initialization
    void Start () {
        ingredientes = Ingredients.listarIngredientes(false);               // Genero la Lista de posibles ingredientes, sin el Pan Superior
        #region Generacion del Pedido
            for (int i = 0; i < dif; i++)
            {
                int ingRan = UnityEngine.Random.Range(0, ingredientes.Count);   // Random entre todos los ingredientes
                string ingNom = ingredientes[ingRan].name;                      // Nombre del Ingrediente
                pedido = addToList(pedido, ingNom);
            }
        #endregion
    }

    // Update is called once per frame
    void Update () {
	
	}
    #region Funciones
    public static void HamburguesaTerminada(GameObject aChequear)
    {
        if (aChequear.name == "PanSuperior(Clone)")
        {
            Ingredients.sigueJugando = false;
        }
        generarListaHamburguesa();
    }
        #region Comprobar Hamburguesa
    public static void generarListaHamburguesa()
    {
        UnityEngine.GameObject[] todos = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject hamb in todos)
        {
            if (hamb.tag == "Hamburguesa"&&hamb.name!="PanSuperior(Clone)")
            {
                string hambName = deleteClone(hamb.name);           //Porque es ing(clone)
                realizado = addToList(realizado, hambName);
            }                
        }
        WinGame.correcto=checkHamburguesa(pedido,realizado);
        Menu.WinS();
    }
    private static int checkHamburguesa(Dictionary<string, int> mapA,Dictionary<string, int> mapB)
    {
        int contador=10;
        if (ingredientes.Count > 0)
        {
            for (int a = 0; a < ingredientes.Count; a++)
            {
                string nombre = ingredientes[a].name;
                if (mapA.ContainsKey(nombre))
                {
                    if (mapB.ContainsKey(nombre))
                    {
                        if (mapA[nombre] != mapB[nombre])
                            if(contador>1)
                                contador--;
                    }
                    else
                        if (contador > 1)
                            contador--;
                }
                if(mapB.Count<1)
                    contador = 1;
            }
        }
            
        return contador;
    }
        #endregion

        #region Agregar a Lista
    private static Dictionary<string, int> addToList(Dictionary<string, int> map, string key)
    {
        if (map.ContainsKey(key))                                 // Si existe en el Map
        {
            int cantExs = map[key];                               // Averiguo cuanto habia antes
            map.Remove(key);                                      // Lo remuevo
            map.Add(key, (cantExs + 1));                          // Lo vuelvo a agregar con el valor de antes +1
        }
        else
            map.Add(key, 1);
        return map;
    }
    #endregion
        #region Quitar (Clone) del String
        public static string deleteClone(string palabra)
        {
            int desde = palabra.IndexOf('(');
            return palabra.Remove(desde);
        }
        #endregion

    #endregion
}
