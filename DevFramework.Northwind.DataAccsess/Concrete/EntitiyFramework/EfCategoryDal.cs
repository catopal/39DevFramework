using DevFramework.Core.DataAccess.EntityFramework;
using DevFramework.Northwind.DataAccsess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccsess.Concrete.EntitiyFramework
{
    public class EfCategoryDal:EfEntityRepositoryBase<Category, NorthwindContext>, ICategoryDal
    {

    }
}
