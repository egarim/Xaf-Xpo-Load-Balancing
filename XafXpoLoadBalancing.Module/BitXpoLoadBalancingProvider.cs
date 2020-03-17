using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafXpoLoadBalancing.Module
{
   
    public class BitXpoLoadBalancingProvider : DataStoreForkMultipleReadersSingleWriter
    {
        public BitXpoLoadBalancingProvider(IDataStore changesProvider, params IDataStore[] readProviders) : base(changesProvider, readProviders)
        {
        }

        public override IDataStore AcquireReadProvider()
        {
            IDataStore dataStore = base.AcquireReadProvider();
            DevExpress.Xpo.DB.ConnectionProviderSql RealDataStore = (DevExpress.Xpo.DB.ConnectionProviderSql)dataStore;

            System.Diagnostics.Debug.WriteLine("AcquireReadProvider Data from connection:" + RealDataStore.ConnectionString.ToString().Split(';').FirstOrDefault(cs => cs.StartsWith("Initial Catalog=")).Replace("Initial Catalog=",""));
            return dataStore;
            // your code goes here
        }
        public override IDataStore AcquireChangeProvider()
        {
            IDataStore dataStore = base.AcquireChangeProvider();
            DevExpress.Xpo.DB.ConnectionProviderSql RealDataStore = (DevExpress.Xpo.DB.ConnectionProviderSql)dataStore;

            System.Diagnostics.Debug.WriteLine("AcquireChangeProvider Data from connection:" + RealDataStore.ConnectionString.ToString().Split(';').FirstOrDefault(cs => cs.StartsWith("Initial Catalog=")).Replace("Initial Catalog=", ""));
            return dataStore;


            
        }

    }
}
