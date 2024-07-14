using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//·������
public class AStarPoint
{
    public int RowIndex;
    public int ColIndex;
    public int G; //��ǰ�㵽��ʼ��ľ���
    public int H; //��ǰ�㵽�յ�ľ���
    public int F; //F = H + G
    public AStarPoint Parent; //�ҵ���ǰ��ĸ��ڵ�

    public AStarPoint(int row, int col)
    {
        this.RowIndex = row;
        this.ColIndex = col;
        this.Parent = null;
    }

    public AStarPoint(int row, int col, AStarPoint parent)
    {
        this.RowIndex = row;
        this.ColIndex = col;
        this.Parent = parent;
    }

    public int GetG() 
    {
        int _g = 0;
        AStarPoint parent = this.Parent;
        while (parent != null)
        {
            _g = _g + 1;
            parent = parent.Parent;
        }
        return _g;
    }

    public int GetH(AStarPoint end)
    {
        return Mathf.Abs(RowIndex - end.RowIndex) + Mathf.Abs(ColIndex - end.ColIndex);
    }
}

public class AStar
{
    private int rowCount;
    private int colCount;
    private List<AStarPoint> open; //open��
    private Dictionary<string, AStarPoint> close; //close�� �Ѿ����ҹ���·�����浽�����
    private AStarPoint start; //��ʼ��
    private AStarPoint end; //�յ�
    public AStar(int rowCount, int colCount)
    {
        this.rowCount = rowCount;
        this.colCount = colCount;
        open = new List<AStarPoint> ();
        close = new Dictionary<string, AStarPoint> ();

    }

    //�ҵ���open��·����
    public AStarPoint IsInOpen(int rowIndex, int colIndex)
    {
        for (int i = 0; i < open.Count; i++)
        {
            if (open[i].RowIndex == rowIndex && open[i].ColIndex == colIndex)
            {
                return open[i];
            }
        }
        return null;
    }

    //ĳ�����Ƿ��Ѿ�����close��
    public bool IsInClose(int rowIndex, int colIndex)
    {
        return close.ContainsKey($"{rowIndex}_{colIndex}");
    }

    /*
     * A��˼·
     * 1.�������ӵ�open��
     * 2.����open���� fֵ��С��·����
     * 3.���ҵ���Сfֵ�ĵ��open�����Ƴ�������ӵ�close��
     * 4.����ǰ��·������Χ�ĵ���ӵ�open���������ҵĵ㣩
     * 5.�ж��յ��Ƿ���open���У�������� �Ӳ���2����ִ���߼�
     */
    public bool FindPath(AStarPoint start, AStarPoint end, System.Action<List<AStarPoint>> findCallBack)
    {
        this.start = start;
        this.end = end;
        open = new List<AStarPoint>();
        close = new Dictionary<string, AStarPoint>();

        //1.�����ӵ�open��
        open.Add(start);
        while (true)
        {
            //2.��open���л��Fֵ��С��·����
            AStarPoint current = GetMinFFromInOpen();
            if (current == null)
            {
                //û��·��
                return false;
            } else
            {
                //3.1��open�����Ƴ���
                open.Remove(current);
                //3.2��ӵ�close��
                close.Add($"{current.RowIndex}_{current.ColIndex}", current);
                //4.����ǰ·������Χ�ĵ�ӵ�open����
                AddAroundInOpen(current);
                //5.�ж��յ��Ƿ���open��
                AStarPoint endPoint = IsInOpen(end.RowIndex, end.ColIndex);
                if (endPoint != null)
                {
                    //�ҵ�·����
                    findCallBack(GetPath(endPoint));
                    return true;
                }

                //��open������
                open.Sort(OpenSort);
            }
        }
      

    }

    public List<AStarPoint> GetPath(AStarPoint point)
    {
        List<AStarPoint> pathList = new List<AStarPoint> ();
        pathList.Add(point);
        AStarPoint parent = point.Parent;
        while (parent != null)
        {
            pathList.Add(parent);
            parent = parent.Parent;
        }

        //����
        pathList.Reverse();
        return pathList;
    }

    public int OpenSort(AStarPoint a, AStarPoint b)
    {
        return a.F - b.F;
    }

    public void AddAroundInOpen(AStarPoint current)
    {
        //��
        if (current.RowIndex - 1 >= 0)
        {
            AddOpen(current, current.RowIndex - 1, current.ColIndex);
        }
        //��
        if (current.RowIndex + 1 < rowCount)
        {
            AddOpen(current, current.RowIndex + 1, current.ColIndex);
        }
        //��
        if (current.ColIndex - 1 >= 0)
        {
            AddOpen(current, current.RowIndex, current.ColIndex - 1);
        }
        //��
        if (current.ColIndex + 1 < colCount)
        {
            AddOpen(current, current.RowIndex, current.ColIndex + 1);
        }
    }

    public void AddOpen(AStarPoint current, int row, int col)
    {
        if (IsInClose(row, col) == false && IsInOpen(row, col) == null && GameApp.MapManager.GetBlockType(row, col) == BlockType.Null)
        {
            AStarPoint newPoint = new AStarPoint(row, col, current);
            newPoint.G = newPoint.GetG();
            newPoint.H = newPoint.GetH(end);
            newPoint.F = newPoint.G + newPoint.H;
            open.Add(newPoint);
        }
    }

    public AStarPoint GetMinFFromInOpen()
    {
        if (open.Count == 0)
        {
            return null;
        }
        return open[0]; //open������� ��Сfֵ�ڵ�һλ�����Է��ص�һλ��·����
    }

}
