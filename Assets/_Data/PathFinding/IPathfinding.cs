using System;
public interface IPathfinding
{
    public abstract bool FindPath(BlockCtrl startBlock, BlockCtrl targetBlock);

    public abstract void DataReset();
}
