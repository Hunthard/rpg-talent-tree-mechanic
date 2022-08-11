using System.Collections.Generic;

namespace Huntag.TalentTreeFeature
{
    public class TalentTreeModel
    {
        public List<Talent> Talents;

        public TalentTreeModel()
        {
            Talents = new List<Talent>();
        }

        public TalentTreeModel(List<Talent> talents)
        {
            Talents = talents;
        }

        public bool IsTreeValid(int removableID)
        {
            if (removableID == 0) return false;
            
            foreach (var exploredTalent in Talents)
            {
                if (!(exploredTalent.State is ExploredTalentState)) continue;

                if (!exploredTalent.HasRootPath(removableID)) return false;
            }

            return true;
        }
    }
}