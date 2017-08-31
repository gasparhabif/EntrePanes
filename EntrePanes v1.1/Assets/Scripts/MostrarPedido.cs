using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MostrarPedido : MonoBehaviour {
    #region Variables
    public UnityEngine.UI.Image fondo;
    GameObject can,canup;      // Encuentro el Canvas por Codigo

    [Header("Configuracion del Juego")]
    [Range(50f, 500f)]
    public int frecuenciaDeCaidaDeLosIngredientes = 200;    // Diu el nombre x2
    [Range(1f, 10f)]
    public float velocidadDeCaidaDeLosIngredientes = 0.5f;
    bool empezo = true;
    GameObject pan;
    #endregion
    // Use this for initialization
    void Start() {
        can = GameObject.Find("MostrarPedido");
        canup = GameObject.Find("PedidoUp");
        pan = GameObject.Find("Pan");
        canup.SetActive(false);
        pan.SetActive(false);
        Ingredients.sigueJugando = false;
    }

    // Update is called once per frame
    void Update() {
        if (empezo)
        {
            addText();
            addClone();
            empezo = false;
        }
    }
    public void Avanzar()
    {
        canup.SetActive(true);
        addTextUP();
        addCloneUP();
        pan.SetActive(true);
        addScript();
        Ingredients.sigueJugando = true;
    }
    public void Volver(int back)
    {
        SceneManager.LoadScene(back);
    }
    #region Agregar y Configurar Script de Lanzamiento de Ingredientes
    void addScript()
    {
        GameObject camera = GameObject.Find("Main Camera");
        camera.AddComponent<Ingredients>();
        camera.GetComponent<Ingredients>().img = fondo;
        camera.GetComponent<Ingredients>().frecuenciaDeCaida = frecuenciaDeCaidaDeLosIngredientes;
        camera.GetComponent<Ingredients>().velocidadDeCaida = velocidadDeCaidaDeLosIngredientes;
        can.SetActive(false);
    }
    #endregion
    #region Agregar textos del Pedido
    void addText()
    {
        float repos = 3f, fontsize=0.75f;
        for (int i = 0; i < Pedidos.ingredientes.Count; i++)
        {
            string ingNom = Pedidos.ingredientes[i].name;
            if (Pedidos.pedido.ContainsKey(ingNom))
            {
				GameObject mostrar=new GameObject();
				string texto = Pedidos.pedido[ingNom] + " de " + ingNom.ToUpperInvariant();
				Text linea = mostrar.AddComponent<Text> ();
                #region Configuración del GameObject
                mostrar.name = ingNom;
				mostrar.transform.SetParent(can.transform);
                mostrar.transform.localScale = new Vector3(fontsize, fontsize, fontsize);
				mostrar.transform.localPosition = new Vector2 (2.5f, repos);
                mostrar.GetComponent<RectTransform>().sizeDelta = new Vector2(10f, 2f);
                repos-=fontsize+0.1f;
                #endregion
                #region Configuración de Font
                linea.text = texto;
                linea.color = Color.black;
                linea.fontSize = 1;
                linea.font = Resources.Load<Font>("font_pedidos");
                #endregion
            }                            
        }
    }
    #endregion
    #region Posicionar GameObjects del Pedido
    void addClone(){
        
        GameObject clon;            
        float repos = 3.3f,size=0.75f;                                                                  // Valor Inicial de la altura, y el tamaño del ingrediente
        for (int i = 0; i < Pedidos.ingredientes.Count; i++)
        {
            string ingNom = Pedidos.ingredientes[i].name;
            if (Pedidos.pedido.ContainsKey(ingNom))
            {
                Vector3 pos = new Vector3(-2.2f, repos, 1f);//-10f);
                clon = Instantiate(Pedidos.ingredientes[i], (new Vector3(0f,0f,0f)), Quaternion.identity) as GameObject;    // Instancio el ingrediente 
                clon.tag = "SeQueda";
                clon.transform.SetParent(can.transform,false);                                          // Hijo del Canvas....
                clon.transform.localPosition = pos;                                                     // ReConfiguro la pos del Ingrediente
                clon.transform.localScale = new Vector3(size,size,size);                                // ReConfiguro el tamaño
                repos = repos - size-0.1f;                                                              // Asigno la pos en Y del proximo
            }
        }
    }
    #endregion
    #region Agregar textos del PedidoUP
    private void addTextUP()
    {
        float repos = -6f, fontsize = 0.5f;
        for (int i = 0; i < Pedidos.ingredientes.Count; i++)
        {
            string ingNom = Pedidos.ingredientes[i].name;
            if (Pedidos.pedido.ContainsKey(ingNom))
            {
                GameObject mostrar = new GameObject();
                string texto = "x"+Pedidos.pedido[ingNom];
                Text linea = mostrar.AddComponent<Text>();
                #region Configuración del GameObject
                mostrar.name = ingNom;
                mostrar.tag = "SeQueda";
                mostrar.transform.SetParent(canup.transform);
                mostrar.transform.localScale = new Vector3(fontsize, fontsize, fontsize);
                mostrar.transform.localPosition = new Vector2(repos, 3.65f);
                mostrar.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 5f);
                repos += 2.25f;
                #endregion
                #region Configuración de Font
                linea.text = texto;
                linea.color = Color.white;
                linea.fontSize = 1;
                linea.font = Resources.Load<Font>("font_pedidos");
                #endregion
            }
        }
    }    
    #endregion
    #region Posicionar GameObjects del PedidoUP
    private void addCloneUP()
    {
        GameObject clon;
        float repos = -7f, size = 0.75f;
        for (int i = 0; i < Pedidos.ingredientes.Count; i++)
        {
            string ingNom = Pedidos.ingredientes[i].name;
            if (Pedidos.pedido.ContainsKey(ingNom))
            {
                //Vector3 pos = new Vector3(repos, 5.25f, 0f);//Y=0.085f
                clon = Instantiate(Pedidos.ingredientes[i], (new Vector3(repos, 5.25f, 0f)), Quaternion.identity) as GameObject;    // Instancio el ingrediente 
                clon.transform.SetParent(canup.transform, false);                                          // Hijo del Canvas....
                //clon.transform.localPosition = pos;                                                        // ReConfiguro la pos del Ingrediente
                clon.transform.localScale = new Vector3(size, size, size);                                 // ReConfiguro el tamaño
                Destroy(clon.GetComponent<BoxCollider2D>());
                repos += 2.25f;
            }
        }
    }
    #endregion
}
