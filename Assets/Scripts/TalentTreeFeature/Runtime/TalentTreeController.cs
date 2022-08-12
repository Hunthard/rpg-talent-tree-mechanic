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

        private int _points = 0;

        #region Unity Messages

        private void Awake()
        {
            if (Model == null) Model = GetTalentTree();

            InitButtonViews();
            Subscribe();
        }

        private void OnEnable()
        {
            ClearSelection();
            UpdateUI();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        // TODO: Only for testing purpose. Should create some Editor tool to create and edit talent tree.
        private TalentTreeModel GetTalentTree()
        {
            var talents = new List<Talent>(11);

            talents.Add(new Talent(0, "Base", new ExploredTalentState(), 0));
            talents.Add(new Talent(1, "1", new LockedTalentState(), 1, talents[0]));
            talents.Add(new Talent(2, "2", new LockedTalentState(), 2, talents[0]/*, talents[3]*/));
            talents.Add(new Talent(3, "3", new LockedTalentState(), 3, talents[2]));
            talents.Add(new Talent(4, "4", new LockedTalentState(), 4, talents[0]/*, talents[5], talents[6]*/));
            talents.Add(new Talent(5, "5", new LockedTalentState(), 5, talents[4]/*, talents[7]*/));
            talents.Add(new Talent(6, "6", new LockedTalentState(), 6, talents[4]/*, talents[7]*/));
            talents.Add(new Talent(7, "7", new LockedTalentState(), 7, talents[5], talents[6]));
            talents.Add(new Talent(8, "8", new LockedTalentState(), 8, talents[0]/*, talents[10]*/));
            talents.Add(new Talent(9, "9", new LockedTalentState(), 9, talents[0]/*, talents[10]*/));
            talents.Add(new Talent(10, "10", new LockedTalentState(), 10, talents[8], talents[9]));

            talents[0].AddLinkedTalents(talents[1], talents[2], talents[4], talents[8], talents[9]);
            talents[2].AddLinkedTalents(talents[3]);
            talents[2].AddLinkedTalents(talents[3]);
            talents[4].AddLinkedTalents(talents[5], talents[6]);
            talents[5].AddLinkedTalents(talents[7]);
            talents[6].AddLinkedTalents(talents[7]);
            talents[8].AddLinkedTalents(talents[10]);
            talents[9].AddLinkedTalents(talents[10]);

            return new TalentTreeModel(talents);
        }

        // TODO: Add talent object reference to each button
        private void InitButtonViews()
        {
            for (int i = 0; i < View.TalentButtons.Count; i++)
            {
                View.TalentButtons[i].Talent = Model.Talents[i];
                View.TalentButtons[i].Name.text = Model.Talents[i].Name;
            }
        }

        private void Explore()
        {
            if (_points < _selectedTalent.Cost) return;

            _points -= _selectedTalent.Cost;
            _selectedTalent.Explore();
        }

        private void ResetAbility()
        {
            if (!Model.IsTreeValid(_selectedTalent.Id)) return;

            _points += _selectedTalent.Cost;
            _selectedTalent.Unlock();
        }

        private void ResetAllAbilities()
        {
            foreach (var talent in Model.Talents)
            {
                if (talent.Id == 0) continue;

                if (talent.State is ExploredTalentState)
                    _points += talent.Cost;

                talent.Lock();
            }
        }

        private void AddPoint() => _points++;

        private void Subscribe()
        {
            View.ExploreClicked += Explore;
            View.ResetClicked += ResetAbility;
            View.ResetAllClicked += ResetAllAbilities;
            View.AddPointClicked += AddPoint;

            View.ExploreClicked += UpdateUI;
            View.ResetClicked += UpdateUI;
            View.ResetAllClicked += UpdateUI;
            View.AddPointClicked += UpdateUI;

            foreach (var button in View.TalentButtons)
            {
                button.Clicked += SelectTalent;
            }
        }

        private void Unsubscribe()
        {
            View.ExploreClicked -= Explore;
            View.ResetClicked -= ResetAbility;
            View.ResetAllClicked -= ResetAllAbilities;
            View.AddPointClicked -= AddPoint;

            View.ExploreClicked -= UpdateUI;
            View.ResetClicked -= UpdateUI;
            View.ResetAllClicked -= UpdateUI;
            View.AddPointClicked -= UpdateUI;

            foreach (var button in View.TalentButtons)
            {
                button.Clicked -= SelectTalent;
            }
        }

        private void ClearSelection()
        {
            _selectedTalent = null;
            View.TalentName.text = string.Empty;
            View.TalentDescription.text = string.Empty;
        }

        private void UpdateUI()
        {
            UnlockAvailableTalents();

            if (_selectedTalent != null)
            {
                View.Explore.gameObject.SetActive(_selectedTalent.State is UnlockedTalentState);
                View.Reset.gameObject.SetActive(_selectedTalent.State is ExploredTalentState);
            }

            for (int i = 0; i < Model.Talents.Count; i++)
            {
                UpdateButtonView(View.TalentButtons[i], Model.Talents[i]);
            }

            View.PointsLeft.text = $"Points left: {_points}";
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

        private void UnlockAvailableTalents()
        {
            foreach (var talent in Model.Talents)
            {
                if (talent.Id == 0) continue;

                if (talent.State is LockedTalentState && talent.ExploredLinkedTalentCount() > 0)
                    talent.Unlock();

                if (talent.State is UnlockedTalentState && talent.ExploredLinkedTalentCount() < 1)
                    talent.Lock();
            }
        }

        private void SelectTalent(object sender, TalentButton.TalentButtonEventArgs e)
        {
            _selectedTalent = e.Talent;
            View.TalentName.text = _selectedTalent.Name;
            View.TalentDescription.text = $"Cost: {_selectedTalent.Cost}";

            View.Explore.gameObject.SetActive(_selectedTalent.State is UnlockedTalentState);
            View.Reset.gameObject.SetActive(_selectedTalent.State is ExploredTalentState);
        }

        #endregion
    }
}