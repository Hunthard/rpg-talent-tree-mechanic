namespace Huntag.TalentTreeFeature
{
    public class UnlockedTalentState : ITalentState
    {
        public void Explore(Talent talent)
        {
            talent.State = new ExploredTalentState();
        }

        public void Lock(Talent talent)
        {
            talent.State = new LockedTalentState();
        }

        public void Unlock(Talent talent)
        { }
    }
}
