using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeLayer.Common.Entities
{
    public class Role : IEntityWithId
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
