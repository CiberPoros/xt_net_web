using System;
using System.Collections.Generic;

namespace ThreeLayer.Common.Entities
{
    public class User
    {
        private int _age;
        private ICollection<Award> _awards;

        public User(int id, string name, DateTime dateOfBirth, int age)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Age = age;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get => _age;
            set => _age = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), value, "Age can't be negative.");
        }

        public ICollection<Award> Awards 
        { 
            get => _awards; 
            set => _awards = value ?? throw new ArgumentNullException(nameof(value), "Argument is null."); 
        }
    }
}
