using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //マスの配列
    private int [ , ] squares = new int [10, 10];

    private const int EMPTY = 0;
    private const int WHITE = 1;
    private const int BLACK = -1;

    private int currentPlayer = WHITE;

    private Camera camera_object;
    private RaycastHit hit;

    public GameObject whiteStone;
    public GameObject blackStone;

    //ターン表示テキスト
    public Text turn;
 
    //ターン表示の定型文
    private const string whiteTurn = "白のターン";
    private const string blackTurn = "黒のターン";


    // Start is called before the first frame update
    void Start()
    {
        //ターン表示テキストの初期値を代入
        turn.text = whiteTurn;

        camera_object = GameObject.Find("Main Camera").GetComponent<Camera>();

        InitializeArray();

        DebugArray();
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckStone(WHITE) || CheckStone(BLACK)){
            return;
        }

        if(Input.GetMouseButtonDown(0)){

            //マイクのポジションを取得してRayに代入する
            Ray ray = camera_object.ScreenPointToRay(Input.mousePosition);

            //マウスのポジションからrayを投げて当たったらhitに入れる
            if(Physics.Raycast(ray, out hit)){
                int x = (int)hit.collider.gameObject.transform.position.x;
                int z = (int)hit.collider.gameObject.transform.position.z;

                //マスが空の時
                if(squares[z, x] == EMPTY){
                    if(currentPlayer == WHITE){
                        //情報の更新
                        squares[z, x] = WHITE;
                        //Stoneを放出
                        GameObject stone = Instantiate(whiteStone);
                        stone.transform.position = hit.collider.gameObject.transform.position;
                        //playerChange
                        currentPlayer = BLACK;
                        //turnを更新
                        turn.text = blackTurn;
                    }
                    
                    else if(currentPlayer == BLACK){
                        //情報の更新
                        squares[z, x] = BLACK;
                        //Stoneを放出
                        GameObject stone = Instantiate(blackStone);
                        stone.transform.position = hit.collider.gameObject.transform.position;
                        //playerChange
                        currentPlayer = WHITE;
                        //turnを更新
                        turn.text = whiteTurn;
                    }
                }
            }
        }
    }

    //配列の初期化関数
    private void InitializeArray() {
        for(int i = 0; i < 10; i++){
            for(int j = 0; j < 10; j++){
                //全部のマスを空にする
                squares[i, j] = EMPTY;
            }
        }
    }

    //配列情報を確認する（デバッグ用）
    private void DebugArray(){
        for(int i = 0; i < 10; i++){
            for(int j = 0; j < 10; j++){
                Debug.Log("(i,j) = (" + i + ", " + j + ") = " + squares[i, j]);
            }
        }     
    }

    private bool CheckStone(int color){
        //石をカウントする
        int count = 0;

        //横
        for(int i = 0; i < 10; i++){
            for(int j = 0; j < 10; j++){

                //squaresの値が空の時
                if(squares[i, j] == EMPTY || squares[i, j] != color){
                    count = 0;
                } else { //空じゃないとき
                    count++;
                }

                //countが5の時
                if(count == 5){
                    if(color == WHITE){
                        Debug.Log("白の勝ち");
                    } else {
                        Debug.Log("黒の勝ち");
                    }

                    return true;
                }
            }
        }

        //縦
        count = 0;
        for(int i = 0; i < 10; i++){
            for(int j = 0; j < 10; j++){

                //squaresの値が空の時
                if(squares[j, i] == EMPTY || squares[j, i] != color){
                    count = 0;
                } else { //空じゃないとき
                    count++;
                }

                //countが5の時
                if(count == 5){
                    if(color == WHITE){
                        Debug.Log("白の勝ち");
                    } else {
                        Debug.Log("黒の勝ち");
                    }

                    return true;
                }
            }
        }

        //斜め(右上がり)
        count = 0;
        for(int i = 0; i < 10; i++){

            int up = 0;

            for(int j = i; j < 10; j++){

                //squaresの値が空の時
                if(squares[j, up] == EMPTY || squares[j, up] != color){
                    count = 0;
                } else { //空じゃないとき
                    count++;
                }

                //countが5の時
                if(count == 5){
                    if(color == WHITE){
                        Debug.Log("白の勝ち");
                    } else {
                        Debug.Log("黒の勝ち");
                    }

                    return true;
                }

                up++;
            }
        }

        //斜め(右下がり)
        count = 0;
        for(int i = 0; i < 10; i++){

            int down = 9;

            for(int j = i; j < 10; j++){

                //squaresの値が空の時
                if(squares[j, down] == EMPTY || squares[j, down] != color){
                    count = 0;
                } else { //空じゃないとき
                    count++;
                }

                //countが5の時
                if(count == 5){
                    if(color == WHITE){
                        Debug.Log("白の勝ち");
                    } else {
                        Debug.Log("黒の勝ち");
                    }

                    return true;
                }

                down--;
            }
        }
        //判定が出ないとき
        return false;
    }

}
