using UnityEngine;
using Huntag.TalentTreeFeature;

public class Player : MonoBehaviour
{
    private static int s_SkillPoints = 0;
    public static int SkillPoints
    {
        get { return s_SkillPoints; }
        set { s_SkillPoints = Mathf.Clamp(s_SkillPoints, 0, int.MaxValue); }
    }

    [SerializeField]
    private TalentTreeModel _talentTree;

    public void AddSkillPoint()
    {
        SkillPoints++;
    }
}
