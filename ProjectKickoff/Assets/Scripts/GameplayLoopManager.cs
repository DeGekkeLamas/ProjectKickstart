using JetBrains.Annotations;
using System.Linq.Expressions;
using UnityEngine;

public class GameplayLoopManager : MonoBehaviour
{
    public static GameplayLoopManager instance;
    void Awake()
    {
        instance = this;
    }
    public enum GameState
    {
        startGame,
        turorial,
        startingDeck,
        placingCards,
        platforming,
        choosingCards,
        shop,
    }

    public GameState currentState;

    public GameState GetState()
    {
        return currentState;
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
        InitiateState(newState);
    }

    private void InitiateState(GameState newState)
    {
        switch (newState)
        {
            case GameState.startGame:
            break;
            case GameState.turorial:
            break;
            case GameState.startingDeck:
            break;
            case GameState.placingCards:
            break;
            case GameState.platforming:
            break;
            case GameState.choosingCards:
            break;
            case GameState.shop:
            break;
        }
    }
}
