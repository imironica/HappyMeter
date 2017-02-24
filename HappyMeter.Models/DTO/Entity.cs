using System;
using System.ComponentModel;

namespace HappyMeter.Models
{
    public class Entity
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
