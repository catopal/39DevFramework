using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.NHibernate
{
    public class NhQueeryableRepository<T> : IQueryableRepository<T>
        where T : class, IEntity, new()
    {
        private NHibernateHelper _nHibernateHelper;
        private IQueryable<T> _entites;

        public NhQueeryableRepository(NHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }


        public IQueryable<T> Table
        {
            get { return this._entites; }
        }

        public virtual IQueryable<T> Entities 
        { 
            get 
            { 
                if (_entites == null)
                {
                    _entites = _nHibernateHelper.OpenSession().Query<T>();
                }
                return _entites; 
            } 
        }
    }
}
