using System;
public abstract class AbstractPathfinding : SaiMonoBehaviour
{
    public abstract bool FindPath(BlockCtrl startBlock, BlockCtrl targetBlock);

    public abstract void DataReset();
}
