namespace Huntag.TalentTreeFeature
{
    public interface ITalentState
    {
        void Lock(Talent talent);
        void Unlock(Talent talent);
        void Explore(Talent talent);
    }
}
