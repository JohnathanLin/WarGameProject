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
    /// ������������
    /// </summary>
    /// <param name="row">���������</param>
    /// <param name="col">���������</param>
    /// <param name="step">����</param>
    /// <returns></returns>
    public List<Point> Search(int row, int col, int step)
    {
        //������������
        List<Point> searchList = new List<Point>();
        //��ʼ��
        Point startPoint = new Point(row, col);
        //����ʼ��浽������
        searchList.Add(startPoint);
        //��ʼ��Ĭ���Ѿ��ҵ����洢���Ѿ��ҵ��ֵ���
        this.finds.Add($"{row}_{col}", startPoint);

        //�����������൱�ڿ������Ĵ���
        for (int i = 0; i < step; i++)
        {
            //����һ����ʱ�ļ��ϣ����ڴ洢Ŀǰ�ҵ����������ĵ�
            List<Point> tempList = new List<Point>();
            //������������
            for (int j = 0; j < searchList.Count; j++)
            {
                Point current = searchList[j];
                //���ҵ�ǰ����Χ�ĵ�
                FindAroundPoints(current, tempList);
            }

            if (tempList.Count == 0)
            {
                break;
            }

            //�����ļ���Ҫ���
            searchList.Clear();
            //����ʱ���ϵĵ���ӵ���������
            searchList.AddRange(tempList);
        }

        return finds.Values.ToList();
    }

    public void FindAroundPoints(Point current, List<Point> tempList)
    {
        //��
        if (current.RowIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex - 1, current.ColIndex, current, tempList);
        }
        //��
        if (current.RowIndex + 1 < RowCount)
        {
            AddFinds(current.RowIndex + 1, current.ColIndex, current, tempList);
        }
        //��
        if (current.ColIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex, current.ColIndex - 1, current, tempList);
        }
        //��
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

    //Ѱ�ҿ��ƶ��ĵ� ���յ�����ĵ��·������
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
