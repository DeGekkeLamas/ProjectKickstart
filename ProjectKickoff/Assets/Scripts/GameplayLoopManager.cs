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
    public GameObject buttonDonePlacing;
    public GameObject player;
    public GameObject cameraPanPrefab;
    private GameObject theCameraPrefab;
    public GameObject defaultCamera;
    public CardPickerManager cardPickerManager;
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
                cardPickerManager.ClearCards();
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
                GameManager gamemanager1 = GameManager.instance;
                List<CardBase> CardsInHand = new();
                CardsInHand.AddRange(gamemanager1.cardsInHand);
                foreach (var item in CardsInHand)
                {
                    gamemanager1.RemoveCardFromHand(item);
                }
                buttonDonePlacing.SetActive(false);
                Destroy(theCameraPrefab);
                player.SetActive(true);
                break;
            case GameState.choosingCards:
                player.SetActive(false);
                player.transform.position = player.GetComponent<PlayerController>().spawnPoint;
                Transform placerTransform = CardPlacer.cardContainer.transform;
                for (int i = 0; i < placerTransform.childCount; i++)
                {
                    Destroy(placerTransform.GetChild(i).gameObject);
                }
                buttonDonePicking.SetActive(true);
                cardPickerManager.SpawnCards();
            break;
            case GameState.shop:
            break;
        }
    }
}
