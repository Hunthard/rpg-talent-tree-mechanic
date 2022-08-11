using System;
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
            Subscribe();
        }

        private void OnEnable()
        {
            UpdateView();
        }

        private void OnDestroy()
        { }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        private void CreateTree()
        {
            var talents = new List<Talent>(11);
            talents.Add(new Talent(0, "Base", Talent.State.Investigated, 0));

            for (int i = 1; i < talents.Capacity; i++)
            {
                talents.Add(new Talent(i, i.ToString(), Talent.State.Unlocked, (uint)i));
                talents[i].AddLinkedTalents(talents[0]);
            }

            talents[5].AddLinkedTalents(talents[4]);
            talents[5].RemoveLinckedTalents(talents[0]);
            talents[6].AddLinkedTalents(talents[4]);
            talents[6].RemoveLinckedTalents(talents[0]);
            talents[7].AddLinkedTalents(talents[5], talents[6]);
            talents[7].RemoveLinckedTalents(talents[0]);
            talents[3].AddLinkedTalents(talents[2]);
            talents[3].RemoveLinckedTalents(talents[0]);
            talents[10].AddLinkedTalents(talents[8], talents[9]);
            talents[10].RemoveLinckedTalents(talents[0]);

            Model = new TalentTreeModel(talents);
        }

        private void Subscribe()
        {
            foreach (var button in View.TalentButtons)
            {
                AddButtonEventHandler(button);
            }
        }

        private void Unsubscribe()
        { }

        private void UpdateView()
        {
            CheckTreeTalentsStatus();
            
            for (int i = 0; i < View.TalentButtons.Count; i++)
            {
                UpdateButtonView(View.TalentButtons[i], Model.Talents[i]);
            }
        }

        private void UpdateButtonView(TalentButton button, Talent talent)
        {
            button.Name.text = talent.Name;

            button.Icon.color = talent.Status switch
            {
                Talent.State.Locked => _settings.Locked,
                Talent.State.Unlocked => _settings.Unlocked,
                Talent.State.Investigated => _settings.Investigated,
                _ => _settings.Locked,
            };

            button.Talent = talent;
        }

        private void CheckTreeTalentsStatus()
        {
            foreach (var talent in Model.Talents)
            {
                talent.CheckStatus();
            }
        }

        private void AddButtonEventHandler(TalentButton button)
        {
            button.Clicked += Proccess;
        }

        private void Proccess(object sender, TalentButton.TalentButtonEventArgs e)
        {
            var talent = e.Talent;

            if (talent.Status == Talent.State.Locked) return;

            if (talent.Status == Talent.State.Unlocked)
            {
                talent.Investigate();
                e.Icon.color = _settings.Investigated;
            }

            UpdateView();
        }

        #endregion
    }
}