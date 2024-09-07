using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Golden_Card : MonoBehaviour
{
    public string Name;
    public int Atk;
    public string Posicion_Fila;//para almacenar la posición de la fila en la que se invocará la carta dorada

    public bool carta_Invocada ;

    public MANOS manos;//variable que se utilizará para hacer referencia al componente MANOS que gestiona las manos de los jugadores
    void Start()//Este método se ejecuta al inicio y asigna el componente MANOS del objeto padre al que pertenece esta carta dorada a la variable manos
    {
        manos = transform.parent.GetComponent<MANOS>();
    }

    public void OnMouseDown()// Este método se ejecuta cuando se hace clic en la carta dorada, llama al método Invocar_GoldCard  al que está asociada esta carta, pasándose a sí misma como argumento.y luego, llama al método ActualizarTextoSumaAtaque  para actualizar la suma de los puntos de ataque en el tablero del juego
    {
        if(manos.Turno && manos.cartas_Jugadas == 0 || manos.Posibilidad_de_Convocar)
        {
            manos.Invocar_GoldCard(this);
            manos.ActualizarTextoSumaAtaque();
            manos.cartas_Jugadas++;
            if(this.Name == "Messi")
            {
              manos.Efecto_Messi("Distancia", 10 );
              
            }
            else if(this.Name == "Maldini")
            {
                manos.Elimina_Carta_Menor_Atk();
            }
           
        }

        else if(manos.cartas_Jugadas !=0 && !manos.Posibilidad_de_Convocar)
        { 
            Debug.Log("No puedes convocar ") ; 
        
        }
        else if (!manos.Turno)
        {
            Debug.Log("Ya no es tu turno , no puedes convocar cartas");
        }
    
        
    }

}
