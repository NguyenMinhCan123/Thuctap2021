using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;

namespace TTQK7
{    
    public class tblMailMetadata
    {
        public int idMail { get; set; }

        [Display(Name = "Ngày soạn")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Ngay { get; set; }

        [Display(Name = "Tiêu đề")]
        public string TieuDe { get; set; }

        [Display(Name = "Nội dung")]
        [DataType(DataType.MultilineText)]
        public string NoiDung { get; set; }

        public Nullable<int> idUser { get; set; }
    }

}