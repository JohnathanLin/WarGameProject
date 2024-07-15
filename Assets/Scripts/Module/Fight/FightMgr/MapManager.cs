using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//格子显示方向的枚举，枚举字符串跟资源图片一致
public enum BlockDirection 
{
    none = -1,
    down,
    horizontal,
    left,
    left_down,
    left_up,
    right,
    right_down,
    right_up,
    up,
    vertical,
    max,
}

//地图管理器 管理地图的网格信息
public class MapManager
{
    private Tilemap tileMap;

    public Block[,] mapArr;

    public int RowCount;
    public int ColCount;

    public List<Sprite> dirSpArr; //存储箭头方向图片的集合

    public void Init()
    {
        dirSpArr = new List<Sprite>();

        for (int i = 0;i < (int)BlockDirection.max; i++)
        {
            dirSpArr.Add(Resources.Load<Sprite>($"Icon/{(BlockDirection)i}"));
        }
        tileMap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();

        //地图大小
        RowCount = 12;
        ColCount = 20;

        mapArr = new Block[RowCount, ColCount];

        List<Vector3Int> tmpPosArr = new List<Vector3Int>(); //临时记录瓦片地图每个格子的位置

        foreach (var pos in tileMap.cellBounds.allPositionsWithin)
        {
            if (tileMap.HasTile(pos))
            {
                tmpPosArr.Add(pos);
            }
        }

        //将一维数组的位置转换成二维数组的Block 进行存储
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

    public BlockType GetBlockType(int row, int col)
    {
        return mapArr[row, col].Type;
    }

    public void ChangeBlockType(int row, int col, BlockType type)
    {
        mapArr[row, col].Type = type;
    }

    //显示移动的区域
    public void ShowStepGrid(ModelBase model, int step)
    {
        _BFS bfs = new _BFS(RowCount, ColCount);
        List<_BFS.Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);

        for (int i = 0; i < points.Count; i++)
        {
            mapArr[points[i].RowIndex, points[i].ColIndex].ShowGrid(Color.blue);
        }
    }

    //隐藏移动的区域
    public void HideStepGrid(ModelBase model, int step) 
    {
        _BFS bfs = new _BFS(RowCount, ColCount);
        List<_BFS.Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);

        for (int i = 0; i < points.Count; i++)
        {
            mapArr[points[i].RowIndex, points[i].ColIndex].HideGrid();
        }
    }

    //根据方向枚举 设置格子的方向图标和颜色
    public void SetBlockDir(int rowIndex, int colIndex, BlockDirection dir, Color color)
    {
        mapArr[rowIndex, colIndex].SetDirSp(dirSpArr[(int)dir], color);
    }
}
