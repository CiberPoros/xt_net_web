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
    }
}
