using System;
using System.Collections.Generic;
using System.Text;

namespace Views
{
    public interface ICreature
    {
        string GetNamePicture();
        int GetDrawingPriority();
        CreatureCommand Act(int x, int y);
        bool DeadInConflict(ICreature conflitedObject);
    }
}
