using System;

namespace ThreeLayer.Common.Entities
{
    public class Award : IEntityWithId
    {
        private string _title;

        public int Id { get; set; }

        public string Title
        {
            get => _title;
            set => _title = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(value), "String can't be null or white space.") : value;
        }

        public override bool Equals(object obj) => obj is Award award && Id == award.Id;
        public override int GetHashCode() => 2108858624 + Id.GetHashCode();
        public override string ToString() => $"ID = {Id}; Title = {Title}";
    }
}
