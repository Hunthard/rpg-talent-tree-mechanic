using System.Collections.Generic;
using UnityEngine;

public class TalentTreeController : MonoBehaviour
{
    [SerializeField]
    private TalentViewCommonSettingsSO _settings;

    public List<TalentView> TalentsView;

    public TalentTree TalentTree;

    #region Unity Messages

    private void Awake()
    {
        CreateTree();
        InitTalentTree();
    }

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    private void CreateTree()
    {
        var talents = new List<Talent>(11);
        talents.Add(new Talent("Base", Talent.State.Investigated, 0));

        for (int i = 1; i < talents.Capacity; i++)
        {
            talents.Add(new Talent(i.ToString(), Talent.State.Unlocked, 0));
        }

        talents[7].AddLinkedTalents(talents[5], talents[6]);
        talents[3].AddLinkedTalents(talents[2]);
        talents[10].AddLinkedTalents(talents[8], talents[9]);

        TalentTree = new TalentTree(talents);
    }

    private void InitTalentTree()
    {
        for (int i = 0; i < TalentTree.Talents.Count; i++)
        {
            UpdateTalentView(TalentsView[i], TalentTree.Talents[i]);
        }
    }

    private void UpdateTalentView(TalentView view, Talent talent)
    {
        view.Name.text = talent.Name;

        view.Icon.color = talent.Status switch
        {
            Talent.State.Locked => _settings.Locked,
            Talent.State.Unlocked => _settings.Unlocked,
            Talent.State.Investigated => _settings.Investigated,
            _ => _settings.Locked,
        };
    }

    #endregion
}
