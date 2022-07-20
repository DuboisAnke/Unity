using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region variables
    public FieldScriptableObject fieldScriptableObject;
    public int amountToWin;
    public int rowAmount;
    public int colAmount;
    public int[,] fieldArray;
    public GameObject playerCoin;
    public GameObject opponentCoin;
    public GameObject field;
    public GameObject turnPiece;
    public GameObject fieldParent;
    public GameObject coinStack;

    bool isPlayerTurn = true;
    bool isOpponentTurn = false;
    bool isCheckingForWinner = false;
    bool isDroppping = false;
    public bool playingGame = false;
    public bool gameOver = false;

    public Button restart;
    public GameObject endScreen;

    #endregion

    void Start()
    {
        // initialisation of the field and the coinstack for the player
        Init(fieldScriptableObject);
        // we add a listener on the restart button so we load the scene in again to start clean
        restart.onClick.AddListener(() => Restart());
    }

    void Update()
    {
        // this is just a helper function to see whats in the 2d array
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowArray();
        }

        // this sets the endscreen to active when a party has won.
        if (gameOver)
        {
            endScreen.gameObject.SetActive(true);
        }

    }

    void Init(FieldScriptableObject fieldScriptableObject)
    {
        // we're playing the game now!
        playingGame = true;
        gameOver = false;
        endScreen.gameObject.SetActive(false);

        // we're setting our properties for our field from our scriptableObject
        amountToWin = fieldScriptableObject.amountToWin;
        rowAmount = fieldScriptableObject.rowAmount;
        colAmount = fieldScriptableObject.colAmount;

        // we also make the 2d array here, we'll fill it in the next part
        fieldArray = new int[colAmount, rowAmount];

        // we fill the fieldparent with the correct amount of fields and fill the 2d array with default 'empty' values 0
        field = fieldScriptableObject.field;
        if (field != null)
        {
            for (int x = 0; x < colAmount; x++)
            {
                for (int y = 0; y < rowAmount; y++)
                {


                    GameObject spawnedField = Instantiate(field, new Vector3(x, y * -1, -1), Quaternion.identity);
                    spawnedField.gameObject.name = x + "/" + y;
                    //Debug.LogError(x + " " + y);
                    fieldArray[x, y] = 0;

                    spawnedField.transform.parent = fieldParent.transform;
                }
            }
        }

        // we instantiate the coinstack in the correct position as well
        GameObject stack = Instantiate(coinStack, new Vector3(colAmount + 1, -rowAmount + 0.5f, 0.25f), Quaternion.identity);
        stack.name = "CoinStack";
        Camera.main.transform.position = new Vector3((colAmount - 1) / 2.0f, -((rowAmount - 1) / 2.0f), Camera.main.transform.position.z);


    }

    // helper function
    void ShowArray()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rowAmount; i++)
        {
            for (int j = 0; j < colAmount; j++)
            {
                sb.Append(fieldArray[j, i]);
                sb.Append(' ');
            }
            sb.AppendLine();
        }
        Debug.LogError(sb.ToString());
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
