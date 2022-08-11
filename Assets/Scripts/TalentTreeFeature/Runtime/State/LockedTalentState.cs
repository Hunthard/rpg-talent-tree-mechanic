namespace Huntag.TalentTreeFeature
{
    public class LockedTalentState : ITalentState
    {
        public void Explore(Talent talent)
        { }

        public void Lock(Talent talent)
        { }

        public void Unlock(Talent talent)
        {
            talent.State = new UnlockedTalentState();
        }
    }
}
