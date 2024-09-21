using System;
using System.Collections.Generic;

public abstract class AbstractPathfinding : SaiMonoBehaviour
{
    protected List<Node> pathNodes = new List<Node>();
    public List<Node> PathNodes => pathNodes;

    public abstract bool FindPath(BlockCtrl startBlock, BlockCtrl targetBlock);

    public abstract void DataReset();
}
