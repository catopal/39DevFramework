using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccsess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System.Collections.Generic;
using DevFramework.Core.Aspects.Postsharp.TransactionAspects;
using DevFramework.Core.Aspects.Postsharp.ValidationAspects;
using DevFramework.Core.Aspects.Postsharp.CacheAspects;
using DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.Core.Aspects.Postsharp.PerformanceAspect;
using System.Diagnostics;
using System.Threading;
using DevFramework.Core.Aspects.Postsharp.AuthorizationAspects;

namespace DevFramework.Northwind.Business.Concrate.Managers
{
   // [LogAspect(typeof(FileLogger))] buraya yazmak yerine assembly infoya yazarak tüm classların loglaması gerçekleştirilebilir.
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))] 
        public Product Add(Product product)
        {
           // ValidatorTool.FluentValidate(new ProductValidator(), product);
            return _productDal.Add(product);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformanceCounterAspect(2)]
        [SecuredOperation(Roles="Admin,Editor")]
        public List<Product> GetAll()
        {
            Thread.Sleep(3000);
            return _productDal.GetList();
        }

        public Product GetById(int id)
        {
            return _productDal.Get(p=> p.ProductId==id);
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        public Product Update(Product product)
        {
            return _productDal.Update(product);
        }

        [TransactionScopeAspect]
        public void TransactionalOperation(Product product1, Product product2)
        {
            _productDal.Add(product1);
            _productDal.Update(product2);
        }

    }
}
