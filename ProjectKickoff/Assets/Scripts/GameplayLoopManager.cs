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

    public void SetState(int newState)
    {
        
        currentState = (GameState)newState;
        InitiateState((GameState)newState);
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
                GameManager gamemanager = GameManager.instance;
                foreach (var card in gamemanager.cardsInDeck)
                {
                    gamemanager.AddCardToHand(card);
                }
                
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
