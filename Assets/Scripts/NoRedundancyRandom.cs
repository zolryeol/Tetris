using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ߺ����� ������ ������.
/// </summary>
public class NoRedundancyRandom
{
    public NoRedundancyRandom() // ������
    {
        qTetrominoSet = new Queue<int>();
    }

    public Queue<int> qTetrominoSet; //7���� ������

    public void InitNRDDCRandom()
    {
    }

    void CopyQQueue(ref Queue<Queue<int>> QQa, ref Queue<Queue<int>> QQb /* ����ִٰ� ����*/)
    {
        foreach (var Qa in QQa)
        {
            Queue<int> _newQ = new Queue<int>();

            QQb.Enqueue(Qa);
        }
    }

    void CopyQueue(ref Queue<int> oriQ, ref Queue<int> destQ)     // ť 2�� ����
    {
        for (int i = 0; i < oriQ.Count; ++i)
        {
            int _nowA = oriQ.Dequeue();

            destQ.Enqueue(_nowA);  // ���簡 �Ͼ�� _nowA�� int �̱� ������
        }
    }

    // �ߺ����� ����
    public Queue<int> NRDDCRANDOM(int _rangeMiniMum, int _rangeMax)    //�ߺ����� ����
    {
        while (qTetrominoSet.Count < 7) // ť�ȿ� �ִ� ������ 7���� �ɶ����� �ݺ��� ���̴�.
        {
            int _num = Random.Range(_rangeMiniMum, _rangeMax + 1);              // �������� ���ڸ� �̴´�.
            if (qTetrominoSet.Contains(_num) == false)    // �������ڰ� ť�� �������� �ʴ� �ٸ�  ť�� �ִ´�.
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

