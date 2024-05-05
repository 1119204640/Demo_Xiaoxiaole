using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using static BoardModel;

public class BoardView : MonoBehaviour
{
    BoardModel board;
    BoardView view;

    GameObject[][] virtualBoard;

    void Awake() {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        board = new BoardModel();
        view = gameObject.GetComponent<BoardView>();
        board.InitBoard();
        view.DrawBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawBoard() {
        cellType[][] temp = board.cells;
        GameObject item = null;
        for (int i = 0; i < board.rowNum + 1; i++) {
            for (int j = 0; j < board.colNum; j++) {
                cellType cell = temp[i][j];
                switch (cell) {
                    case cellType.A:
                    item = Instantiate(Resources.Load("Prefabs/1Tomato_Item")) as GameObject;
                        break;
                    case cellType.B:
                    item = Instantiate(Resources.Load("Prefabs/2Orange_Item")) as GameObject;
                        break;
                    case cellType.C:
                    item = Instantiate(Resources.Load("Prefabs/3Grape_Item")) as GameObject;
                        break;
                    default:
                        break;
                }
                // Debug.Log($"{cell}, ({i},{j})");
                if (item) {
                    // item.GetComponent<RectTransform>(). = 
                    item.transform.SetParent(this.transform);
                    // Debug.Log($"{board.cellHeight * i}");
                    item.transform.SetPositionAndRotation(new Vector3((board.cellWidth + board.clip) * j, board.cellHeight * (board.rowNum - i), 0), Quaternion.identity);
                    // item.transform.SetPositionAndRotation(new Vector3((board.cellWidth + board.clip) * j, 0, 0), Quaternion.identity);
                    // item.GetComponent<RectTransform>().position = new Vector2((board.cellWidth + board.clip) * j, board.cellHeight * i);
                    Debug.Log($"~~~~~~~~~~~~~~~~~~~~~~~{item.GetComponent<RectTransform>().anchoredPosition}");
                    Debug.Log($"{item.GetComponent<RectTransform>().position}");
                }
                
            }
        }
    }
}
