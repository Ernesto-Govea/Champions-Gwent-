using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather_Card : MonoBehaviour
{
    public string Name;
  //  public int Atk;
    public string Posicion_Fila;
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
            manos.Invocar_Weather_Card(this);
            manos.cartas_Jugadas++;
            if(this.Name == "Tarjeta Roja")
            {
              manos.Efecto_Clima("Asedio",5 ,this);
            }
            else if(this.Name == "Tarjeta Amarilla")
            {
              manos.Efecto_Clima("Distancia",3,this);
            }
             else if(this.Name == "Signal Indiuna Park")
            {
              manos.Efecto_Clima("Cuerpo a Cuerpo",2,this);
            }
        }
        else if (manos.cartas_Jugadas != 0 && !manos.Posibilidad_de_Convocar)
        {
            Debug.Log("No puedes convocar");
        }
          else if (!manos.Turno)
        {
            Debug.Log("Ya no es tu turno , no puedes convocar cartas");
        }
      
        //manos.Invocar_Weather_Card(this);
        
    }
}
