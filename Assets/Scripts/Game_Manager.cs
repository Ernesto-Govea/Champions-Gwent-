
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public DECKS jugador_Deck;//referencia al mazo del jugador 1
   public DECKS rival_Deck;// referencia mazo jugador 2
  
   public MANOS jugador;// referencia a la mano y sus interacciones
   public MANOS jugador_Rival;// referencia a la mano y sus interacciones
 

   //Estas variables son referencias a los componentes que representan las distintas zonas de juego
  /* public Ataque MeleeZone_Jugador1;
   public Ataque MeleeZone_Rival;
   public Distancia Distancia_Zona_Jugador1;
   public Distancia Distancia_Zona_Rival;
   public Asedio Asedio_Zona_Jugador1;
      public Asedio Asedio_Zona_Rival;*/

   //variables para almacenar los puntos de ambos jugadores
   public int puntos;
   public int puntos_Rivales;
    public GameObject Lider_Zona;
    public GameObject Lider_Rival_Zona;
    public GameObject cartaLiderPrefab;


   void Start()// se llama al inicio del juego. Baraja los mazos del jugador y del rival, establece que el jugador tiene el primer turno
   {
    Debug.Log("Ronda 1");
   
       jugador_Deck.InvokeLeaderCard(cartaLiderPrefab,Lider_Zona);
       rival_Deck.InvokeLeaderCard(cartaLiderPrefab,Lider_Rival_Zona);
    for(int i =0; i < 10; i++)
    {
      jugador_Deck.ShuffleDeck();
      rival_Deck.ShuffleDeck();
    }
      jugador.Turno = true;
       Debug.Log("Turno del jugador 1");

   }
 
    void Update()//Actualiza los puntos de ataque del jugador y del rival, y luego comprueba si ambas manos han pasado la ronda para determinar al ganador
   {
    // actualizan los puntos de ataque del jugador y del rival llamando al método ActualizarTextoSumaAtaque() en las instancias de MANOS del jugador y del rival, respectivamente
    puntos = jugador.ActualizarTextoSumaAtaque();
    puntos_Rivales = jugador_Rival.ActualizarTextoSumaAtaque();

    if(MANOS.ronda == 1)// verifica que estemos en la 1era romda del juego
    {
      if(jugador.paso_Ronda && jugador_Rival.paso_Ronda)//Verifica si tanto el jugador como el rival han pasado la ronda. esto se hace accediendo a las propiedades paso_Ronda de las instancias jugador y jugador_Rival de la clase MANOS
      {
         Ganador_Ronda();// si ambos jugadores han pasado de ronda se llma a este metodo para determinar quien  es el ganador
         Nueva_Ronda();// inicia la siguiente ronda
         Debug.Log("Comienza la ronda # 2");
        
      }
    }
   else if (MANOS.ronda == 2)
    {
        if (jugador.paso_Ronda && jugador_Rival.paso_Ronda) // Verifica si ambos jugadores han pasado la ronda
        {
            Ganador_Ronda(); // Determina el ganador de la ronda y asigna el turno
            if (jugador.Rondas_GANADAS == 2)
            {
              SceneManager.LoadScene("Ganador Jugador1");
            }
            else if(jugador_Rival.Rondas_GANADAS == 2)
            {
              SceneManager.LoadScene("Ganador Jugador2");
              }
            
                  Nueva_Ronda(); // Prepara la siguiente ronda
                  Debug.Log("Comienza la ronda #3");
                
        }
    }
     else if (MANOS.ronda == 3)
    {
        if (jugador.paso_Ronda && jugador_Rival.paso_Ronda) // Verifica si ambos jugadores han pasado la ronda
        {
            Ganador_Ronda(); // Determina el ganador de la ronda y asigna el turno
            if (jugador.Rondas_GANADAS > jugador_Rival.Rondas_GANADAS)
            {
              SceneManager.LoadScene("Ganador Jugador1");
            }
            else if (jugador.Rondas_GANADAS < jugador_Rival.Rondas_GANADAS)
            {
               SceneManager.LoadScene("Ganador Jugador2");
            }
            else if (jugador.Rondas_GANADAS == jugador_Rival.Rondas_GANADAS)
            {
                Debug.Log("El juego termino en Empate");
            }
        }
    }
   }



   public void Cambio_Turno_Jugador1()
    {
      if( jugador.cartas_Jugadas != 0 && !jugador_Rival.paso_Ronda)//verifica si el jugador actual (jugador 1) ha jugado al menos una carta (jugador.cartas_Jugadas != 0) y si el jugador rival no ha pasado la ronda (!jugador_Rival.paso_Ronda). Esto significa que el jugador 1 solo puede cambiar su turno si ha jugado al menos una carta y si el jugador rival aún no ha pasado la ronda
      {
        //Si se cumple la condición anterior, se ejecuta este bloque de código. Establece el turno del jugador actual (jugador 1) en false para indicar que ya no es su turno
        //luego, establece el turno del jugador rival (jugador 2) en true para indicar que es su turno. También restablece el contador de cartas jugadas del jugador rival a 0. Finalmente, muestra un mensaje en la consola indicando que es el turno del jugador 2.
        jugador.Turno = false;
        jugador_Rival.Turno = true;
        jugador_Rival.cartas_Jugadas = 0;
        Debug.Log("Es el turno del jugador 2");
      }
      else if(jugador_Rival.paso_Ronda)//Si la condición anterior no se cumple y el jugador rival ha pasado la ronda, comienza
      {
        Debug.Log("No puedes pasar tu turno, ya el rival ha terminado su ronda");
      }
      else
      //Si ninguna de las condiciones anteriores se cumple, comienza este bloque de código. Esto significa que el jugador actual (jugador 1) no ha jugado ninguna carta y el jugador rival tampoco ha pasado la ronda
      {
        Debug.Log("No se puede , hay que jugar una carta por lo menos");
      }
    }
    
    public void Cambio_Turno_Rival()
    {
      if(  jugador_Rival.cartas_Jugadas != 0 && !jugador.paso_Ronda)
      {
       jugador.Turno= true;
       jugador_Rival.Turno= false;
       jugador.cartas_Jugadas = 0;
       Debug.Log("Es el turno del jugador 1");
      }
      else if(jugador.paso_Ronda)
      {
        Debug.Log("No puede pasar el turno ,ya el jugador contrario termino su ronda");
      }
      else
      {
        Debug.Log("Debe jugar al menos una carta");
      }
    }
    public void Pasar_Ronda_Jugador1()
    {
      if(jugador.cartas_Jugadas == 0 || jugador.Cards_In_Hand.Count==0 || jugador_Rival.Turno)//El jugador actual no ha jugado ninguna carta (jugador.cartas_Jugadas == 0).El jugador actual no tiene cartas en la mano (jugador.Cards_In_Hand.Count == 0).Es el turno del jugador rival (jugador_Rival.Turno). Si alguna de estas condiciones es verdadera, el jugador 1 puede pasar de ronda
      {
        Debug.Log("Jugador 1 cedio Paso su Ronda");
        jugador.paso_Ronda = true;//Establece la variable paso_Ronda del jugador actual (jugador 1) en true, indicando que ha pasado la ronda
        jugador.Turno = false;//Establece el turno del jugador actual (jugador 1) en false para indicar que ya no es su turno
        jugador_Rival.Turno = true;//Luego, establece el turno del jugador rival en true para indicar que es su turno
        jugador_Rival.cartas_Jugadas = 0;//Restablece el contador de cartas jugadas del jugador rival a 0 
      jugador_Rival.Posibilidad_de_Convocar = true;//establece la posibilidad de convocar cartas para el jugador rival en true
      }
      else
      {
        Debug.Log("No puede pasar de ronda en este turno");
      }
    }
    public void Pasar_Ronda_Rival()
    {
      if(jugador_Rival.cartas_Jugadas == 0 || jugador_Rival.Cards_In_Hand.Count==0 || jugador.Turno)
      {
        Debug.Log("Jugador 2 cedio Paso su Ronda");
        jugador_Rival.paso_Ronda = true;
        jugador.Turno = true;
        jugador_Rival.Turno = false;
        jugador.cartas_Jugadas = 0;
        jugador.Posibilidad_de_Convocar = true;
      }
      else
      {
        Debug.Log("No puede pasar de ronda en este turno");
      }
    }
    public void Ganador_Ronda()
    {
       Debug.Log("Se acabo la ronda");
       //Establece el turno tanto del jugador 1 como del jugador rival en false, indicando que ninguno de los jugadores tiene el turno
        jugador.paso_Ronda = false;
        jugador_Rival.paso_Ronda = false;
        //Restablece el contador de cartas jugadas tanto del jugador 1 como del jugador rival a 0.
        jugador.cartas_Jugadas = 0;
        jugador_Rival.cartas_Jugadas = 0;
        //Da posibilidad de convocar cartas tanto para el jugador 1 como para el jugador rival en false, lo que significa que no pueden convocar más cartas en esta ronda
        jugador.Posibilidad_de_Convocar = false;
        jugador_Rival.Posibilidad_de_Convocar = false;

        Debug.Log("Calculando los puntos para ver al ganador");
        if(puntos > puntos_Rivales)// verifica que los puntos del jugador 1 sean mayores que los del rival
        {
          // muestra e mensaje e incrementa el contador de rondas ganadas del jugador 1 y avanza al siguiente número de ronda
          Debug.Log("El jugador 1 a ganado esta Ronda");
          jugador.Rondas_GANADAS++;
          MANOS.ronda++;
          jugador.Turno = true;
          jugador_Rival.Turno = false;
        }
        else if(puntos < puntos_Rivales)// verifica que los puntos del jugador rivale sean mayores que los del jugador 1
        {
          //// muestra e mensaje e incrementa el contador de rondas ganadas del jugador rival y avanza al siguiente número de ronda
          Debug.Log("EL jugador 2 a ganado esta Ronda");
          jugador_Rival.Rondas_GANADAS++;
          MANOS.ronda ++;
          jugador.Turno = false;
          jugador_Rival.Turno = true;
        }
        else if(puntos == puntos_Rivales)// analiza si hay empate de puntos
        {
          Debug.Log("La Ronda ha terminado en empate");
          MANOS.ronda ++;// avanza a la siguiente ronda 
        }
    }

    public void Nueva_Ronda()
    {
      jugador.Limpia_Tablero();
      jugador_Rival.Limpia_Tablero();
      jugador.ACtualiza_Marcador();
      jugador_Rival.ACtualiza_Marcador();
      jugador.Actualiza_Marcador_RONDA();
      jugador_Rival.Actualiza_Marcador_RONDA();


      for(int i = 0; i < jugador_Deck.listaBooleana.Count;i++)
      {
        if(jugador_Deck.listaBooleana[i]== false)
        {

        for(int j = 0; j < 2; j++)//este bucle que se ejecuta dos veces. En cada iteración, ambos jugadores roban una carta de su mazo (jugador_Deck y rival_Deck). Esto se hace para que a  la mano vayan 2 nuevas cartas a la mano en cada ronda para ambos jugadores

          {
            jugador_Deck.RobarCarta();
          }
        }   
      }
      
      for(int i = 0; i < rival_Deck.listaBooleana.Count;i++)
      {
        if(rival_Deck.listaBooleana[i]== false)
        {

        for(int j = 0; j < 2; j++)//este bucle que se ejecuta dos veces. En cada iteración, ambos jugadores roban una carta de su mazo (jugador_Deck y rival_Deck). Esto se hace para que a  la mano vayan 2 nuevas cartas a la mano en cada ronda para ambos jugadores

          {
            rival_Deck.RobarCarta();
          }
        }   
      }
    }
public int TriggerPlayer()
{
    if (jugador.Turno)
    {
        return 0;
    }
    else 
    {
        return 1;
    }
}

    
}
