using System.Collections.Generic;

namespace XM.Model
{
    public class TreeViewModel
    {
        public List<TreeViewModel> ChildNodes { get; set; }
        public string parentId { get; set; }
        public string id { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        /// <summary>
        ///  选中状态 0(不选中)，1(选中)
        /// </summary>
        public int? checkstate { get; set; }
        public bool showcheck { get; set; }
        public bool complete { get; set; }
        public bool isexpand { get; set; }
        public bool hasChildren { get; set; }
        public string parentnodes { get; set; }
    }
}