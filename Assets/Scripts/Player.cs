using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _skillPoints = 0;
    public int SkillPoints
    {
        get => _skillPoints;
        set => _skillPoints = value;
    }

    [SerializeField]
    private TalentTree _talentTree;
}
