using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOA.WLIMS.Models
{
    public class StorehouseModel
    {

        #region 基元属性

        public global::System.Int64 ID
        { get; set; }
        public global::System.String Name
        { get; set; }
        public global::System.String Address
        { get; set; }
        public Nullable<global::System.Int32> Capacity
        { get; set; }
        public global::System.String Master
        { get; set; }

        #endregion

    }
}
