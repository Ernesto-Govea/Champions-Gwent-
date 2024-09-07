using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System;


public class Error : Exception
{
    public string message { get; private set; }
    public ErrorType errorType{ get; private set; }  


public Error( string message, ErrorType errorType)
{
    this.message = message;
    this.errorType = errorType;

}

 public string Report()
 {
    return $"{errorType} Error : {message}";

 }

}

public enum ErrorType
{
     LexicalError, // Ocurren cuando el Lexer encuentra un token invalido
    SyntaxError, //Ocurren cuando el parser encuentra una expresion invalida
    SemanticError //Ocurren cuando el evaluador encuentra una expresion invalida
}
