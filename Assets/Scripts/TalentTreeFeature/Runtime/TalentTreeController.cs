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
            CreateTree();
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
        private void CreateTree()
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

        public void Explore()
        {
            if (_points < _selectedTalent.Cost) return;

            _points -= _selectedTalent.Cost;
            _selectedTalent.Explore();

            UpdateUI();
        }

        public void ResetAbility()
        {
            if (!Model.IsTreeValid(_selectedTalent.Id)) return;

            _points += _selectedTalent.Cost;
            _selectedTalent.Unlock();

            UpdateUI();
        }

        public void ResetAllAbilities()
        {
            for (int i = 1; i < Model.Talents.Count; i++)
            {
                if (Model.Talents[i].State is ExploredTalentState)
                    _points += Model.Talents[i].Cost;

                Model.Talents[i].Lock();
            }

            UpdateUI();
        }

        public void AddPoint()
        {
            _points++;

            UpdateUI();
        }

        private void Subscribe()
        {
            View.ExploreClicked += Explore;
            View.ResetClicked += ResetAbility;
            View.AddPointClicked += AddPoint;

            foreach (var button in View.TalentButtons)
            {
                AddButtonEventHandler(button);
            }
        }

        private void Unsubscribe()
        {
            View.ExploreClicked -= Explore;
            View.ResetClicked -= ResetAbility;
            View.AddPointClicked -= AddPoint;
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

            for (int i = 0; i < View.TalentButtons.Count; i++)
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
            for (int i = 1; i < Model.Talents.Count; i++)
            {
                if (Model.Talents[i].State is LockedTalentState && Model.Talents[i].ExploredLinkedTalentCount() > 0)
                    Model.Talents[i].Unlock();

                if (Model.Talents[i].State is UnlockedTalentState && Model.Talents[i].ExploredLinkedTalentCount() < 1)
                    Model.Talents[i].Lock();
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

            View.Explore.gameObject.SetActive(_selectedTalent.State is UnlockedTalentState);
            View.Reset.gameObject.SetActive(_selectedTalent.State is ExploredTalentState);
        }

        #endregion
    }
}