using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aumento_Card : MonoBehaviour
{
    
    public string Name;
    public string Posicion_Fila;
    //  public int Atk;
    public List<Silver_Card> cartas_afectadas = new List<Silver_Card>();
     public MANOS manos;
    void Start()
    {
        manos = transform.parent.GetComponent<MANOS>();
    }

    public void OnMouseDown()
    {
         if(manos.Turno && manos.cartas_Jugadas == 0 || manos.Posibilidad_de_Convocar)
        {
            manos.Invocar_Aumento_Card(this);
            manos.cartas_Jugadas++;
        }
         if(this.Name == "Energizante")
            {
              manos.Efecto_Aumento("Cuerpo a Cuerpo", 5 );
            }
            else if(this.Name == "Pura Tactica")
            {
              manos.Efecto_Aumento("Distancia",15);
            }
             else if(this.Name == "Penales")
            {
              manos.Efecto_Aumento("Cuerpo a Cuerpo",10);
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
