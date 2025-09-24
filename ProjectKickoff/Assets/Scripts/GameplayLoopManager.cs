using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

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
    public GameObject buttonToggleVisibility;
    public GameObject buttonMakeStartIngDeck;
    public GameObject buttonChangePlacements;
    public GameObject player;
    public GameObject cameraPanPrefab;
    private GameObject theCameraPrefab;
    public GameObject defaultCamera;
    public GameObject resetButton;
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
                CardPickerManager.instance.cardsPickedCounter.transform.parent.gameObject.SetActive(true);
                CardPickerManager.instance.UpdateCardCounter();
                break;
            case GameState.turorial:
            break;
            case GameState.startingDeck:
                cardPickerManager.ClearCards();
                CardPickerManager.instance.cardsPickedCounter.transform.parent.gameObject.SetActive(false);
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
                buttonToggleVisibility.SetActive(false);
                Destroy(theCameraPrefab);
                defaultCamera.SetActive(true);
                resetButton.SetActive(false);
                if (newState != GameState.platforming)
                {
                    if (CardPlacer.cardContainer != null)
                    {
                        Transform placerTransform = CardPlacer.cardContainer.transform;
                        for (int i = 0; i < placerTransform.childCount; i++)
                        {
                            Destroy(placerTransform.GetChild(i).gameObject);
                        }
                    }
                    GameManager.instance.cardsInDeck.Clear();
                    CardPickerManager.instance.cardsPicked = 0;
                    CardPickerManager.instance.UpdateCardCounter();
                }
                break;
            case GameState.platforming:
                defaultCamera.SetActive(true);
                buttonChangePlacements.SetActive(false);
                player.SetActive(false);
                player.transform.position = player.GetComponent<PlayerController>().worldSpawn;
                if (CardPlacer.cardContainer != null)
                {
                    Transform placerTransform = CardPlacer.cardContainer.transform;
                    for (int i = 0; i < placerTransform.childCount; i++)
                    {
                        if (newState == GameState.placingCards)
                        {
                            Transform cardTransform = placerTransform.GetChild(i);
                            CardBase cardbase = cardTransform.GetComponent<CardBase>();
                            //print(cardbase);
                            cardsToKeepInPlay.Add((CardBase)cardbase);
                        }
                        else
                        {
                            Destroy(placerTransform.GetChild(i).gameObject);
                        }
                    }
                }
                if (newState == GameState.startGame)
                {
                    GameManager.instance.cardsInDeck.Clear();
                    CardPickerManager.instance.cardsPicked = 0;
                    CardPickerManager.instance.UpdateCardCounter();
                }
                
                break;
            case GameState.choosingCards:
                cardPickerManager.cardsPicked = 0;
                cardPickerManager.ClearCards();
                CardPickerManager.instance.cardsPickedCounter.transform.parent.gameObject.SetActive(false);
                buttonDonePicking.SetActive(false);
                defaultCamera.SetActive(false);
                buttonRerollCards.SetActive(false);
                break;
            case GameState.shop:
            break;
        }


        switch (newState)//initiate stuff
        {
            case GameState.startGame:
                buttonMakeStartIngDeck.SetActive(true);
                
                break;
            case GameState.turorial:
                break;
            case GameState.startingDeck:
                cardPickerManager.ClearCards();
                CardPickerManager.instance.cardsPickedCounter.transform.parent.gameObject.SetActive(true);
                CardPickerManager.instance.UpdateCardCounter();
                buttonDonePicking.SetActive(true);
                buttonRerollCards.SetActive(true);
                GameManager.instance.UpdateCoinCount(7);
                cardPickerManager.SpawnCards();
                defaultCamera.SetActive(true);
                break;
            case GameState.placingCards:
                defaultCamera.SetActive(false);
                resetButton.SetActive(true);
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
                buttonToggleVisibility.SetActive(true);
                theCameraPrefab = Instantiate(cameraPanPrefab, player.transform.position, Quaternion.identity, transform);
                break;
            case GameState.platforming:
                defaultCamera.SetActive(false);
                player.SetActive(true);
                buttonChangePlacements.SetActive(true);
                break;
            case GameState.choosingCards:
                GameManager.instance.cardsInDeck.Clear();
                GameManager.instance.cardsInHand.Clear();
                GameManager.instance.UpdateCoinCount(GameManager.instance.collectedCoins + 1);
                buttonDonePicking.SetActive(true);
                cardPickerManager.SpawnCards();
                CardPickerManager.instance.cardsPickedCounter.transform.parent.gameObject.SetActive(true);
                CardPickerManager.instance.UpdateCardCounter();
                buttonRerollCards.SetActive(true);
                defaultCamera.SetActive(true);
                break;
            case GameState.shop:
                break;
        }
    }
}

