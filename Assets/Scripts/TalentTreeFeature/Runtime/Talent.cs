using System;
using System.Collections.Generic;

namespace Huntag.TalentTreeFeature
{
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

        public int Id { get; private set; }
        public string Name { get; private set; }
        public uint Cost { get; private set; }

        public ITalentState State { get; set; }

        private HashSet<Talent> _linkedTalents;

        public Talent()
        {
            Id = -1;
            Name = "new talent";
            State = new LockedTalentState();
            Cost = 0;
            _linkedTalents = new HashSet<Talent>();
        }

        public Talent(int id, string name, ITalentState state, uint cost) : this()
        {
            Id = id;
            Name = name;
            State = state;
            Cost = cost;
        }

        public Talent(int id, string name, ITalentState state, uint cost, params Talent[] talents) : this(id, name, state, cost)
        {
            AddLinkedTalents(talents);
        }

        public void Explore() => State.Explore(this);

        public void Lock() => State.Lock(this);

        public void Unlock() => State.Unlock(this);

        public void AddLinkedTalents(params Talent[] talents)
        {
            foreach (var talent in talents)
            {
                if (talent.Equals(this)) continue;
                
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

        public bool HasOneOrMoreExploredLinkedTalents()
        {
            foreach (var talent in _linkedTalents)
            {
                if (talent.State is ExploredTalentState) return true;
            }

            return false;
        }
    }
}