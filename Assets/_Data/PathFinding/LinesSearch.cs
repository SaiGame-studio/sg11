using System.Collections.Generic;
using UnityEngine;

public class LinesSearch : AbstractPathfinding
{
    [Header("Lines Search")]
    public GridManagerCtrl ctrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.parent.GetComponent<GridManagerCtrl>();
        Debug.LogWarning(transform.name + " LoadCtrl", gameObject);
    }

    public override void DataReset()
    {
        this.pathNodes = new List<Node>();
    }

    public override bool FindPath(BlockCtrl startBlock, BlockCtrl targetBlock)
    {
        Node startNode = startBlock.blockData.node;
        Node targetNode = targetBlock.blockData.node;
        this.pathNodes.Add(startNode);

        //Debug.Log("==============================");
        startNode.Dump("startNode: ");
        targetNode.Dump("targetNode: ");

        //Line up
        if (this.IsLineUp(startNode, targetNode) && !this.IsLineBlocked(startNode, targetNode))
        {
            this.pathNodes.Add(targetNode);
            return true;
        }

        //Two cross points
        //Debug.Log("== Two cross points =================");
        List<Vector2> twoCrossPoints = this.GetCrossPoints(startNode, targetNode);
        if (this.IsCrossPointsLinked(startNode, targetNode, twoCrossPoints)) return true;

        List<Vector2> multiCrossPoints;

        //Bellow Cross Point
        //Debug.Log("== Bellow cross points =================");
        multiCrossPoints = this.GetBellowCrossPoints(startNode);
        if (this.CheckMultiCrossPoints(multiCrossPoints, targetNode)) return true;

        //Above Cross Point
        //Debug.Log("== Above cross points =================");
        multiCrossPoints = this.GetAboveCrossPoints(startNode);
        if (this.CheckMultiCrossPoints(multiCrossPoints, targetNode)) return true;

        //Right Cross Point
        //Debug.Log("== Right cross points =================");
        multiCrossPoints = this.GetRightCrossPoints(startNode);
        if (this.CheckMultiCrossPoints(multiCrossPoints, targetNode)) return true;

        //Left Cross Point
        //Debug.Log("== Left cross points =================");
        multiCrossPoints = this.GetLeftCrossPoints(startNode);
        if (this.CheckMultiCrossPoints(multiCrossPoints, targetNode)) return true;

        return false;
    }

    protected virtual bool CheckMultiCrossPoints(List<Vector2> multiCrossPoints, Node targetNode)
    {
        foreach (Vector2 point in multiCrossPoints)
        {
            Node subNode = this.GetNode((int)point.x, (int)point.y);
            if (subNode == null) continue;//TODO: study why

            subNode.Dump("Sub node: ");

            if (subNode.occupied) break;

            List<Vector2> subTwoCrossPoints = this.GetCrossPoints(subNode, targetNode);
            if (this.IsCrossPointsLinked(subNode, targetNode, subTwoCrossPoints)) return true;
        }

        return false;
    }

    protected virtual List<Vector2> GetBellowCrossPoints(Node startNode)
    {
        List<Vector2> crossPoints = new List<Vector2>();
        int x = startNode.x;
        int currentY = startNode.y;
        for (int i = currentY - 1; i >= 0; i--)
        {
            Vector2 point = new Vector2(x, i);
            crossPoints.Add(point);
        }

        return crossPoints;
    }

    protected virtual List<Vector2> GetAboveCrossPoints(Node startNode)
    {
        List<Vector2> crossPoints = new List<Vector2>();
        int x = startNode.x;
        int currentY = startNode.y;
        int maxY = GridManagerCtrl.Instance.gridSystem.height;
        for (int i = currentY + 1; i <= maxY; i++)
        {
            Vector2 point = new Vector2(x, i);
            crossPoints.Add(point);
        }

        return crossPoints;
    }

    protected virtual List<Vector2> GetRightCrossPoints(Node startNode)
    {
        List<Vector2> crossPoints = new List<Vector2>();
        int y = startNode.y;
        int currentX = startNode.x;
        int maxX = GridManagerCtrl.Instance.gridSystem.width;
        for (int i = currentX + 1; i <= maxX; i++)
        {
            Vector2 point = new Vector2(i, y);
            crossPoints.Add(point);
        }

        return crossPoints;
    }

    protected virtual List<Vector2> GetLeftCrossPoints(Node startNode)
    {
        List<Vector2> crossPoints = new List<Vector2>();
        int y = startNode.y;
        int currentX = startNode.x;
        for (int i = currentX - 1; i >= 0; i--)
        {
            Vector2 point = new Vector2(i, y);
            crossPoints.Add(point);
        }

        return crossPoints;
    }

    protected virtual bool IsCrossPointsLinked(Node startNode, Node targetNode, List<Vector2> twoCrossPoints)
    {
        foreach (Vector2 point in twoCrossPoints)
        {
            Node node = this.GetNode((int)point.x, (int)point.y);
            node.Dump("IsCrossPointsLinked: ");

            bool firstCrossLinked = !this.IsLineBlocked(startNode, node);
            bool secondCrossLinked = !this.IsLineBlocked(targetNode, node);

            if (!node.occupied && firstCrossLinked && secondCrossLinked)
            {
                this.pathNodes.Add(startNode);
                this.pathNodes.Add(node);
                this.pathNodes.Add(targetNode);
                return true;
            }
        }

        return false;
    }

    protected virtual List<Vector2> GetCrossPoints(Node startNode, Node targetNode)
    {
        List<Vector2> points = new List<Vector2>();

        Vector2 point1 = new Vector2(startNode.x, targetNode.y);
        points.Add(point1);
        Vector2 point2 = new Vector2(targetNode.x, startNode.y);
        points.Add(point2);

        return points;
    }

    protected virtual bool IsLineBlocked(Node startNode, Node targetNode)
    {
        if (startNode.y == targetNode.y)
        {
            if (this.IsLineBlockedX(startNode, targetNode)) return true;
        }

        if (startNode.x == targetNode.x)
        {
            if (this.IsLineBlockedY(startNode, targetNode)) return true;
        }

        return false;
    }

    protected virtual List<int> GetNumberBettwen(int start, int end)
    {

        //int startNumber = Mathf.Min(start, end);
        //int endNumber = Mathf.Max(start, end);
        //for (int i = startNumber; i < endNumber; i++)
        //{
        //    numbers.Add(i);
        //}
        //return numbers;

        List<int> numbers = new List<int>();
        if (start > end)
        {
            for (int i = start - 1; i > end; i--)
            {
                numbers.Add(i);
            }
            return numbers;
        }

        for (int i = start + 1; i < end; i++)
        {
            numbers.Add(i);
        }

        return numbers;
    }

    protected virtual bool IsLineBlockedX(Node startNode, Node targetNode)
    {
        int y = startNode.y;
        List<int> numbers = this.GetNumberBettwen(startNode.x, targetNode.x);
        if (numbers.Count == 0) return false;
        foreach (var x in numbers)
        {
            Node node = this.GetNode(x, y);
            if (node.nodeId == startNode.nodeId) continue;
            if (node.nodeId == targetNode.nodeId) continue;
            if (node.occupied) return true;
        }
        return false;
    }

    protected virtual bool IsLineBlockedY(Node startNode, Node targetNode)
    {
        int x = startNode.x;
        List<int> numbers = this.GetNumberBettwen(startNode.y, targetNode.y);
        if (numbers.Count == 0) return false;
        foreach (var y in numbers)
        {
            Node node = this.GetNode(x, y);
            if (node.occupied) return true;
        }
        return false;
    }

    protected virtual bool IsLineUp(Node startNode, Node targetNode)
    {
        if (startNode.x == targetNode.x) return true;
        if (startNode.y == targetNode.y) return true;
        return false;
    }

    protected virtual Node GetNode(int x, int y)
    {
        return GridManagerCtrl.Instance.gridSystem.GetNodeByXY(x, y);
    }

    protected virtual void AddNodeToPath(Node node)
    {
        if (this.pathNodes.Contains(node)) return;
        this.pathNodes.Add(node);
    }
}
