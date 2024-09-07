using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MANOS : MonoBehaviour
{
    public List<GameObject> ZONAS = new List<GameObject>();// lista de GameObjects llamada ZONAS, que se utiliza para almacenar las zonas del campo de juego ,cada zona será representada por un GameObject en Unity
    public List<GameObject> Cards_In_Hand = new List<GameObject>();//Esta línea declara una lista de GameObjects llamada Cards_In_Hand, que se utiliza para almacenar las cartas en la mano del jugador, cada carta será representada por un GameObject en Unity
    //public List<int> valoresDeAtaqueEnCampo = new List<int>();// Define una lista para almacenar los valores de ataque de todas las cartas en el campo

    

    public TextMeshProUGUI Marcador; // Referencia al objeto TextMeshPro que mostrará la suma de los puntos de ataque
    public TextMeshProUGUI Marcador_Ronda;
    public DECKS Mazo;//: Esta línea declara una variable de tipo DECKS llamada Mazo ,esta variable se utiliza para hacer referencia al script DECKS, que gestiona el mazo de cartas del juego
    public MANOS jugador_Rival;// referencia a la mano y sus interacciones
    public Clima Clima_Zona;

    public Ataque AttackController;//Se utiliza para hacer referencia al script Ataque, que controla las acciones relacionadas con la fila Cuerpo a Cuerpo
    public Distancia DistanciaController;//declara una variable de tipo Distancia llamada DistanciaController, que hace referencia al script Distancia
    public Asedio AsedioController;//declara una variable de tipo Asedio llamada AsedioController, que hace referencia al script Asedio
    public Clima ClimaController;//declara una variable de tipo Clima llamada ClimaController, que hace referencia al script Clima
    public Aumento AumentoController;//declara una variable de tipo Aumento llamada AumentoController, que hace referencia al script Aumento
    public Cementerio cementerio;// encargado de las acciones relacionadas con el cementerio de cartas en el juego.
    
    
// Arrays para controlar las posiciones ocupadas en cada fila del campo
    
     public  bool[] posicionesOcupadas_Ataque = new bool[5];
     public  bool[] posicionesOcupadas_Distancia = new bool[5];
     public  bool[] posicionesOcupadas_Asedio = new bool[5];
     public  bool[] posicionesOcupadas_Clima = new bool[3];
     public  bool[] posicionesOcupadas_Aumento = new bool[3];
    public List<GameObject> CartasClimaEnElCampo = new List<GameObject>();
// Variables de control del turno y la ronda
    public bool Turno; //esto es para saber si estoy o no en mi turno
    public bool paso_Ronda;// se utiliza para determinar si el jugador ha pasado su turno en la ronda actual
    public bool Posibilidad_de_Convocar;//determinar si el jugador tiene la posibilidad de convocar cartas en su turno
     public static int ronda =1;//# de rondas de juego empieza por defecto en la 1era ronda
     public int Rondas_GANADAS = 0;// para llevar la cuenta del # de rondas ganadas por el jugador
     public int cartas_Jugadas = 0;// para llevar la cuenta de las cartas que has jugado en el tuno
     public List<GameObject> activeClimaCards = new List<GameObject>();
     public bool effectActive = true;

     //Invocar carta lider
    
    

   // Los siguientes metodos son para invocar los disferentes tipos de cartas ...explicados a traves del comentado de el codigo de invocar las weather cards
    public void Invocar_GoldCard(Golden_Card CartaOro)//metodo para invocar carta oro
    {
        if(CartaOro.Posicion_Fila == "Cuerpo a Cuerpo")//verifica si la posición de la fila de la carta dorada es "Cuerpo a Cuerpo
        {
            for(int i = 0; i < posicionesOcupadas_Ataque.Length; i++)//itera sobre cada posición en la fila de ataque del campo de juego
            {
                if(posicionesOcupadas_Ataque[i] == false)//verifica si la posición actual en la fila de ataque está libre
                {
                    CartaOro.transform.position = AttackController.position_ataque_fila[i].transform.position;//asigna la posición de la carta dorada en la fila de ataque del campo de juego, utilizando la posición correspondiente en el AttackController
                    AttackController.Cartas_Fila_Ataque[i] = CartaOro.gameObject;//Agrega el GameObject de la carta dorada al AttackController para llevar un registro de las cartas en esa fila.
                    int index = Cards_In_Hand.IndexOf(CartaOro.gameObject);//Obtiene el índice de la carta dorada en la mano del jugador.
                    Cards_In_Hand[index] = null;//Remueve la carta dorada de la mano del jugador
                    Mazo.listaBooleana[index] = false; // Inserta false en la posición de la carta dorada en la lista booleana del mazo, indicando que esa posición ahora está vacía en la mano del jugador
                    posicionesOcupadas_Ataque[i]= true;//Marca la posición en la fila de ataque como ocupada, ya que ahora contiene una carta
                    ACtualiza_Marcador();
                    break;//una vez que se ha encontrado una posición disponible y se ha jugado la carta dorada
                }
                else if( i == 4 )//erifica si se ha llegado al final de la fila de ataque y no se ha encontrado ninguna posición libre
                {
                    Debug.Log("No se puede convocar");
                }
            }
        }
        else if(CartaOro.Posicion_Fila == "Distancia")
        {
             for(int i = 0; i < posicionesOcupadas_Distancia.Length; i++)
            {
                if(posicionesOcupadas_Distancia[i] == false)
                {
                    CartaOro.transform.position = DistanciaController.position_distancia_fila[i].transform.position;
                    DistanciaController.Cartas_Fila_Distancia[i] = CartaOro.gameObject;
                    int index = Cards_In_Hand.IndexOf(CartaOro.gameObject);
                    Cards_In_Hand[index] = null;
                    Mazo.listaBooleana[index]= false;
                    posicionesOcupadas_Distancia[i]= true;
                    ACtualiza_Marcador();
                    break;
                }
                else if( i == 4 )
                {
                    Debug.Log("No se puede convocar");
                }
            }
        }
         else if(CartaOro.Posicion_Fila == "Asedio")
        {
             for(int i = 0; i < posicionesOcupadas_Asedio.Length; i++)
            {
                if(posicionesOcupadas_Asedio[i] == false)
                {
                    CartaOro.transform.position = AsedioController.position_asedio_fila[i].transform.position;
                    AsedioController.Cartas_Fila_Asedio[i]=CartaOro.gameObject;
                    int index = Cards_In_Hand.IndexOf(CartaOro.gameObject);
                    Cards_In_Hand[index]= null;
                    Mazo.listaBooleana[index] = false;
                    posicionesOcupadas_Asedio[i]= true;
                    ACtualiza_Marcador();
                    break;
                }
                else if( i == 4 )
                {
                    Debug.Log("No se puede convocar");
                }
            }
        }
    }
    public void Invocar_SilverCard(Silver_Card CartaPlata)
    {
        if(CartaPlata.Posicion_Fila == "Cuerpo a Cuerpo")
        {
            for(int i = 0; i < posicionesOcupadas_Ataque.Length; i++)
            {
                if(posicionesOcupadas_Ataque[i] == false)
                {
                    CartaPlata.transform.position = AttackController.position_ataque_fila[i].transform.position;
                    AttackController.Cartas_Fila_Ataque[i] = CartaPlata.gameObject;
                    int index = Cards_In_Hand.IndexOf(CartaPlata.gameObject);
                    Cards_In_Hand[index] = null;
                    Mazo.listaBooleana[index] = false;
                    posicionesOcupadas_Ataque[i]= true;
                    ACtualiza_Marcador();
                    break;
                }
                else if( i == 4 )
                {
                    Debug.Log("No se puede convocar");
                }
            }
        }
        else if(CartaPlata.Posicion_Fila == "Distancia")
        {
             for(int i = 0; i < posicionesOcupadas_Distancia.Length; i++)
            {
                if(posicionesOcupadas_Distancia[i] == false)
                {
                    CartaPlata.transform.position = DistanciaController.position_distancia_fila[i].transform.position;
                    DistanciaController.Cartas_Fila_Distancia[i]=CartaPlata.gameObject;
                    int index = Cards_In_Hand.IndexOf(CartaPlata.gameObject);
                    Cards_In_Hand[index]= null;
                    Mazo.listaBooleana[index]=false;
                    posicionesOcupadas_Distancia[i]= true;
                    ACtualiza_Marcador();
                    break;
                }
                else if( i == 4 )
                {
                    Debug.Log("No se puede convocar");
                }
            }
        }

         else if(CartaPlata.Posicion_Fila == "Asedio")
        {
             for(int i = 0; i < posicionesOcupadas_Asedio.Length; i++)
            {
                if(posicionesOcupadas_Asedio[i] == false)
                {
                    CartaPlata.transform.position = AsedioController.position_asedio_fila[i].transform.position;
                    AsedioController.Cartas_Fila_Asedio[i]=CartaPlata.gameObject;
                    int index = Cards_In_Hand.IndexOf(CartaPlata.gameObject);
                    Cards_In_Hand[index]=null;
                    Mazo.listaBooleana[index]=false;
                    posicionesOcupadas_Asedio[i]= true;
                    ACtualiza_Marcador();
                    break;
                }
                else if( i == 4 )
                {
                    Debug.Log("No se puede convocar");
                }
            }
        }

    }
    // Invocar cartas clima 
    public void Invocar_Weather_Card(Weather_Card CartaClima)
    {
        if(CartaClima.Posicion_Fila == "Clima Zona") //Comprueba si la posición de la carta de clima es "Clima Zona".
        {
            for(int i = 0; i < posicionesOcupadas_Clima.Length; i++)//itera sobre cada posicion de la fila
            {
                if(posicionesOcupadas_Clima[i] == false)//verifica si esa posicion de la fila esta libre
                {
                    CartaClima.transform.position = ClimaController.position_clima_fila[i].transform.position;//Posiciona la carta de clima en la posición correspondiente en el campo de juego
                    ClimaController.Cartas_Fila_Clima[i] = CartaClima.gameObject;//Añade la carta de clima a la lista de cartas en la fila de clima gestionada por el controlador de clima.
                    int index = Cards_In_Hand.IndexOf(CartaClima.gameObject);
                    Cards_In_Hand[index] = null;
                    Mazo.listaBooleana[index] = false;
                    posicionesOcupadas_Clima[i]= true;//marca la posicion en la fila como ocupada
                   jugador_Rival.posicionesOcupadas_Clima[i] = true;
                   CartasClimaEnElCampo[i] = CartaClima.gameObject;
                   jugador_Rival.CartasClimaEnElCampo[i] = CartaClima.gameObject;
                    break;//Finaliza el bucle una vez que se ha encontrado una posición disponible y se ha jugado la carta de clima.
                }
                else if( i == 2 )
                {
                    Debug.Log("No se puede convocar");
                }
            }
        }
    }
      public void Invocar_Aumento_Card(Aumento_Card CartaAumento)
    {
        if(CartaAumento.Posicion_Fila == "Aumento Zone")//// Verifica si la carta de aumento se debe colocar en la zona de aumento
        {
            for(int i = 0; i < posicionesOcupadas_Aumento.Length; i++)
            {
                if(posicionesOcupadas_Aumento[i] == false)//verifica si la posición está ocupada
                {
                    CartaAumento.transform.position = AumentoController.position_aumento_fila[i].transform.position;//// Coloca la carta en la posición vacía en la fila de aumento
                    AumentoController.Cartas_Fila_Aumento[i]=CartaAumento.gameObject;//// Añade la carta a la lista de cartas en la fila de aumento del controlador de aumento
                    int index = Cards_In_Hand.IndexOf(CartaAumento.gameObject);// Encuentra el índice de la carta en la lista de cartas en mano
                    Cards_In_Hand[index] = null;// Inserta un valor nulo en la lista de cartas en mano en el mismo índice
                    Mazo.listaBooleana[index] = false;// // Inserta un valor booleano `false` en la lista de booleanos del mazo en el mismo índice
                    posicionesOcupadas_Aumento[i]= true;// Marca la posición actual como ocupada
                   // ACtualiza_Marcador();
                    break;
                }
                else if( i == 3 )
                {
                    Debug.Log("No se puede convocar");
                }
            }
        }
    }
 public void Invocar_DespejeCard(Despeje_Card CartaDespeje)
    {
        if(CartaDespeje.Posicion_Fila == "Cuerpo a Cuerpo")
        {
            for(int i = 0; i < posicionesOcupadas_Ataque.Length; i++)
            {
                if(posicionesOcupadas_Ataque[i] == false)
                {
                    CartaDespeje.transform.position = AttackController.position_ataque_fila[i].transform.position;
                    AttackController.Cartas_Fila_Ataque[i] = CartaDespeje.gameObject;
                    int index = Cards_In_Hand.IndexOf(CartaDespeje.gameObject);
                    Cards_In_Hand[index] = null;
                    Mazo.listaBooleana[index] = false;
                    posicionesOcupadas_Ataque[i]= true;
                    Efecto_Despeje();
                    ACtualiza_Marcador();
                    break;
                }
                else if( i == 4 )
                {
                    Debug.Log("No se puede convocar");
                }
            }
        }
        else if(CartaDespeje.Posicion_Fila == "Distancia")
        {
             for(int i = 0; i < posicionesOcupadas_Distancia.Length; i++)
            {
                if(posicionesOcupadas_Distancia[i] == false)
                {
                    CartaDespeje.transform.position = DistanciaController.position_distancia_fila[i].transform.position;
                    DistanciaController.Cartas_Fila_Distancia[i] = CartaDespeje.gameObject;
                    int index = Cards_In_Hand.IndexOf(CartaDespeje.gameObject);
                    Cards_In_Hand[index] = null;
                    Mazo.listaBooleana[index] = false;
                    posicionesOcupadas_Distancia[i]= true;
                    Efecto_Despeje();
                    ACtualiza_Marcador();
                    break;
                }
                else if( i == 4 )
                {
                    Debug.Log("No se puede convocar");
                }
            }
        }
         else if(CartaDespeje.Posicion_Fila == "Asedio")
        {
             for(int i = 0; i < posicionesOcupadas_Asedio.Length; i++)
            {
                if(posicionesOcupadas_Asedio[i] == false)
                {
                    CartaDespeje.transform.position = AsedioController.position_asedio_fila[i].transform.position;
                    AsedioController.Cartas_Fila_Asedio[i] = CartaDespeje.gameObject;
                    int index = Cards_In_Hand.IndexOf(CartaDespeje.gameObject);
                    Cards_In_Hand[index] = null;
                    Mazo.listaBooleana[index] = false;
                    posicionesOcupadas_Asedio[i]= true;
                    Efecto_Despeje();
                    ACtualiza_Marcador();
                    break;
                }
                else if( i == 4 )
                {
                    Debug.Log("No se puede convocar");
                }
            }
        }
    }
    // Método para calcular la suma de los puntos de ataque de las cartas en la fila "Cuerpo a Cuerpo"
  public int Cuerpo_A_Cuerpo_Puntos()
{
    int sumaAtaque = 0;
    // Recorre las cartas en la fila "Cuerpo a Cuerpo" y suma sus puntos de ataque
    foreach (var carta in AttackController.Cartas_Fila_Ataque)
    {
        // Verifica que la carta no sea nula
        if (carta != null)
        {
            // Intenta obtener el componente Golden_Card de la carta
            Golden_Card cartaOro = carta.GetComponent<Golden_Card>();
            // Si es una carta Golden_Card, suma sus puntos de ataque
            if (cartaOro != null)
            {
                sumaAtaque += cartaOro.Atk;
            }
            // Si no es una carta Golden_Card, intenta obtener el componente Silver_Card
            else
            {
                Silver_Card cartaPlata = carta.GetComponent<Silver_Card>();
                // Si es una carta Silver_Card, suma sus puntos de ataque
                if (cartaPlata != null)
                {
                    sumaAtaque += cartaPlata.Atk;
                }
            }
        }
    }
    return sumaAtaque;
}
 public int Distancia_Puntos()
{
    int sumaAtaque = 0;
    // Recorre las cartas en la fila "Distancia" y suma sus puntos de ataque
    foreach (var carta in DistanciaController.Cartas_Fila_Distancia)
    {
        // Verifica que la carta no sea nula
        if (carta != null)
        {
            // Intenta obtener el componente Golden_Card de la carta
            Golden_Card cartaOro = carta.GetComponent<Golden_Card>();
            // Si es una carta Golden_Card, suma sus puntos de ataque
            if (cartaOro != null)
            {
                sumaAtaque += cartaOro.Atk;
            }
            // Si no es una carta Golden_Card, intenta obtener el componente Silver_Card
            else
            {
                Silver_Card cartaPlata = carta.GetComponent<Silver_Card>();
                // Si es una carta Silver_Card, suma sus puntos de ataque
                if (cartaPlata != null)
                {
                    sumaAtaque += cartaPlata.Atk;
                }
            }
        }
    }
    return sumaAtaque;
}
 public int Asedio_Puntos()
{
    int sumaAtaque = 0;
    // Recorre las cartas en la fila "Cuerpo a Cuerpo" y suma sus puntos de ataque
    foreach (var carta in AsedioController.Cartas_Fila_Asedio)
    {
        // Verifica que la carta no sea nula
        if (carta != null)
        {
            // Intenta obtener el componente Golden_Card de la carta
            Golden_Card cartaOro = carta.GetComponent<Golden_Card>();
            // Si es una carta Golden_Card, suma sus puntos de ataque
            if (cartaOro != null)
            {
                sumaAtaque += cartaOro.Atk;
            }
            // Si no es una carta Golden_Card, intenta obtener el componente Silver_Card
            else
            {
                Silver_Card cartaPlata = carta.GetComponent<Silver_Card>();
                // Si es una carta Silver_Card, suma sus puntos de ataque
                if (cartaPlata != null)
                {
                    sumaAtaque += cartaPlata.Atk;
                }
            }
        }
    }
    return sumaAtaque;
}
// Metodo para actualizar los puntos del marcador en el tablero
     public int ActualizarTextoSumaAtaque()
    {
        int puntos_Cuerpo_a_Cuerpo = Cuerpo_A_Cuerpo_Puntos();
        int puntos_Distancia = Distancia_Puntos();
        int puntos_Asedio = Asedio_Puntos();

        return puntos_Cuerpo_a_Cuerpo + puntos_Distancia + puntos_Asedio;
    }
     public void ACtualiza_Marcador()
    {
        int puntos_totales = ActualizarTextoSumaAtaque();
        Marcador.text = puntos_totales.ToString();
    }
    public void Actualiza_Marcador_RONDA()
    {
        Marcador_Ronda.text = Rondas_GANADAS.ToString();
    }

     public void Limpia_Tablero()
    {
         // Limpia la fila "Cuerpo a Cuerpo"
    foreach (var carta in AttackController.Cartas_Fila_Ataque)// Este bucle itera sobre cada carta (carta) en la lista Cartas_Fila_Ataque, representa las cartas en la fila "Cuerpo a Cuerpo" del tablero de juego. AttackController es una instancia o una clase que gestiona las cartas en la fila "Cuerpo a Cuerpo".
    {
        if (carta != null)//Esta condición verifica si la carta actual (carta) no es nula
        {
            cementerio.Cartas_en_Cementerio.Add(carta);//agrega la carta (carta) a la lista Cartas_en_Cementerio en el objeto y El objeto cementerio representa el lugar donde se almacenan las cartas jugadas
            Destroy(carta);
        }
    }
    for (int i = 0;i<AttackController.Cartas_Fila_Ataque.Count;i++)
    {
        if(AttackController.Cartas_Fila_Ataque[i]!=null) AttackController.Cartas_Fila_Ataque[i] = null;
    } // Limpia la lista de cartas en la fila "Cuerpo a Cuerpo"

    // Limpia la fila "Distancia"
    foreach (var carta in DistanciaController.Cartas_Fila_Distancia)
    {
        if (carta != null)
        {
            cementerio.Cartas_en_Cementerio.Add(carta);
            carta.transform.position = cementerio.transform.position;
        }
    }
    for (int i = 0;i<DistanciaController.Cartas_Fila_Distancia.Count;i++)
    {
        if(DistanciaController.Cartas_Fila_Distancia[i]!=null) DistanciaController.Cartas_Fila_Distancia[i] = null;
    } // Limpia la lista de cartas en la fila "Cuerpo a Cuerpo"

    // Limpia la fila "Asedio"
    foreach (var carta in AsedioController.Cartas_Fila_Asedio)
    {
        if (carta != null)
        {
            cementerio.Cartas_en_Cementerio.Add(carta);
             carta.transform.position = cementerio.transform.position;
        }
    }
    for (int i = 0;i<AsedioController.Cartas_Fila_Asedio.Count;i++)
    {
        if(AsedioController.Cartas_Fila_Asedio[i]!=null) AsedioController.Cartas_Fila_Asedio[i] = null;
    } // Limpia la lista de cartas en la fila "Cuerpo a Cuerpo"


    // Limpia la fila de Clima (si es necesario)
    foreach (var carta in ClimaController.Cartas_Fila_Clima)
    {
        if (carta != null)
        {
            cementerio.Cartas_en_Cementerio.Add(carta);
             carta.transform.position = cementerio.transform.position;
        }
    }
    for (int i = 0;i<ClimaController.Cartas_Fila_Clima.Count;i++)
    {
        if(ClimaController.Cartas_Fila_Clima[i]!=null) ClimaController.Cartas_Fila_Clima[i] = null;
    } // Limpia la lista de cartas en la fila "Cuerpo a Cuerpo"


    // Limpia la fila de Aumento (si es necesario)
    foreach (var carta in AumentoController.Cartas_Fila_Aumento)
    {
        if (carta != null)
        {
            cementerio.Cartas_en_Cementerio.Add(carta);
            carta.transform.position = cementerio.transform.position;
        }
    }
   for (int i = 0;i<AumentoController.Cartas_Fila_Aumento.Count;i++)
    {
        if(AumentoController.Cartas_Fila_Aumento[i]!=null) AumentoController.Cartas_Fila_Aumento[i] = null;
    } // Limpia la lista de cartas en la fila "Cuerpo a Cuerpo"

    ACtualiza_Marcador();
    }
 public void Efecto_Clima(string fila,int disminuye_puntos ,Weather_Card carta_clima)
       {
         if(fila == "Cuerpo a Cuerpo")
         {
            for(int i = 0 ;i< AttackController.Cartas_Fila_Ataque.Count;i++)
            {
                if(AttackController.Cartas_Fila_Ataque[i] != null)
                {//Le disminuye el ataque a las cartas plata en la fila
                    GameObject carta = AttackController.Cartas_Fila_Ataque[i];
                    Silver_Card plata_afectada = carta.GetComponent<Silver_Card>();
                    if(plata_afectada != null)
                   {
                    plata_afectada.Atk -= disminuye_puntos;
                    carta_clima.cartas_afectadas.Add(plata_afectada);
                   } 
                }   
            }
            for (int i = 0; i < jugador_Rival.AttackController.Cartas_Fila_Ataque.Count;i++)
            {//Recorre la fila Cuerpo a cuerpo del rival
                if(jugador_Rival.AttackController.Cartas_Fila_Ataque[i] != null)
                {//Se disminuye el ataque a las cartas plata en la fila
                   GameObject carta = jugador_Rival.AttackController.Cartas_Fila_Ataque[i];
                   Silver_Card plata_afectada = carta.GetComponent<Silver_Card>();
                   if(plata_afectada != null)
                   {
                     plata_afectada.Atk -= disminuye_puntos;
                      carta_clima.cartas_afectadas.Add(plata_afectada);
                   } 
                }
            }
            //Destroy(EffectMenu);
            ACtualiza_Marcador();
            jugador_Rival.ACtualiza_Marcador();
         }
         else if(fila == "Distancia")
         {
            for(int i = 0;i < DistanciaController.Cartas_Fila_Distancia.Count;i++)
            {//Recorre la fila A Distancia del jugador
                if(DistanciaController.Cartas_Fila_Distancia[i] != null)
                {//Se disminuye el ataque a las cartas plata en la fila
                    GameObject carta = DistanciaController.Cartas_Fila_Distancia[i];
                    Silver_Card plata_afectada = carta.GetComponent<Silver_Card>();
                    if(plata_afectada != null)
                    {
                       plata_afectada.Atk -= disminuye_puntos;
                       carta_clima.cartas_afectadas.Add(plata_afectada);
                    }
                }
            }
            for(int i = 0;i< jugador_Rival.DistanciaController.Cartas_Fila_Distancia.Count;i++)
            {//Recorre la fila A Distancia del rival
               if(jugador_Rival.DistanciaController.Cartas_Fila_Distancia[i] != null)
               {//Se disminuye el ataque de las cartas plata en la fila
                  GameObject carta = jugador_Rival.DistanciaController.Cartas_Fila_Distancia[i];
                  Silver_Card plata_afectada= carta.GetComponent<Silver_Card>();
                  if(plata_afectada != null)
                  {
                    plata_afectada.Atk -= disminuye_puntos;
                    carta_clima.cartas_afectadas.Add(plata_afectada);
                  } 
               }
            }
           // Destroy(EffectMenu);
            ACtualiza_Marcador();
            jugador_Rival.ACtualiza_Marcador();
         }
         else if(fila == "Asedio")
         {
            for(int i =0;i < AsedioController.Cartas_Fila_Asedio.Count;i++)
            {//Recorre la fila Asedio del jugador
                if(AsedioController.Cartas_Fila_Asedio[i] != null)
                {//Se disminuye el ataque de las cartas plata en la fila
                    GameObject carta = AsedioController.Cartas_Fila_Asedio[i];
                    Silver_Card plata_afectada = carta.GetComponent<Silver_Card>();
                    if(plata_afectada != null)
                    {
                       plata_afectada.Atk -= disminuye_puntos;
                       carta_clima.cartas_afectadas.Add(plata_afectada);
                    }
                }
            }
            for(int i =0;i < jugador_Rival.AsedioController.Cartas_Fila_Asedio.Count;i++)
            {//Se recorre la fila Asedio del rival
                if(jugador_Rival.AsedioController.Cartas_Fila_Asedio[i] != null)
                {//Se disminuye el ataque de las cartas plata de la fila
                    GameObject carta = jugador_Rival.AsedioController.Cartas_Fila_Asedio[i];
                    Silver_Card plata_afectada = carta.GetComponent<Silver_Card>();
                    if(plata_afectada != null)
                    {
                       plata_afectada.Atk -= disminuye_puntos;
                       carta_clima.cartas_afectadas.Add(plata_afectada);
                    }
                }
            }
           // Destroy(EffectMenu);
            ACtualiza_Marcador();
            jugador_Rival.ACtualiza_Marcador();
         }
       }
       // efecto cartas aumento
      public void Efecto_Aumento(string fila,int aumenta_puntos)
       {
          if(fila == "Cuerpo a Cuerpo")
          {
            for(int i =0;i<AttackController.Cartas_Fila_Ataque.Count;i++)
            {//Recorre la fila Cuerpo a cuerpo del jugador
                if(AttackController.Cartas_Fila_Ataque[i] != null)
                {//Aumenta el ataque de las cartas de la fila
                  GameObject carta = AttackController.Cartas_Fila_Ataque[i];
                  Silver_Card plata_aumentada = carta.GetComponent<Silver_Card>();
                  if(plata_aumentada != null) plata_aumentada.Atk += aumenta_puntos;
                }
            }
            ACtualiza_Marcador();
          }
          else if(fila == "Distancia")
          {
            for (int i = 0; i < DistanciaController.Cartas_Fila_Distancia.Count; i++)
            {//Recorre la fila A Distancia del jugador
                if(DistanciaController.Cartas_Fila_Distancia[i] != null)
                {//Aumenta el ataque de las cartas de la fila
                    GameObject carta = DistanciaController.Cartas_Fila_Distancia[i];
                    Silver_Card plata_aumentada = carta.GetComponent<Silver_Card>();
                    if(plata_aumentada != null)plata_aumentada.Atk += aumenta_puntos;
                }
            }
            ACtualiza_Marcador();
          }
          else if(fila == "Asedio")
          {
            for(int i = 0; i < AsedioController .Cartas_Fila_Asedio.Count;i++)
            {//Recorre la fila Asedio del jugador
                if(AsedioController.Cartas_Fila_Asedio[i] != null)
                {//AUmenta el ataque de las cartas de la fila
                   GameObject carta = AsedioController.Cartas_Fila_Asedio[i];
                   Silver_Card plata_aumentada = carta.GetComponent<Silver_Card>();
                   if(plata_aumentada != null) plata_aumentada.Atk += aumenta_puntos;
                }
            }
            ACtualiza_Marcador();
          }
       }
    public void Efecto_Despeje()
{
    // Itera a través de cada carta en la fila de Clima
    foreach (var carta in ClimaController.Cartas_Fila_Clima)
    {
        if (carta != null)//si la carta no es nula
        {
            // Obtiene el componente Weather_Card de la carta
            Weather_Card carta_clima = carta.GetComponent<Weather_Card>();
           
            if (carta_clima != null) // Si la carta tiene el componente Weather_Card
            {
                foreach (var carta_afectada in carta_clima.cartas_afectadas) // Itera a través de cada carta afectada por la carta de Clima
                {
                    if (carta_afectada != null)
                    {
                         if(carta_clima.Name == "Tarjeta Roja")
                        {
                            carta_afectada.Atk += 5;
                        }
                        else  if(carta_clima.Name == "Tarjeta Amarilla")
                        {
                            carta_afectada.Atk += 3;
                        }
                         if(carta_clima.Name == "Signal Indiuna Park")
                        {
                            carta_afectada.Atk += 2;
                        }
                        //carta_afectada.Atk += 2;  // Restaura los puntos de ataque que fueron disminuidos por la carta de Clima
                    }
                }
            }
        }
    }
    // Limpia la fila de Clima
    foreach (var carta in ClimaController.Cartas_Fila_Clima)
    {
        if (carta != null)
        {
            cementerio.Cartas_en_Cementerio.Add(carta);// añade la carta al cementerio
            Destroy(carta);//destruye la carta del juego
        }
    }
    ClimaController.Cartas_Fila_Clima.Clear();// limpia la lista de cartas de la fila Clima

    // Actualiza el marcador después de limpiar la fila de Clima
    ACtualiza_Marcador();
} 

    public void Efecto_Messi(string fila,int aumenta_puntos)
       {
          if(fila == "Distancia")
          {
            for(int i =0;i<AttackController.Cartas_Fila_Ataque.Count;i++)
            {//Recorre la fila Cuerpo a cuerpo del jugador
                if(AttackController.Cartas_Fila_Ataque[i] != null)
                {//Aumenta el ataque de las cartas de la fila
                  GameObject carta = AttackController.Cartas_Fila_Ataque[i];
                  Silver_Card plata_aumentada = carta.GetComponent<Silver_Card>();
                  if(plata_aumentada != null) plata_aumentada.Atk += aumenta_puntos;
                }
            }
            ACtualiza_Marcador();
          }
       }

              public void Elimina_Carta_Menor_Atk()//Efecto que elimina la carta con menos atk del rival
       {
        GameObject CardToEliminate = null;
        int lessAtk = int.MaxValue;
        for(int i = 0; i < jugador_Rival.AttackController.Cartas_Fila_Ataque.Count;i++)
        {
            if(jugador_Rival.AttackController.Cartas_Fila_Ataque [i] != null)
            {
                GameObject carta = jugador_Rival.AttackController.Cartas_Fila_Ataque[i];
          Golden_Card cartaOro = carta.GetComponent<Golden_Card>();
          Silver_Card cartaPlata = carta.GetComponent<Silver_Card>();
          if(cartaOro != null)
          {
            if(cartaOro.Atk < lessAtk)
            {
                lessAtk = cartaOro.Atk;
                CardToEliminate = carta;
            }
          }
          else if(cartaPlata != null)
          {
            if(cartaPlata.Atk < lessAtk)
            {
                lessAtk = cartaPlata.Atk;
                CardToEliminate = carta;
            }
          }  
            }
        }
        for(int i = 0; i < jugador_Rival.DistanciaController.Cartas_Fila_Distancia.Count;i++)
        {
            if(jugador_Rival.DistanciaController.Cartas_Fila_Distancia[i]!=null)
            {
                GameObject carta = jugador_Rival.DistanciaController.Cartas_Fila_Distancia[i];
          Golden_Card cartaOro = carta.GetComponent<Golden_Card>();
          Silver_Card cartaPlata = carta.GetComponent<Silver_Card>();
          if(cartaOro != null)
          {
            if(cartaOro.Atk < lessAtk)
            {
                lessAtk = cartaOro.Atk;
                CardToEliminate = carta;
            }
          }
          else if(cartaPlata != null)
          {
            if(cartaPlata.Atk < lessAtk)
            {
                lessAtk = cartaPlata.Atk;
                CardToEliminate = carta;
            }
          }  
            }
        }
        for(int i = 0; i < jugador_Rival.AsedioController.Cartas_Fila_Asedio.Count;i++)
        {
            if(jugador_Rival.AsedioController.Cartas_Fila_Asedio[i] != null)
            {
                GameObject carta = jugador_Rival.AsedioController.Cartas_Fila_Asedio[i];
          Golden_Card cartaOro = carta.GetComponent<Golden_Card>();
          Silver_Card cartaPlata = carta.GetComponent<Silver_Card>();
          if(cartaOro != null)
          {
            if(cartaOro.Atk < lessAtk)
            {
                lessAtk = cartaOro.Atk;
                CardToEliminate = carta;
            }
          }
          else if(cartaPlata != null)
          {
            if(cartaPlata.Atk < lessAtk)
            {
                lessAtk = cartaPlata.Atk;
                CardToEliminate = carta;
            }
          }  
            }
        }
       
        if(CardToEliminate != null)
        {
          CardToEliminate.transform.position = jugador_Rival.cementerio.transform.position;
          jugador_Rival.cementerio.Cartas_en_Cementerio.Add(CardToEliminate);
          if(jugador_Rival.AttackController.Cartas_Fila_Ataque.Contains(CardToEliminate))
          {
            int index = jugador_Rival.AttackController.Cartas_Fila_Ataque.IndexOf(CardToEliminate);
            jugador_Rival.AttackController.Cartas_Fila_Ataque.RemoveAt(index);
            jugador_Rival.AttackController.Cartas_Fila_Ataque.Insert(index,null);
            posicionesOcupadas_Ataque[index] = false;
            jugador_Rival.ACtualiza_Marcador();
          }
          else if(jugador_Rival.DistanciaController.Cartas_Fila_Distancia.Contains(CardToEliminate))
          {
            int index = jugador_Rival.DistanciaController.Cartas_Fila_Distancia.IndexOf(CardToEliminate);
            jugador_Rival.DistanciaController.Cartas_Fila_Distancia.RemoveAt(index);
            jugador_Rival.DistanciaController.Cartas_Fila_Distancia.Insert(index,null);
            posicionesOcupadas_Distancia[index] = false;
            jugador_Rival.ACtualiza_Marcador();
          } 
          }
          else if(jugador_Rival.AsedioController.Cartas_Fila_Asedio.Contains(CardToEliminate))
          {
            int index = jugador_Rival.AsedioController.Cartas_Fila_Asedio.IndexOf(CardToEliminate);
            jugador_Rival.AsedioController.Cartas_Fila_Asedio.RemoveAt(index);
            jugador_Rival.AsedioController.Cartas_Fila_Asedio.Insert(index,null);
            posicionesOcupadas_Asedio[index] = false;
            jugador_Rival.ACtualiza_Marcador();
          } 
       }

       public int Calcular_Promedio()//Calcula el promedio de las cartas del campo
       {
          int promedio = 0;
          int cartasTotales = 0;
          int ataqueTotal = 0;
          for(int i = 0;i < AttackController.Cartas_Fila_Ataque.Count;i++)
          {
            if(AttackController.Cartas_Fila_Ataque[i] != null)
            {
                GameObject carta = AttackController.Cartas_Fila_Ataque[i];
                Golden_Card oro = carta.GetComponent<Golden_Card>();
                Silver_Card plata = carta.GetComponent<Silver_Card>();
                if(oro != null)
                {
                    cartasTotales++;
                    ataqueTotal += oro.Atk;
                }
                else if(plata != null)
                {
                    cartasTotales++;
                    ataqueTotal += plata.Atk;
                }
            }
          }
           for(int i = 0;i < DistanciaController.Cartas_Fila_Distancia.Count;i++)
          {
            if(DistanciaController.Cartas_Fila_Distancia[i] != null)
            {
                GameObject carta = DistanciaController.Cartas_Fila_Distancia[i];
                Golden_Card oro = carta.GetComponent<Golden_Card>();
                Silver_Card plata = carta.GetComponent<Silver_Card>();
                if(oro != null)
                {
                    cartasTotales++;
                    ataqueTotal += oro.Atk;
                }
                else if(plata != null)
                {
                    cartasTotales++;
                    ataqueTotal += plata.Atk;
                }
            }
          }
           for(int i = 0;i < AsedioController.Cartas_Fila_Asedio.Count;i++)
          {
            if(AsedioController.Cartas_Fila_Asedio[i] != null)
            {
                GameObject carta = AsedioController.Cartas_Fila_Asedio[i];
                Golden_Card oro = carta.GetComponent<Golden_Card>();
                Silver_Card plata = carta.GetComponent<Silver_Card>();
                if(oro != null)
                {
                    cartasTotales++;
                    ataqueTotal += oro.Atk;
                }
                else if(plata != null)
                {
                    cartasTotales++;
                    ataqueTotal += plata.Atk;
                }
            }
          }
          for(int i = 0;i<jugador_Rival.AttackController.Cartas_Fila_Ataque.Count;i++)
          {
            if(jugador_Rival.AttackController.Cartas_Fila_Ataque[i] != null)
            {
                GameObject carta = jugador_Rival.AttackController.Cartas_Fila_Ataque[i];
                Golden_Card oro = carta.GetComponent<Golden_Card>();
                Silver_Card plata = carta.GetComponent<Silver_Card>();
                if(oro != null)
                {
                   cartasTotales++;
                    ataqueTotal += oro.Atk;
                }
                else if(plata != null)
                {
                    cartasTotales++;
                    ataqueTotal += plata.Atk;
                }
            }
          }
          for(int i = 0;i<jugador_Rival.DistanciaController.Cartas_Fila_Distancia.Count;i++)
          {
            if(jugador_Rival.DistanciaController.Cartas_Fila_Distancia[i] != null)
            {
                GameObject carta = jugador_Rival.DistanciaController.Cartas_Fila_Distancia[i];
                Golden_Card oro = carta.GetComponent<Golden_Card>();
                Silver_Card plata = carta.GetComponent<Silver_Card>();
                if(oro != null)
                {
                   cartasTotales++;
                    ataqueTotal += oro.Atk;
                }
                else if(plata != null)
                {
                    cartasTotales++;
                    ataqueTotal += plata.Atk;
                }
            }
          }
          for(int i = 0;i<jugador_Rival.AsedioController.Cartas_Fila_Asedio.Count;i++)
          {
            if(jugador_Rival.AsedioController.Cartas_Fila_Asedio[i] != null)
            {
                GameObject carta = jugador_Rival.AsedioController.Cartas_Fila_Asedio[i];
                Golden_Card oro = carta.GetComponent<Golden_Card>();
                Silver_Card plata = carta.GetComponent<Silver_Card>();
                if(oro != null)
                {
                   cartasTotales++;
                    ataqueTotal += oro.Atk;
                }
                else if(plata != null)
                {
                    cartasTotales++;
                    ataqueTotal += plata.Atk;
                }
            }
          }
          promedio = ataqueTotal/cartasTotales;
          return promedio;
       }

        public void Efecto_Promedio()//Efecto que iguala mis cartas al promedio de las cartas en el campo
       {
         int promedio = Calcular_Promedio();
         for(int i = 0 ; i < jugador_Rival.AttackController.Cartas_Fila_Ataque.Count;i++)
         {
            if(jugador_Rival.AttackController.Cartas_Fila_Ataque[i] != null)
            {
                GameObject carta = jugador_Rival.AttackController.Cartas_Fila_Ataque[i];
                Golden_Card oro = carta.GetComponent<Golden_Card>();
                Silver_Card plata = carta.GetComponent<Silver_Card>();
                if(oro != null) oro.Atk = promedio;
                else if(plata != null) plata.Atk = promedio;
            }
         }
          for(int i = 0 ;i < jugador_Rival.DistanciaController.Cartas_Fila_Distancia.Count;i++)
         {
            if(jugador_Rival.DistanciaController.Cartas_Fila_Distancia[i] != null)
            {
                GameObject carta = jugador_Rival.DistanciaController.Cartas_Fila_Distancia[i];
                Golden_Card oro = carta.GetComponent<Golden_Card>();
                Silver_Card plata = carta.GetComponent<Silver_Card>();
                if(oro != null) oro.Atk = promedio;
                else if(plata != null) plata.Atk = promedio;
            }
         }
          for(int i = 0 ;i < jugador_Rival.AsedioController.Cartas_Fila_Asedio.Count;i++)
         {
            if(jugador_Rival.AsedioController.Cartas_Fila_Asedio[i] != null)
            {
                GameObject carta = jugador_Rival.AsedioController.Cartas_Fila_Asedio[i];
                Golden_Card oro = carta.GetComponent<Golden_Card>();
                Silver_Card plata = carta.GetComponent<Silver_Card>();
                if(oro != null) oro.Atk = promedio;
                else if(plata != null) plata.Atk = promedio;
            }
         }
         jugador_Rival.ACtualiza_Marcador();
}
}