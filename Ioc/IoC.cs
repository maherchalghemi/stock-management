using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ioc
{
    public class IoC
    {
        private static IUnityContainer _Container;

        public IoC(IUnityContainer Container)
        {
            _Container = Container;
        }
        public void ResgitreType()
        {
          
            
            /*_Container.RegisterType(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            _Container.RegisterType<IDepotRepository, DepotRepository>();*/
    }
    }
}
    
