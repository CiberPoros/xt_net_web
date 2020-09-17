using System;

namespace ThreeLayer.Common.Entities
{
    public class User : IEntityWithId
    {
        private int _age;

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get => _age;
            set => _age = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), value, "Age can't be negative.");
        }

        public override bool Equals(object obj) => obj is User user && Id == user.Id;
        public override int GetHashCode() => 2108858624 + Id.GetHashCode();
        public override string ToString() =>
            $"ID = {Id}; Name = {Name}; Age = {Age}; BirthDay = {DateOfBirth:MM/dd/yyyy}";
    }
}
