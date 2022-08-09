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
    }
}