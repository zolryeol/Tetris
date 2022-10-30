using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 중복없는 랜덤을 만들자.
/// </summary>
public class NoRedundancyRandom
{
    public NoRedundancyRandom() // 생성자
    {
        qTetrominoSet = new Queue<int>();
    }

    public Queue<int> qTetrominoSet; //7개를 담을것

    public void InitNRDDCRandom()
    {
    }

    void CopyQQueue(ref Queue<Queue<int>> QQa, ref Queue<Queue<int>> QQb /* 비어있다고 가정*/)
    {
        foreach (var Qa in QQa)
        {
            Queue<int> _newQ = new Queue<int>();

            QQb.Enqueue(Qa);
        }
    }

    void CopyQueue(ref Queue<int> oriQ, ref Queue<int> destQ)     // 큐 2개 전제
    {
        for (int i = 0; i < oriQ.Count; ++i)
        {
            int _nowA = oriQ.Dequeue();

            destQ.Enqueue(_nowA);  // 복사가 일어난다 _nowA가 int 이기 때문에
        }
    }

    // 중복없는 랜덤
    public Queue<int> NRDDCRANDOM(int _rangeMiniMum, int _rangeMax)    //중복없는 랜덤
    {
        while (qTetrominoSet.Count < 7) // 큐안에 있는 개수가 7개가 될때까지 반복할 것이다.
        {
            int _num = Random.Range(_rangeMiniMum, _rangeMax + 1);              // 랜덤으로 숫자를 뽑는다.
            if (qTetrominoSet.Contains(_num) == false)    // 뽑은숫자가 큐에 존재하지 않는 다면  큐에 넣는다.
            {
                qTetrominoSet.Enqueue(_num);
            }
        }
        return qTetrominoSet;
    }

//     public Queue<Queue<int>> FillPocket()
//     {
//          QSet.Enqueue(NRDDCRANDOM(0, 6));
//          return QSet;
//     }

//     public NoRedundancyRandom DeepCopy()
//     {
//          NoRedundancyRandom noreRandom = new NoRedundancyRandom();
//          noreRandom.qTetrominoSet = new Queue<int>(this.qTetrominoSet);
//          noreRandom.QSet = new Queue<Queue<int>>(this.QSet);
//          return noreRandom;
//     }
}

