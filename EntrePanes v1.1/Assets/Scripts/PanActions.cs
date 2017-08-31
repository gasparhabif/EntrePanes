using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Leap;
using Windows.Kinect;

public class PanActions : MonoBehaviour {

    #region Variables
    #region Leap Motion

    #endregion
    #region Kinect
    KinectSensor kinectSensor;
    BodyFrameReader bodyFrameReader;
    Body[] bodies = null;
    #endregion
    int sebagay=8;
    #region Objetos Fisicos
    static GameObject pan;
    #endregion

    #region Variables de Movimiento
    float hMovement, sentido;                             // Sentido hace referencia a si va hacia la izquierda o hacia la derecha
    int direccion = 0;                                    // Tope de cada lado --> Direccion: 1 (Izquierda) | 2 (Derecha)
    public static float panY = -4f;

    [Range(0.05f, 0.5f)]
    public float speed;
    #endregion
    #region Variables para Flow
    [HideInInspector]                   // Que no aparezcan en el Inspector
    public static float repos = 0.3f;
    public static int bajar = 0;        // Contador para bajar la hamburguesa en pantalla
    #endregion

    #endregion

    void Start() {
        pan = this.gameObject;
        //hController.enabled = false;
    }

    void Update() {
        Debug.Log(sebagay);
        switch (InputSelection.input)
        {
            #region Teclado
            case "Keyboard":
            case "Teclado":
                {
                    sentido = Input.GetAxis("Horizontal");
                    if ((direccion == 0) || (direccion == 1 && sentido > 0) || (direccion == 2 && sentido < 0))
                    {
                        hMovement = sentido * speed;
                        if (sentido < 0)
                            pan.transform.position = new Vector2(Mathf.Lerp(pan.transform.position.x, pan.transform.position.x + hMovement, -sentido), panY);
                        if (sentido > 0)
                            pan.transform.position = new Vector2(Mathf.Lerp(pan.transform.position.x, pan.transform.position.x + hMovement, sentido), panY);

                        direccion = 0;                                                                        // Direccion = 0 para que pueda moverse libremente
                    }
                }
                break;
            #endregion
            #region Leap Motion
            case "Leap Motion":
                {

                }
                break;
            #endregion
            #region Kinect
            case "Kinect":
                {
                    #region Config Inicial Kinect
                    kinectSensor = KinectSensor.GetDefault();
                    if (kinectSensor != null)                                           // Si el sensor esta conectado
                    {
                        bodyFrameReader = kinectSensor.BodyFrameSource.OpenReader();    // Estabelzco conexion con la deteccion del cuerpo
                        if (!kinectSensor.IsOpen)                                       // Si el sensor esta conectado pero no activo...
                        {   
                            kinectSensor.Open();                                        // Lo activo ;)
                        }
                    }
                    #endregion
                    #region Chequeo de Frames y Cuerpos
                    var frame = bodyFrameReader.AcquireLatestFrame();               // Pido el frame mas reciente
                    if (frame != null)                                              // Si el frame mas reciente es valido
                    {
                        if (bodies == null)                                         // Si la variable bodies que se encarga de cargar los cuerpos detectados no esta inicializada
                        {
                            bodies = new Body[kinectSensor.BodyFrameSource.BodyCount];  // La inicializo...
                        }
                        frame.GetAndRefreshBodyData(bodies);                        // Refresco el conteo de cuerpos
                        foreach (var body in bodies)
                        {
                            if (body != null)
                            {
                                if (body.IsTracked)                                 // Chequeo que no sea un cuerpo vacio, que este siendo detectado por el sensor
                                {
                                    #endregion
                                    #region Ejercicios
                                    switch (InputSelection.ejercicio)
                                    {
                                        #region Brazos
                                        case "Brazo Izquierdo":
                                        case "Brazo Derecho":
                                            {
                                                Windows.Kinect.Joint brazo;
                                                if (InputSelection.ejercicio=="Brazo Derecho")
                                                    brazo = body.Joints[JointType.ElbowRight];                  // Determino el brazo a usar
                                                else
                                                    brazo = body.Joints[JointType.ElbowLeft];

                                                hMovement = brazo.Position.Y + 0.15f;                           // Balance para que el 0 movimiento sea un poco mas bajo que el real
                                                                                                                // Para que sea mas confortable para el paciente

                                                float panX = pan.transform.position.x;
                                                if ((panX > -10.5f && hMovement < 0) || (panX < 10.5f && hMovement > 0))    // Topes de cada lado
                                                {
                                                    pan.transform.position = new Vector2(panX + hMovement, panY);     // Movimiento del pan 
                                                }
                                            }
                                            break;
                                        #endregion
                                        #region Cuello
                                        case "Cuello":
                                            {
                                                Windows.Kinect.Joint head = body.Joints[JointType.Head];
                                                hMovement = (float) System.Math.Round(head.Position.X * 5,2);

                                                if (hMovement >= 0f)
                                                {
                                                    pan.transform.position = new Vector2(Mathf.Lerp(pan.transform.position.x, pan.transform.position.x + hMovement, hMovement), panY);
                                                }
                                                else
                                                {
                                                    pan.transform.position = new Vector2(Mathf.Lerp(pan.transform.position.x, pan.transform.position.x + hMovement, -hMovement), panY);
                                                }
                                            }
                                            break;
                                        #endregion
                                        #region Torso  
                                        case "Torso":
                                            {

                                            }
                                            break;
                                        #endregion
                                    }
                                }

                            }
                        }
                        frame.Dispose();
                        frame = null;
                    }
                    #endregion
                }
                break;
                #endregion
        }
        #region Flow del Pan
        
        if (bajar > 0 && bajar < 11)                                                              // Si estoy dentro del contador
        {
            panY -= 0.05f;//0.025f;                                                               // Establezco la nueva pos en Y
            pan.transform.position = new Vector2(pan.transform.position.x, panY);                 // Lo llevo a la nueva pos
            bajar++;                                                                              // Incremento el contador
        }
        
        #endregion
        panY = pan.transform.position.y;
    }


    void OnTriggerEnter2D(Collider2D collision) {        
        #region Deteccion de Costados
        if (collision.gameObject.name == "BordeDerecho")    // Detecto contra que borde esta chocando
            direccion = 2;                                  // En caso de ser el derecha, direccion=2
        if (collision.gameObject.name == "BordeIzquierdo")  // En caso de ser el izquierda, direccion=1
            direccion = 1;
        #endregion
    }

    #region Funciones

    public static void PosicionPan(GameObject ing)
    {                
        ing.transform.parent = pan.transform;                                                                   // Seteo al ingrediente como hijo del Pan
        ing.transform.position = new Vector2(pan.transform.position.x, (pan.transform.position.y + repos));     // Transformo la posicion del Ingrediente, igual a la del pan, más la altura correspondiente a la cantidad de ingredientes que haya.        

        pan.GetComponent<Collider2D>().transform.position = ing.transform.position;                             // Reposiciono el Collider, arriba de todo para el proximo ingrediente
        repos += 0.38f;                                                                                         // Incremento el valor de $repos para que el proximo ingrediente se posicione por encima del anterior
        bajar = 1;                                                                                              // Igualo $bajar a 1 para que comience el contador de bajada                                                                                                            
    }
    #endregion
}
