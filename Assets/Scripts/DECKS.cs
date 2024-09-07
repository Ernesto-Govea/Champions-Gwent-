using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DECKS : MonoBehaviour
{
    // script que gestiona las manos de los jugadores
    public MANOS Hand;
    
    // Lista de GameObjects que representan las cartas en el mazo
    public List<GameObject> deck = new List<GameObject>();
    
    // Lista de booleanos que indican si una posición en la mano está ocupada o no
    public List<bool> listaBooleana = new List<bool>();
    
   
    // Método llamado al inicio del juego
    void Start()
    {
       ;
        // Baraja el mazo al inicio del juego
        ShuffleDeck();
       // Hand.Invocar_Lider();
        
        // Roba un número inicial de cartas y las asigna a la mano
        for(int i = 0; i < 10; i++)
        {
            RobarCarta();
        }
    }
    // Método para barajr el mazo
    public void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            GameObject temp = deck[i];// Guarda la carta actual en una variable temporal
            int randomIndex = Random.Range(i, deck.Count); // Genera un índice aleatorio entre el índice actual y el final del mazo
            deck[i] = deck[randomIndex];// Intercambia la carta actual con la carta en el índice aleatorio generado
            deck[randomIndex] = temp;// Coloca la carta guardada en la variable temporal en el lugar de la carta intercambiada
        }
    }

    // Método para robar una carta del mazo y asignarla a la mano
    public void RobarCarta()
    {
        // Guarda la carta en la parte superior del mazo
        GameObject Copia_Carta = deck[0];
        // Elimina la carta del mazo
        deck.RemoveAt(0);

        // Itera sobre la lista de posiciones de la mano
        for(int i = 0; i < listaBooleana.Count; i++)
        {
            // Si la posición de la mano está libre
            if (listaBooleana[i] == false)
            {
                // Instancia una copia de la carta en la posición de la mano
                GameObject CARTA = Instantiate(Copia_Carta, Hand.ZONAS[i].transform.position, Quaternion.identity);
                // Establece el padre de la carta como la mano
                CARTA.transform.SetParent(Hand.transform);
                // Inserta la carta en la lista de cartas en la mano
                Hand.Cards_In_Hand.Insert(i, CARTA);
                // Marca la posición como ocupada
                listaBooleana[i] = true;
                // Sale del bucle
                break;
            }
            // Si se han revisado todas las posiciones y ninguna está libre
            if(i == 9)
            {
                // Muestra un mensaje de advertencia
                Debug.Log("No se pueden robar  mas cartas");
            }
        }
    }
    //dudas aquí
    public void InvokeLeaderCard(GameObject PrefabLeaderCard,GameObject LeaderZone)
    {
        GameObject LeaderCard = Instantiate(PrefabLeaderCard, LeaderZone.transform.position,Quaternion.identity);
        LeaderCard.transform.SetParent(this.transform);
    }
    public void EfectoLider()
    {
        Hand.Efecto_Promedio();
    }
    public void IncrementarCartasJugadas()
    {
        Hand.cartas_Jugadas++;
    }  
}
