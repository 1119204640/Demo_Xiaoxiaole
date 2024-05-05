using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using Unity.VisualScripting;

public class BoardModel
{
    public enum cellType {
        Empty = 0, A = 1, B = 2, C = 3, End = 4,
    }
    const int _rowNum = 8;
    const int _colNum = 6;
    cellType[][] _cells = new cellType[_rowNum + 1][];   //行数加1是因为把屏幕底部外的一行看作最后一行
    /*
        eg. -----------
            1,2,1,1,2,3     row0
            3,1,2,3,3,1
            1,2,1,1,2,3
            3,1,2,3,3,1
            1,2,1,1,2,3
            3,1,2,3,3,1
            1,2,1,1,2,3
            3,1,2,3,3,1     rowNum-1
            -----------
            1,2,1,1,2,3     rowNum
    */
    const float _cellWidth = 180f, _cellHeight = 180f, _margin = 0f, _clip = 0f;
    bool _isEmpty = true;    //屏幕内是否为全空
    int _top = _rowNum;    //指向当前最上方的非空行，初始状态指向屏幕外这一行
    bool _isOver = false;    //游戏结束标志

    #region 把私有属性设置为可外部访问的公共属性
    public int rowNum {get => _rowNum;}
    public int colNum {get => _colNum;}
    public cellType[][] cells {
        get => _cells;
    }
    public float cellWidth {get => _cellWidth;}
    public float cellHeight {get => _cellHeight;}
    public float margin {get => _margin;}
    public float clip {get => _clip;}
    public bool isEmpty {get => _isEmpty;}
    public int top {get => _top;}
    public bool isOver {get => _isOver;}
    #endregion
    
    cellType[] CreateRow() {
        cellType[] newRow = new cellType[_colNum];
        for (int col = 0; col < _colNum; col++) {
            // int r = Random.Range(1, (int) cellType.End);
            var rand = new System.Random();
            int r = rand.Next((int)cellType.A, (int)cellType.End);
            newRow[col] = (cellType) r;
        }
        return newRow;
    }

    public void InitBoard() {
        for (int i = 0; i < _rowNum; i++) {    //屏幕内的都初始化为空
        _cells[i] = new cellType[_colNum];
            for (int j = 0; j < _colNum; j++) {
                _cells[i][j] = cellType.Empty;
            }
        }
        cellType[] temp = CreateRow();  //屏幕外一行都随机初始化
        _cells[_rowNum] = temp;
    }

    void UpdateBoardState() {
        for (int i = 0; i < _rowNum; i++) {
            for (int j = 0; j < _colNum; j++) {
                if (_cells[i][j] != cellType.Empty) {
                    _top = i;
                    _isEmpty = false;
                    return;
                }
            }
        }
        _top = _rowNum;
        _isEmpty = true;

        if (_top >= 0) {
            _isOver = false;
        }
        else _isOver = true;
    }
    
    void MoveUpward() { //把所有行上移一行
        int newTop = _top - 1;
        if (newTop < 0) {
            _isOver = true;
        }
        else {
            for (int i = newTop; i < _rowNum; i++) {
                _cells[i] = _cells[i+1];
            }
            _top = newTop;
        }
    }
}
