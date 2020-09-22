using System;

namespace ThreeLayer.Common.Entities
{
    public class User : IEntityWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age => 
            DateTime.Now.Year - DateOfBirth.Year - 
            ((DateOfBirth.Month > DateTime.Now.Month
            || (DateOfBirth.Month == DateTime.Now.Month
            && DateOfBirth.Day >= DateTime.Now.Day)) ? 1 : 0);

        public override bool Equals(object obj) => obj is User user && Id == user.Id;
        public override int GetHashCode() => 2108858624 + Id.GetHashCode();
        public override string ToString() =>
            $"ID = {Id}; Name = {Name}; Age = {Age}; BirthDay = {DateOfBirth:MM/dd/yyyy}";
    }
}
