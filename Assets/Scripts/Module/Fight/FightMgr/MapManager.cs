using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//��ͼ������ �����ͼ��������Ϣ
public class MapManager
{
    private Tilemap tileMap;

    public Block[,] mapArr;

    public int RowCount;
    public int ColCount;

    public void Init()
    {
        tileMap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();

        //��ͼ��С
        RowCount = 12;
        ColCount = 20;

        mapArr = new Block[RowCount, ColCount];

        List<Vector3Int> tmpPosArr = new List<Vector3Int>(); //��ʱ��¼��Ƭ��ͼÿ�����ӵ�λ��

        foreach (var pos in tileMap.cellBounds.allPositionsWithin)
        {
            if (tileMap.HasTile(pos))
            {
                tmpPosArr.Add(pos);
            }
        }

        //��һά�����λ��ת���ɶ�ά�����Block ���д洢
        Object prefabObj = Resources.Load("Model/block");
        for (int i = 0; i < tmpPosArr.Count; i++)
        {
            int row = i / ColCount;
            int col = i % ColCount;

            Block b = (Object.Instantiate(prefabObj) as GameObject).AddComponent<Block>(); //?

            b.RowIndex = row;
            b.ColIndex = col;

            b.transform.position = tileMap.CellToWorld(tmpPosArr[i]) + new Vector3(0.5f, 0.5f, 0); //?
            mapArr[row, col] = b;
        }
    }
}
