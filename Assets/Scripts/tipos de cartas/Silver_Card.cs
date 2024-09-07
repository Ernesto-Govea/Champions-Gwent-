using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silver_Card : MonoBehaviour
{
     public string Name;
    public int Atk;
    public string Posicion_Fila;
     public MANOS manos;
     
     public bool efecto_activado;
     //public bool invocada;


    void Start()
    {
        manos = transform.parent.GetComponent<MANOS>();
    }

    public void OnMouseDown()
    {
        if(manos.Turno && manos.cartas_Jugadas == 0 || manos.Posibilidad_de_Convocar)
        {
            manos.Invocar_SilverCard(this);
            manos.ActualizarTextoSumaAtaque();
            manos.cartas_Jugadas++;
        }
    
        else if (manos.cartas_Jugadas != 0 && !manos.Posibilidad_de_Convocar)
        {
            Debug.Log("No puedes convocar");
        }
          else if (!manos.Turno)
        {
            Debug.Log("Ya no es tu turno , no puedes convocar cartas");
        }
      
        
    }

}

