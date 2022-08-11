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
        {
            Unsubscribe();
        }

        #endregion

        #region Public Methods

        public void Explore()
        {
            _selectedTalent.Explore();
            UpdateView();
        }

        public void ResetAbility()
        {
            _selectedTalent.Unlock();
            UpdateView();
        }

        public void ResetAllAbilities()
        {
            for (int i = 1; i < Model.Talents.Count; i++)
            {
                if (Model.Talents[i].HasRootDirectAccess())
                    Model.Talents[i].Unlock();
                else
                    Model.Talents[i].Lock();
            }

            UpdateView();
        }

        #endregion

        #region Private Methods

        private void CreateTree()
        {
            var talents = new List<Talent>(11);

            talents.Add(new Talent(0, "Base", new ExploredTalentState(), 0));
            talents.Add(new Talent(1, "1", new UnlockedTalentState(), 1, talents[0]));
            talents.Add(new Talent(2, "2", new UnlockedTalentState(), 2, talents[0]));
            talents.Add(new Talent(3, "3", new UnlockedTalentState(), 2, talents[2]));
            talents.Add(new Talent(4, "4", new UnlockedTalentState(), 2, talents[0]));
            talents.Add(new Talent(5, "5", new UnlockedTalentState(), 2, talents[4]));
            talents.Add(new Talent(6, "6", new UnlockedTalentState(), 2, talents[4]));
            talents.Add(new Talent(7, "7", new UnlockedTalentState(), 2, talents[5], talents[6]));
            talents.Add(new Talent(8, "8", new UnlockedTalentState(), 2, talents[0]));
            talents.Add(new Talent(9, "9", new UnlockedTalentState(), 2, talents[0]));
            talents.Add(new Talent(10, "10", new UnlockedTalentState(), 2, talents[8], talents[9]));

            //for (int i = 1; i < talents.Capacity; i++)
            //{
            //    talents.Add(new Talent(i, i.ToString(), new UnlockedTalentState(), (uint)i));
            //    talents[i].AddLinkedTalents(talents[0]);
            //}

            //talents[5].AddLinkedTalents(talents[4]);
            //talents[5].RemoveLinckedTalents(talents[0]);
            //talents[6].AddLinkedTalents(talents[4]);
            //talents[6].RemoveLinckedTalents(talents[0]);
            //talents[7].AddLinkedTalents(talents[5], talents[6]);
            //talents[7].RemoveLinckedTalents(talents[0]);
            //talents[3].AddLinkedTalents(talents[2]);
            //talents[3].RemoveLinckedTalents(talents[0]);
            //talents[10].AddLinkedTalents(talents[8], talents[9]);
            //talents[10].RemoveLinckedTalents(talents[0]);

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
            View.ExploreClicked += Explore;
            View.ResetClicked += ResetAbility;
            
            foreach (var button in View.TalentButtons)
            {
                AddButtonEventHandler(button);
            }
        }

        private void Unsubscribe()
        {
            View.ExploreClicked -= Explore;
            View.ResetClicked -= ResetAbility;
        }

        private void ClearSelection()
        {
            _selectedTalent = null;
            View.TalentName.text = string.Empty;
            View.TalentDescription.text = string.Empty;
        }

        private void UpdateView()
        {
            CheckTreeTalentsStatus();

            if (_selectedTalent != null)
            {
                View.Explore.gameObject.SetActive(!(_selectedTalent.State is ExploredTalentState));
                View.Reset.gameObject.SetActive(!(_selectedTalent.State is UnlockedTalentState));
            }

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

            View.Explore.gameObject.SetActive(!(_selectedTalent.State is ExploredTalentState));
            View.Reset.gameObject.SetActive(!(_selectedTalent.State is UnlockedTalentState));

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