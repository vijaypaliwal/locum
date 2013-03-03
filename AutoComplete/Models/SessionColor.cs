using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoComplete.Models
{
    public class SessionColor
    {
        public int ID{ get; set; }
        public string Color { get; set; }
        [Required]
        public Guid SID { get; set; }
        #region other properties
        [ForeignKey("SID")]
        public virtual Sessions Sessions { get; set; }
        #endregion

    }
}