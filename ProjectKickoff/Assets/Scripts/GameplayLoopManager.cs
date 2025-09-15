using JetBrains.Annotations;
using System.Linq.Expressions;
using UnityEngine;

public class GameplayLoopManager : MonoBehaviour
{
    public static GameplayLoopManager instance;
    public GameObject buttonDonePicking;
    public GameObject buttonDonePlacing;
    public GameObject player;
    public GameObject cameraPanPrefab;
    private GameObject theCameraPrefab;
    public GameObject defaultCamera;
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
                buttonDonePicking.SetActive(false);
                buttonDonePlacing.SetActive(true);
                theCameraPrefab = Instantiate(cameraPanPrefab, player.transform.position, Quaternion.identity, transform);
                defaultCamera.SetActive(false);
                break;
            case GameState.platforming:
                Destroy(theCameraPrefab);
                defaultCamera.SetActive(true);
                player.SetActive(true);
                break;
            case GameState.choosingCards:
            break;
            case GameState.shop:
            break;
        }
    }
}
