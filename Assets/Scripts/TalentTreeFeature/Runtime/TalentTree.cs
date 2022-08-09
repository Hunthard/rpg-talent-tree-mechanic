using System.Collections.Generic;

public class TalentTree
{
    public List<Talent> Talents;

    public TalentTree()
    {
        Talents = new List<Talent>();
    }

    public TalentTree(List<Talent> talents)
    {
        Talents = talents;
    }
}
