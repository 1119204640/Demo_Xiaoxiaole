using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BoardView : MonoBehaviour
{
    enum cellType {
        Empty = 0, A = 1, B = 2, C = 3, End = 4
    }

    GameObject cell;
    float cellWidth, cellHeight, margin = 0f, clip = 0f;
    cellType[,] cells;
    bool isEmpty = true;
    int top = 0;    //指向当前最上方的非空行
    const int rowNum = 8;
    const int colNum = 6;
    void Awake() {
        cells = new cellType[rowNum, colNum];
        cell = Resources.Load("Prefabs/1Tomato_Item") as GameObject; //设置一个默认的item
        cellWidth = cell.GetComponent<RectTransform>().rect.width;
        cellHeight = cell.GetComponent<RectTransform>().rect.height;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitBoard() {
        for (int i = 0; i < rowNum; i++) {
            for (int j = 0; j < colNum; j++) {
                cells[i,j] = cellType.Empty;
            }
            CreateRow(i);
        }
        // CreateAndMoveUp();
        
    }

    cellType[] CreateRow(int i) {  //在屏幕底部外创建一行
        cellType[] newRow = new cellType[colNum];
        GameObject cell = null;
        for (int col = 0; col < colNum; col++) {
            int r = Random.Range(1, (int) cellType.End);
            switch (r) {
                case 1:
                    cell = Resources.Load("Prefabs/1Tomato_Item") as GameObject;
                    break;
                case 2:
                    cell = Resources.Load("Prefabs/2Orange_Item") as GameObject;
                    break;
                case 3:
                    cell = Resources.Load("Prefabs/3Grape_Item") as GameObject;
                    break;
                default:
                    break;
            }

            GameObject temp = Instantiate(cell);
            temp.transform.SetParent(transform);
            temp.transform.SetLocalPositionAndRotation(new Vector3((cellWidth + clip) * col  + margin, cellHeight * i, 0), Quaternion.identity);
            newRow[col] = (cellType) r;
        }
        return newRow;
    }

    void CreateAndMoveUp() {
        cellType[] newRow = CreateRow(0);
        Update2DArray(newRow);

    }

    void Update2DArray(cellType[] newRow) {
        CheckBoardState();   //修改了top, isEmpty变量
        if (isEmpty) {
            for (int i = 0; i < colNum; i++) {
                cells[rowNum-1, i] = newRow[i];
            }

        }
        else {

        }


    }

    void CheckBoardState() { //从顶部往下找第一行非空行
        for (int i = rowNum; i >= 1; i--) {
            for (int j = 0; j < colNum; j++) {
                if (cells[rowNum-i,j] != cellType.Empty) {  //注意cells是以左上为起点，但是icon的坐标是以左下为起点
                    top = i;
                    isEmpty = false;
                    return;
                }
            }
        }
        top = 0;
        isEmpty = true;
    }

}
