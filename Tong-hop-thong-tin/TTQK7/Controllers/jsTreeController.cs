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
    public class jsTreeController : Controller
    {
        private TTQK7Entities  db = new TTQK7Entities ();
        // GET: jsTree
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetTreeData()
        {
            
                List<JsTreeModel> nodes = new List<JsTreeModel>();
                //add những node có parentID null
                List<tblDonVi> dvs = db.tblDonVi.Where(x => x.ParentID == null).ToList();
                foreach (tblDonVi  dv in dvs)
                {
                    JsTreeModel node = new JsTreeModel();
                    node.id = dv.DonViID ;
                    node.text = dv.TenDonVi ;
                    SetChildren(node);
                    nodes.Add(node);
                }
                //AlreadyPopulated = true;
                return Json(nodes);
            
        }
        public void SetChildren(JsTreeModel node)
        {
            List<tblDonVi> dvs = db.tblDonVi.Where(x => x.ParentID == node.id).ToList();
            foreach (tblDonVi dv in dvs)
            {
                JsTreeModel childnode = new JsTreeModel();
                childnode.id = dv.DonViID ;
                childnode.text = dv.TenDonVi ;
                SetChildren(childnode);
                node.children.Add(childnode);
            }
        }
        [HttpPost]
        public ActionResult DoJsTreeOperation(JsTreeOperationData data)
        {
            tblDonVi dv = new tblDonVi();
            int id = 0;
            switch (data.Operation)
            {
                case JsTreeOperation.CopyNode:
                case JsTreeOperation.CreateNode:
                    //todo: save data
                    dv = new tblDonVi();
                    dv.ParentID = int.Parse(data.ParentId);
                    dv.TenDonVi  = data.Text;
                    dv.IsLast = true;
                    db.tblDonVi.Add(dv);
                    db.SaveChanges();
                    return Json(new { id = dv.DonViID  }, JsonRequestBehavior.AllowGet);

                case JsTreeOperation.DeleteNode:
                    //todo: save data
                    id = int.Parse(data.Id);
                    dv = db.tblDonVi.Find(id);
                    db.tblDonVi.Remove(dv);
                    db.SaveChanges();
                    return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);

                case JsTreeOperation.MoveNode:
                    //todo: save data
                    try
                    {
                        id = int.Parse(data.Id);
                        dv = db.tblDonVi.Find(id);
                        dv.ParentID = int.Parse(data.ParentId);
                        db.Entry(dv).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
                    }
                    catch
                    {
                        return Json(new { KetQua = false, ThongBao = "Lỗi lấy dữ liệu. Thử lại" }, JsonRequestBehavior.AllowGet);
                    }
                                       

                case JsTreeOperation.RenameNode:
                    //kiểm tra có tên nào trùng không
                    if(db.tblDonVi.Any(x => x.TenDonVi  == data.Text))
                    {
                        return Json(new { KetQua = false, ThongBao = "Bị trùng tên đơn vị" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //todo: save data
                        id = int.Parse(data.Id);
                        dv = db.tblDonVi.Find(id);
                        dv.TenDonVi  = data.Text;
                        db.Entry(dv).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { KetQua = true, ThongBao = "Đã lưu vào cơ sở dữ liệu" }, JsonRequestBehavior.AllowGet);
                    }
                    

                default:
                    throw new InvalidOperationException(string.Format("{0} is not supported.", data.Operation));
            }
        }

    }
}