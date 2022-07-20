using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPieceController : MonoBehaviour
{
    #region variables
    FieldScriptableObject fieldScriptableObject;
    GameObject hitObject;
    RaycastHit hit;
    GameObject spawnedPiece;
    public GameObject playerCoin;
    GameManager manager;
    bool coinIsPlaced = false;
    int colPosition;
    int rowPosition;
    #endregion

    // Update is called once per frame
    void Update()
    {
        // if the player clicks we use a raycast to check whether or not the user clicked the appropriate coinstack
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                hitObject = hit.transform.gameObject;

                // if the object hit by the raycast is the correct coinstack we can spawn a piece, else, the spawnedpiece is null
                if (hitObject.name == "StackCollider")
                {
                    spawnedPiece = SpawnPiece();
                    coinIsPlaced = false;
                }
            }
        }

        // if the spawnedpiece isnt empty we activate the function to drag the piece around
        if (Input.GetMouseButton(0))
        {
            if (spawnedPiece != null)
            {
                DragPiece(spawnedPiece);

            }
        }

        // on release of the mousebutton, if there is a spawnedpiece, look for a free spot, drop it, then 
        if (Input.GetMouseButtonUp(0))
        {
            if (spawnedPiece != null)
            {
                colPosition = Mathf.RoundToInt(spawnedPiece.transform.position.x);
                manager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

                if (colPosition <= 6 && colPosition >= 0)
                {
                    for (int row = manager.rowAmount - 1; row >= 0; row--)
                    {
                        if (!Physics.CheckSphere(new Vector3(colPosition, -row, 0.25f), 0.2f) && coinIsPlaced == false)
                        {
                            spawnedPiece.transform.position = new Vector3(colPosition, 1, 0.25f);

                            Vector3 pos = new Vector3(colPosition, -row, 0.25f);
                            iTween.MoveTo(spawnedPiece, iTween.Hash(
                                "position", pos,
                                "time", 1.5f,
                                "easetype", iTween.EaseType.easeOutBounce));
                            coinIsPlaced = true;
                            if (coinIsPlaced)
                            {
                                manager.fieldArray[colPosition, row] = 1;
                                row = rowPosition;
                                BoardEvaluator.EvaluationResult result = BoardEvaluator.Evaluate(manager.fieldArray, 1, manager.amountToWin);
                                if (result.hasXInARow)
                                {
                                    manager.playingGame = false;
                                    manager.gameOver = true;
                                    spawnedPiece = null;
                                }
                            }
                        }
                    }
                    spawnedPiece = null;
                }
                else
                {
                    Vector3 mousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.5f);
                    Vector3 cursorPos = Camera.main.ScreenToWorldPoint(mousePoint);
                    spawnedPiece.transform.position = cursorPos;
                }
            }

        }
    }

    GameObject SpawnPiece()
    {
        Vector3 spawnPosition = GameObject.Find("CoinStack").transform.position;

        GameObject piece = Instantiate(playerCoin, spawnPosition, Quaternion.identity);
        piece.name = "YellowPiece";
        return piece;
    }

    void DragPiece(GameObject pieceToDrag)
    {
        Vector3 mousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.5f);
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(mousePoint);
        pieceToDrag.transform.position = cursorPos;
    }


}
