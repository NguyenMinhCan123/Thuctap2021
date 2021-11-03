using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTQK7.Models
{
    public class ComboTreeModel
    {
        public int id;
        public string title;
        public List<ComboTreeModel> subs;
        public ComboTreeModel()
        {
            subs = new List<ComboTreeModel>();
        }
    }
}