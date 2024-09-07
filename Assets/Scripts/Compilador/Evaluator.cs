using System;
using System.Collections.Generic;
using UnityEngine;

public class Evaluator
{
    private readonly Scope globalScope;

    public Evaluator()
    {
        globalScope = new Scope(null);
    }

    public object Evaluate(Expression expression, Scope scope)
    {
        switch (expression)
        {
            case NumberExpression numberExpr:
                return numberExpr.Value;

            case StringExpression stringExpr:
                return stringExpr.Value;

            case BoolExpression boolExpr:
                return boolExpr.Value;

            case BinaryExpression binaryExpr:
                return EvaluateBinaryExpression(binaryExpr, scope);

            case UnaryExpression unaryExpr:
                return EvaluateUnaryExpression(unaryExpr, scope);

            case GroupingExpresion groupingExpr:
                return Evaluate(groupingExpr.Expression, scope);

            case AssignExpresion assignExpr:
                return EvaluateAssignExpression(assignExpr, scope);

            case VariableExpression variableExpr:
                return scope.GetVariable(variableExpr.Name).Value;

            case FunctionExpression funcExpr:
                return EvaluateFunctionExpression(funcExpr, scope);

            case IfExpression ifExpr:
                return EvaluateIfExpression(ifExpr, scope);

            case WhileExpression whileExpr:
                return EvaluateWhileExpression(whileExpr, scope);

            case ForExpression forExpr:
                return EvaluateForExpression(forExpr, scope);

            case EffectExpression effectExpr:
                return EvaluateEffectExpression(effectExpr, scope);

            case CardExpression cardExpr:
                return EvaluateCardExpression(cardExpr, scope);

            // Aquí puedes añadir más casos según las diferentes expresiones de tu DSL

            default:
                throw new Error($"Unknown expression type: {expression.GetType()}", ErrorType.SemanticError);
        }
    }

    private object EvaluateBinaryExpression(BinaryExpression expr, Scope scope)
    {
        var left = Evaluate(expr.Left, scope);
        var right = Evaluate(expr.Right, scope);

        switch (expr.Symbol.Type)
        {
            case TokenType.Plus:
                return (int)left + (int)right;
            case TokenType.Minus:
                return (int)left - (int)right;
            case TokenType.Multiply:
                return (int)left * (int)right;
            case TokenType.Divide:
                return (int)left / (int)right;
            case TokenType.EqualEqual:
                return left.Equals(right);
            case TokenType.NotEqual:
                return !left.Equals(right);
            case TokenType.Less:
                return (int)left < (int)right;
            case TokenType.LessEqual:
                return (int)left <= (int)right;
            case TokenType.GreaterThan:
                return (int)left > (int)right;
            case TokenType.GreatEqualThan:
                return (int)left >= (int)right;
            // Aquí puedes añadir más operadores según lo necesites
            default:
                throw new Error($"Unknown binary operator: {expr.Symbol.Type}", ErrorType.SemanticError);
        }
    }

    private object EvaluateUnaryExpression(UnaryExpression expr, Scope scope)
    {
        var right = Evaluate(expr.Right, scope);

        switch (expr.Symbol.Type)
        {
            case TokenType.Not:
                return !(bool)right;
            // Aquí puedes añadir más operadores unarios según lo necesites
            default:
                throw new Error($"Unknown unary operator: {expr.Symbol.Type}", ErrorType.SemanticError);
        }
    }

    private object EvaluateAssignExpression(AssignExpresion expr, Scope scope)
    {
        var value = Evaluate(expr.Value, scope);
        scope.AssignVariable(expr.Variable.Name, value);
        return value;
    }

    private object EvaluateFunctionExpression(FunctionExpression expr, Scope scope)
    {
        // Aquí puedes manejar la evaluación de funciones definidas por el usuario en tu DSL
        // Por ejemplo, podrías evaluar una función como "Find" o "Shuffle" en el contexto de GWENT
        return null;
    }

    private object EvaluateIfExpression(IfExpression expr, Scope scope)
    {
        var condition = (bool)Evaluate(expr.Condition, scope);
        if (condition)
        {
            return Evaluate(expr.ThenBranch, scope);
        }
        else if (expr.ElseBranch != null)
        {
            return Evaluate(expr.ElseBranch, scope);
        }
        return null;
    }

    private object EvaluateWhileExpression(WhileExpression expr, Scope scope)
    {
        while ((bool)Evaluate(expr.Condition, scope))
        {
            Evaluate(expr.Body, scope);
        }
        return null;
    }

    private object EvaluateForExpression(ForExpression expr, Scope scope)
    {
        var iterable = (IEnumerable<object>)Evaluate(expr.Target, scope);
        foreach (var item in iterable)
        {
            scope.AssignVariable(expr.Variable.Name, item);
            Evaluate(expr.Body, scope);
        }
        return null;
    }

    private object EvaluateEffectExpression(EffectExpression expr, Scope scope)
    {
        // Aquí podrías implementar la evaluación de los efectos en el contexto de GWENT
        return null;
    }

    private object EvaluateCardExpression(CardExpression expr, Scope scope)
    {
        // Aquí podrías implementar la evaluación de las cartas en el contexto de GWENT
        return null;
    }
}
