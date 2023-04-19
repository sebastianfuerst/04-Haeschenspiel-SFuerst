using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button rollDiceButton;
    public List<Text> playerScoreTexts;
    public Text winnerText;
    public Image currentPlayerImage;
    public List<Sprite> playerSprites;

    private int[] playerScores;
    private int currentPlayer;
    private int totalPlayers;

    void Start()
    {
        totalPlayers = playerScoreTexts.Count;
        playerScores = new int[totalPlayers];
        currentPlayer = 0;
        rollDiceButton.onClick.AddListener(RollDice);
        UpdateScoreTexts();
        UpdateCurrentPlayerSprite();
    }

    public void RollDice()
    {
        int diceValue = Random.Range(1, 7);
        int targetPlayer = (currentPlayer + 1) % totalPlayers;

        switch (diceValue)
        {
            case 5:
                playerScores[targetPlayer] -= 1;
                break;
            case 6:
                playerScores[currentPlayer] += 2;
                break;
            default:
                if (diceValue == currentPlayer + 1)
                {
                    playerScores[currentPlayer]++;
                }
                break;
        }

        currentPlayer = (currentPlayer + 1) % totalPlayers;
        CheckForWinner();
        UpdateScoreTexts();
        UpdateCurrentPlayerSprite();
    }

    void CheckForWinner()
    {
        for (int i = 0; i < totalPlayers; i++)
        {
            if (playerScores[i] >= 10)
            {
                winnerText.text = "Player " + (i + 1) + " wins!";
                winnerText.gameObject.SetActive(true);
                rollDiceButton.interactable = false;
                return;
            }
        }
    }

    void UpdateScoreTexts()
    {
        for (int i = 0; i < totalPlayers; i++)
        {
            playerScoreTexts[i].text = "Player " + (i + 1) + ": " + playerScores[i];
        }
    }

    void UpdateCurrentPlayerSprite()
    {
        currentPlayerImage.sprite = playerSprites[currentPlayer];
    }
}