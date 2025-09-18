using JetBrains.Annotations;
using NUnit.Framework;
using System.Linq.Expressions;
using UnityEngine;
using System.Collections.Generic;

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

public class GameplayLoopManager : MonoBehaviour
{
    public static GameplayLoopManager instance;
    public GameObject buttonDonePicking;
    public GameObject buttonRerollCards;
    public GameObject buttonDonePlacing;
    public GameObject buttonMakeStartIngDeck;
    public GameObject buttonChangePlacements;
    public GameObject player;
    public GameObject cameraPanPrefab;
    private GameObject theCameraPrefab;
    public GameObject defaultCamera;
    public CardPickerManager cardPickerManager;
    public List<CardPlacer> cardPlacers = new();


    private List<CardBase> cardsToKeepInPlay = new();
    void Awake()
    {
        instance = this;
    }

    public GameState currentState;

    public GameState GetState()
    {
        return currentState;
    }

    public void SetState(int newState)
    {
        if ((GameState)newState == GetState()) return;
        GameState oldstate = GetState();
        currentState = (GameState)newState;
        InitiateState(oldstate, (GameState)newState);
    }

    private void InitiateState(GameState oldState, GameState newState)
    {
        switch (oldState)//reset stuff
        {
            case GameState.startGame:
                buttonMakeStartIngDeck.SetActive(false);
                break;
            case GameState.turorial:
            break;
            case GameState.startingDeck:
                cardPickerManager.ClearCards();
                buttonDonePicking.SetActive(false);
                buttonRerollCards.SetActive(false);
                defaultCamera.SetActive(false);
                break;
            case GameState.placingCards:
                foreach (var item in cardPlacers)
                {
                    Destroy(item.gameObject);
                }
                GameManager gamemanager1 = GameManager.instance;
                List<CardBase> CardsInHand = new();
                CardsInHand.AddRange(gamemanager1.cardsInHand);
                foreach (var item in CardsInHand)
                {
                    gamemanager1.RemoveCardFromHand(item);
                }
                buttonDonePlacing.SetActive(false);
                Destroy(theCameraPrefab);
                break;
            case GameState.platforming:
                buttonChangePlacements.SetActive(false);
                player.SetActive(false);
                player.transform.position = player.GetComponent<PlayerController>().spawnPoint;
                if (CardPlacer.cardContainer != null)
                {
                    Transform placerTransform = CardPlacer.cardContainer.transform;
                    for (int i = 0; i < placerTransform.childCount; i++)
                    {
                        if (newState == GameState.placingCards)
                        {
                            Transform cardTransform = placerTransform.GetChild(i);
                            CardBase cardbase = cardTransform.GetComponent<CardBase>();
                            print(cardbase);
                            cardsToKeepInPlay.Add((CardBase)cardbase);
                        }
                        else
                        {
                            Destroy(placerTransform.GetChild(i).gameObject);
                        }
                    }
                }
                
                break;
            case GameState.choosingCards:
                
                cardPickerManager.ClearCards();
                buttonDonePicking.SetActive(false);
                defaultCamera.SetActive(false);
            break;
            case GameState.shop:
            break;
        }


        switch (newState)//initiate stuff
        {
            case GameState.startGame:
                break;
            case GameState.turorial:
                break;
            case GameState.startingDeck:
                buttonDonePicking.SetActive(true);
                buttonRerollCards.SetActive(true);
                GameManager.instance.CollectedCoins = 7;
                cardPickerManager.SpawnCards();
                defaultCamera.SetActive(true);
                break;
            case GameState.placingCards:
                GameManager gamemanager = GameManager.instance;
                foreach (var card in gamemanager.cardsInDeck)
                {
                    if (oldState == GameState.platforming && card.ListContainsMatchingType(cardsToKeepInPlay, card, out CardBase match))
                    {
                        cardsToKeepInPlay.Remove(match);
                    }
                    else
                    {
                        gamemanager.AddCardToHand(card);
                    }
                }
                cardsToKeepInPlay.Clear();
                buttonDonePlacing.SetActive(true);
                theCameraPrefab = Instantiate(cameraPanPrefab, player.transform.position, Quaternion.identity, transform);
                break;
            case GameState.platforming:
                player.SetActive(true);
                buttonChangePlacements.SetActive(true);
                break;
            case GameState.choosingCards:
                GameManager.instance.CollectedCoins++;
                buttonDonePicking.SetActive(true);
                cardPickerManager.SpawnCards();
                defaultCamera.SetActive(true);
                break;
            case GameState.shop:
                break;
        }
    }
}

