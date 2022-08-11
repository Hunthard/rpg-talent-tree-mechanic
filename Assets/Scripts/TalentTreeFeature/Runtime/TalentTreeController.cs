using System;
using System.Collections.Generic;
using UnityEngine;

namespace Huntag.TalentTreeFeature
{
    public class TalentTreeController : MonoBehaviour
    {
        public TalentTreeView View;
        public TalentTreeModel Model;

        [SerializeField]
        private TalentViewCommonSettingsSO _settings;

        private Talent _selectedTalent;

        #region Unity Messages

        private void Awake()
        {
            CreateTree();
            InitButtonViews();
            Subscribe();
        }

        private void OnEnable()
        {
            ClearSelection();
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
            talents.Add(new Talent(0, "Base", new ExploredTalentState(), 0));

            for (int i = 1; i < talents.Capacity; i++)
            {
                talents.Add(new Talent(i, i.ToString(), new UnlockedTalentState(), (uint)i));
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

        private void InitButtonViews()
        {
            for (int i = 0; i < View.TalentButtons.Count; i++)
            {
                View.TalentButtons[i].Talent = Model.Talents[i];
                View.TalentButtons[i].Name.text = Model.Talents[i].Name;
            }
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

        private void ClearSelection()
        {
            _selectedTalent = null;
            View.TalentName.text = string.Empty;
            View.TalentDescription.text = string.Empty;
        }

        private void UpdateView()
        {
            //CheckTreeTalentsStatus();

            for (int i = 0; i < View.TalentButtons.Count; i++)
            {
                UpdateButtonView(View.TalentButtons[i], Model.Talents[i]);
            }
        }

        private void UpdateButtonView(TalentButton button, Talent talent)
        {
            switch (talent.State)
            {
                case LockedTalentState locked:
                    button.Icon.color = _settings.Locked;
                    break;
                case UnlockedTalentState unlocked:
                    button.Icon.color = _settings.Unlocked;
                    break;
                case ExploredTalentState explore:
                    button.Icon.color = _settings.Explored;
                    break;
                default:
                    break;
            }
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
            button.Clicked += SelectTalent;
        }

        private void SelectTalent(object sender, TalentButton.TalentButtonEventArgs e)
        {
            _selectedTalent = e.Talent;
            View.TalentName.text = _selectedTalent.Name;
            View.TalentDescription.text = $"Cost: {_selectedTalent.Cost}";
            //if (talent.State is LockedTalentState) return;

            //if (talent.State is UnlockedTalentState)
            //{
            //    talent.Explore();
            //    e.Icon.color = _settings.Explored;
            //}

            //UpdateView();
        }

        #endregion
    }
}