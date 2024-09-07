using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Scope : MonoBehaviour
{
    public Dictionary<string, Card> cards = new Dictionary<string, Card>();

    public GameManager gameManager;
    public Dictionary<string, EffectExpression> effects = new Dictionary<string, EffectExpression>();
    public Dictionary<string, object> value = new Dictionary<string, object>();


    public void MostrarError(string error)
    {
        PlayerPrefs.SetString("ErrorMessage", error);
        SceneManager.LoadScene("Error");
        AudioListener[] listeners = UnityEngine.Object.FindObjectsOfType<AudioListener>();

        // Desactiva todos los Audio Listeners excepto el principal
        foreach (AudioListener listener in listeners)
        {
            if (listener.gameObject.CompareTag("MainAudioListener"))
            {
                listener.enabled = true;
            }
            else
            {
                listener.enabled = false;
            }
        }
    }
    public void PushCard(string value, Card card)
    {
        if (cards.ContainsKey(value))
        {

        }
        cards[value] = card;
    }
    public void PushEffect(string value, EffectExpression effect)
    {
        if (effects.ContainsKey(value))
        {
            MostrarError($"Ya existe una effecto con este nombre:{value}");
        }
        else
        {
            effects[value] = effect;
        }
    }

    public EffectExpression isEffect(string value)
    {
        if (effects.ContainsKey(value))
        {
            return effects[value];
        }
        else
        {
            MostrarError($"No existe un efecto con este nombre");
            throw new Exception("ddkv");
        }
    }

    public Card GetCard(string key)
    {
        if (value.TryGetValue(key, out var cardObj) && cardObj is Card card)
        {
            return card;
        }
        Debug.LogWarning($"No se encontró una carta válida para la clave: {key}");
        return null;
    }
}
