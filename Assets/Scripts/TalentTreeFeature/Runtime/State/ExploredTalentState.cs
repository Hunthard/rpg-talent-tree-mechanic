namespace Huntag.TalentTreeFeature
{
    public class ExploredTalentState : ITalentState
    {
        public void Explore(Talent talent)
        { }

        public void Lock(Talent talent)
        {
            talent.State = new LockedTalentState();
        }

        public void Unlock(Talent talent)
        {
            talent.State = new UnlockedTalentState();
        }
    }
}
