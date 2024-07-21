using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class _BFS
{
    public class Point
    {
        public int RowIndex;
        public int ColIndex;
        public Point Father;

        public Point(int row, int col)
        {
            this.RowIndex = row;
            this.ColIndex = col;
        }

        public Point(int row, int col, Point father)
        {
            this.RowIndex = row;
            this.ColIndex = col;
            this.Father = father;
        }
    }

    public int RowCount;
    public int ColCount;

    public Dictionary<string, Point> finds;

    public _BFS(int rowCount, int colCount)
    {
        RowCount = rowCount;
        ColCount = colCount;
        this.finds = new Dictionary<string, Point>();
    }

    /// <summary>
    /// 搜索行走区域
    /// </summary>
    /// <param name="row">起点行坐标</param>
    /// <param name="col">起点列坐标</param>
    /// <param name="step">步数</param>
    /// <returns></returns>
    public List<Point> Search(int row, int col, int step)
    {
        //定义搜索集合
        List<Point> searchList = new List<Point>();
        //开始点
        Point startPoint = new Point(row, col);
        //将开始点存到集合中
        searchList.Add(startPoint);
        //开始点默认已经找到，存储到已经找到字典中
        this.finds.Add($"{row}_{col}", startPoint);

        //遍历步数，相当于可搜索的次数
        for (int i = 0; i < step; i++)
        {
            //定义一个临时的集合，用于存储目前找到满足条件的点
            List<Point> tempList = new List<Point>();
            //遍历搜索集合
            for (int j = 0; j < searchList.Count; j++)
            {
                Point current = searchList[j];
                //查找当前点周围的点
                FindAroundPoints(current, tempList);
            }

            if (tempList.Count == 0)
            {
                break;
            }

            //搜索的集合要清空
            searchList.Clear();
            //将临时集合的点添加到搜索集合
            searchList.AddRange(tempList);
        }

        return finds.Values.ToList();
    }

    public void FindAroundPoints(Point current, List<Point> tempList)
    {
        //上
        if (current.RowIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex - 1, current.ColIndex, current, tempList);
        }
        //下
        if (current.RowIndex + 1 < RowCount)
        {
            AddFinds(current.RowIndex + 1, current.ColIndex, current, tempList);
        }
        //左
        if (current.ColIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex, current.ColIndex - 1, current, tempList);
        }
        //右
        if (current.ColIndex + 1 < ColCount)
        {
            AddFinds(current.RowIndex, current.ColIndex + 1, current, tempList);
        }
    }

    public void AddFinds(int row, int col, Point father, List<Point> tempList)
    { 
        if (finds.ContainsKey($"{row}_{col}") == false && GameApp.MapManager.GetBlockType(row, col) != BlockType.Obstacle) 
        {
            Point p = new Point(row, col, father);

            finds.Add($"{row}_{col}", p);

            tempList.Add(p);
        }
    }

    //寻找可移动的点 离终点最近的点的路径集合
    public List<Point> FindMinPath(ModelBase model, int step, int endRowIndex, int endColIndex)
    {
        List<Point> resultList = Search(model.RowIndex, model.ColIndex, step);
        if (resultList.Count == 0)
        {
            return null;
        }
        else
        {
            Point minPoint = resultList[0];
            int min_dis = Mathf.Abs(minPoint.RowIndex - endRowIndex) + Mathf.Abs(minPoint.ColIndex - endColIndex);
            for (int i = 1;i < resultList.Count;i++)
            {
                int tmp_dis = Mathf.Abs(resultList[i].RowIndex - endRowIndex) + Mathf.Abs(resultList[i].ColIndex - endColIndex);
                if (tmp_dis < min_dis)
                {
                    min_dis = tmp_dis;
                    minPoint = resultList[i];
                }
            }
            List<Point> pathList = new List<Point>();
            Point current = minPoint.Father;
            pathList.Add(minPoint);
            while (current != null)
            {
                pathList.Add(current);
                current = current.Father;
            }
            
            pathList.Reverse();
            return pathList;
        }
    }
}
