using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Despeje_Card : MonoBehaviour
{
    
    public string Name;
   
    public string Posicion_Fila;

    public MANOS manos;
    public bool invocada;
    public bool destruida;
    void Start()
    {
        manos = transform.parent.GetComponent<MANOS>();
    }

    public void OnMouseDown()
    {
  
        if(destruida)Debug.Log("Ya esta carta fue destruida");
         else if(!invocada)
         {
           manos.Invocar_DespejeCard(this);
           invocada = true;
           manos.Efecto_Despeje();
           manos.cartas_Jugadas++;
      
         }
        
        else if(!manos.Turno)Debug.Log("No es tu turno");
        else if(manos.cartas_Jugadas!=0 && !manos.Posibilidad_de_Convocar)Debug.Log("No puedes jugar mas de una carta");
  
    }
    }

