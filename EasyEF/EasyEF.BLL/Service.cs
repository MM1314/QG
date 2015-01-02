using System;
using EasyEF.Contract;
using System.Collections.Generic;
using EasyEF.Models;
using EasyEF.IDAL;
using System.Linq.Expressions;
using EasyEF.Common;
using EasyEF.WCFExtension;

namespace EasyEF.BLL
{
    public class Service : IService
    {
        public IDAO dao;
        
        public Service()
        {
            //Need to inject dynamic later
            this.dao = new EasyEF.DAL.DAO();
        }

        public PagedList<Product> GetProducts(int pageSize, int pageIndex, int categoryId = 0)
        {
            //Test WCFContext
            var context = WCFContext.Current.Operater;
            
            return this.dao.FindAllByPage<Product, int>(p => categoryId == 0 ? true : p.CategoryId == categoryId, p => p.Id, pageSize, pageIndex);
        }
    }
}
