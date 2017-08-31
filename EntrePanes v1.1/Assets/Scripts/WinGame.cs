using UnityEngine;
using System.Collections;

public class WinGame : MonoBehaviour {

    public static int correcto;
    bool unaVez = true;
    int cantidad;
    float cuenta;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (unaVez&&correcto!=0)
        {
            cuenta = correcto / 2;
            cantidad = (int)Mathf.Ceil(cuenta);            
            unaVez = false;
            switch (cantidad)
            {
                default:
                    Debug.Log("Bien");
                    break;
                case 1:
                    Debug.Log("Bien");
                    break;
                case 2:
                    Debug.Log("Genial");
                    break;
                case 3:
                    Debug.Log("Muy Bien!");
                    break;
                case 4:
                    Debug.Log("Excelente!!");
                    break;
                case 5:
                    Debug.Log("PERFECTA!!!");
                    break;
            }
        }
	}
}
