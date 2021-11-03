using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using TTQK7.Models;

namespace TTQK7.Controllers
{
    public class ComboTreeController : Controller
    {
        private TTQK7Entities  db = new TTQK7Entities ();        
        public ActionResult  GetTreeData()
        {
            
                List<ComboTreeModel> nodes = new List<ComboTreeModel>();
                //add những node có parentID null
                List<tblDonVi> dvs = db.tblDonVi.Where(x => x.ParentID == null).ToList();
                foreach (tblDonVi  dv in dvs)
                {
                    ComboTreeModel node = new ComboTreeModel();
                    node.id = dv.DonViID ;
                    node.title = dv.TenDonVi ;
                    SetChildren(node);
                    nodes.Add(node);
                }
                //AlreadyPopulated = true;
                return Json(nodes,JsonRequestBehavior.AllowGet );
            
        }
        public void SetChildren(ComboTreeModel node)
        {
            List<tblDonVi> dvs = db.tblDonVi.Where(x => x.ParentID == node.id).ToList();
            foreach (tblDonVi dv in dvs)
            {
                ComboTreeModel childnode = new ComboTreeModel();
                childnode.id = dv.DonViID ;
                childnode.title  = dv.TenDonVi ;
                SetChildren(childnode);
                node.subs.Add(childnode);
            }
        }       

    }
}