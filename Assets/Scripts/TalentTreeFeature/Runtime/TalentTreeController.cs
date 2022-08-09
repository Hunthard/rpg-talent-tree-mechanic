using System.Collections.Generic;
using UnityEngine;

namespace Huntag.TalentTreeFeature
{
    public class TalentTreeController : MonoBehaviour
    {
        [SerializeField]
        private TalentViewCommonSettingsSO _settings;

        public TalentTreeView View;
        public TalentTreeModel Model;

        #region Unity Messages

        private void Awake()
        {
            CreateTree();
        }

        private void OnEnable()
        {
            UpdateTalentTreeView();
        }

        #endregion

        #region Public Methods

        public void UnlockTalent(Talent talent)
        {

        }

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

            Model = new TalentTreeModel(talents);
        }

        private void UpdateTalentTreeView()
        {
            for (int i = 0; i < Model.Talents.Count; i++)
            {
                UpdateView(View.TalentsView[i], Model.Talents[i]);
            }
        }

        private void UpdateView(TalentButton view, Talent talent)
        {
            view.Name.text = talent.Name;

            view.Icon.color = talent.Status switch
            {
                Talent.State.Locked => _settings.Locked,
                Talent.State.Unlocked => _settings.Unlocked,
                Talent.State.Investigated => _settings.Investigated,
                _ => _settings.Locked,
            };

            view.Clicked += View_Clicked;
        }

        private void View_Clicked(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}