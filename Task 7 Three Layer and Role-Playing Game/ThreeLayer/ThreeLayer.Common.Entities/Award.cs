using System;
using System.Collections.Generic;

namespace ThreeLayer.Common.Entities
{
    public class Award
    {
        private string _title;
        private ICollection<User> _owners;

        public int Id { get; set; }

        public string Title 
        { 
            get => _title; 
            set => _title = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(value), "String can't be null or white space.") : value; 
        }

        public ICollection<User> Owners
        {
            get => _owners;
            set => _owners = value ?? throw new ArgumentNullException(nameof(value), "Argument is null.");
        }
    }
}
