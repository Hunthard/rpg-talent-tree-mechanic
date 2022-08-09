using System;
using System.Collections.Generic;

namespace Huntag.TalentTreeFeature
{
    [Serializable]
    public class Talent
    {
        public class TalentEventArgs : EventArgs
        {
            public Talent talent;

            public TalentEventArgs(Talent talent) : base()
            {
                this.talent = talent;
            }
        }

        public enum State
        {
            Locked = 0,
            Unlocked = 1,
            Investigated = 2
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public uint Cost { get; private set; }
        public State Status { get; private set; }

        private List<Talent> _linkedTalents;

        public Talent()
        {
            Id = -1;
            Name = "new talent";
            Status = State.Locked;
            Cost = 0;
            _linkedTalents = new List<Talent>();
        }

        public Talent(string name, State state, uint cost) : this()
        {
            Name = name;
            Status = state;
            Cost = cost;
        }

        public Talent(string name, State state, uint cost, List<Talent> linkedTalents) : this(name, state, cost)
        {
            if (linkedTalents == null || linkedTalents.Contains(this))
                return;

            _linkedTalents = linkedTalents;
        }

        public void AddLinkedTalents(params Talent[] talents)
        {
            foreach (var talent in talents)
            {
                if (_linkedTalents.Contains(talent)) continue;

                _linkedTalents.Add(talent);
            }
        }

        public void RemoveLinckedTalents(params Talent[] talents)
        {
            foreach (var talent in talents)
            {
                _linkedTalents.Remove(talent);
            }
        }
    }
}