using System;
using System.Collections.Generic;

namespace ThreeLayer.Common.Entities
{
    public class User
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
    }
}
