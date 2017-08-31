using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class InputSelection : MonoBehaviour {

    #region Variables
    public Dropdown lista, ejercicios;
    public static string input = "Keyboard",ejercicio;
    List<string> ej_leap = new List<string>(), ej_kinect = new List<string>();
    #endregion
    // Use this for initialization
    void Start () {
        input = lista.captionText.text.ToString();
        #region Creación de Listas
        ej_leap.Add("Traslación");
        ej_leap.Add("Abrir-Cerrar");
        ej_leap.Add("Rotación");
        ej_leap.Add("Muñeca");
        ej_kinect.Add("Cuello");
        ej_kinect.Add("Torso");
        ej_kinect.Add("Brazo Izquierdo");
        ej_kinect.Add("Brazo Derecho");
        ej_kinect.Add("Ambos Brazos");
        #endregion
        ejercicios.GetComponentInChildren<Text>().text = "Seleccione el ejercicio";
    }
    void Update()
    {
        input = lista.captionText.text.ToString();
        if (input == "Teclado"){
            ejercicios.ClearOptions();
            ejercicios.interactable = false;
        }else {
            if (input == "Leap Motion"){
                ejercicios.interactable = true;
                ejercicios.ClearOptions();
                ejercicios.AddOptions(ej_leap);
            }
            if (input == "Kinect"){
                ejercicios.interactable = true;
                ejercicios.ClearOptions();
                ejercicios.AddOptions(ej_kinect);
            }
        }
        ejercicio = ejercicios.captionText.text.ToString();
    }
    public void Seleccion()
    {
        SceneManager.LoadScene(1);
    }
}
