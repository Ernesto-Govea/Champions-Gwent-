using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Lider_Card : MonoBehaviour
{
   public string Name;
   public DECKS deck;
   public bool EffectActivated;
   public MANOS manos;
   void Start()
   {
      deck = transform.parent.GetComponent<DECKS>();
   }
   public void OnMouseDown()// Este método se ejecuta cuando se hace clic en la carta dorada, llama al método Invocar_GoldCard  al que está asociada esta carta, pasándose a sí misma como argumento.y luego, llama al método ActualizarTextoSumaAtaque  para actualizar la suma de los puntos de ataque en el tablero del juego
    {
        if(!EffectActivated)
        {
         deck.EfectoLider();
         EffectActivated =  true;
         deck.IncrementarCartasJugadas();
        }
        else
        {
         Debug.Log("Ya el efecto fue activado"); 
        }   
    }
}