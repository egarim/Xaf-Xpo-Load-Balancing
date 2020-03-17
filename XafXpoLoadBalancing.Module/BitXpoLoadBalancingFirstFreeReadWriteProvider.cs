using DevExpress.Xpo.DB;
using System;
using System.Linq;

namespace XafXpoLoadBalancing.Module
{
    /// <summary>
    /// This provider will write on any of the free data store and will read from any of the free datastore
    /// </summary>
    public class BitXpoLoadBalancingFirstFreeReadWriteProvider : DataStoreFork
    {
        public BitXpoLoadBalancingFirstFreeReadWriteProvider(IDataStore changesProvider, params IDataStore[] readProviders) : base(changesProvider, readProviders)
        {
        }

        public override IDataStore AcquireReadProvider()
        {
            IDataStore dataStore = base.AcquireReadProvider();
            DataStoreDiagnostics(dataStore);
            return dataStore;
            // your code goes here
        }

        private static void DataStoreDiagnostics(IDataStore dataStore, [System.Runtime.CompilerServices.CallerMemberName] string MethodName = "")
        {
            DevExpress.Xpo.DB.ConnectionProviderSql RealDataStore = (DevExpress.Xpo.DB.ConnectionProviderSql)dataStore;

            System.Diagnostics.Debug.WriteLine($"{MethodName} Data from connection:" + RealDataStore.ConnectionString.ToString().Split(';').FirstOrDefault(cs => cs.StartsWith("Initial Catalog=")).Replace("Initial Catalog=", ""));
        }

        public override IDataStore AcquireChangeProvider()
        {
            IDataStore dataStore = base.AcquireChangeProvider();
            DataStoreDiagnostics(dataStore);
            return dataStore;
        }
    }
}
