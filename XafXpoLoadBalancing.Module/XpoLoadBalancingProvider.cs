using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafXpoLoadBalancing.Module
{
    public class XpoLoadBalancingProvider: IXpoDataStoreProvider
    {

        IDataStore _appDataStore;
        IDataStore _mainDatabase;
        public XpoLoadBalancingProvider(string mainConnectionString, string replica1ConnectionString, string replica2ConnectionString)
        {
            _mainDatabase = XpoDefault.GetConnectionProvider(mainConnectionString, AutoCreateOption.None);
            IDataStore replica1 = XpoDefault.GetConnectionProvider(replica1ConnectionString, AutoCreateOption.None);
            IDataStore replica2 = XpoDefault.GetConnectionProvider(replica2ConnectionString, AutoCreateOption.None);
            _appDataStore = new BitXpoLoadBalancingMultipleReadSingleWriteProvider(_mainDatabase, replica1, replica2);
        }

        readonly string _connectionString;
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public IDataStore CreateSchemaCheckingStore(out IDisposable[] disposableObjects)
        {
            disposableObjects = null;
            return _appDataStore;
        }

        public IDataStore CreateUpdatingStore(bool allowUpdateSchema, out IDisposable[] disposableObjects)
        {
            disposableObjects = null;
            return _appDataStore;
        }

        public IDataStore CreateWorkingStore(out IDisposable[] disposableObjects)
        {
            disposableObjects = null;
            return _appDataStore;
        }
    }
    //public class SyncDataStoreProvider : IXpoDataStoreProvider
    //{
    //    private SyncDataStore DataStore;
    //    Assembly[] _Assemblies;
    //    public SyncDataStoreProvider(params Assembly[] Assemblies)
    //    {
    //        SyncDataStore.EnableTransactionHistory = true;
    //        _Assemblies = Assemblies;
    //        DataStore = new SyncDataStore("XAF");
    //    }
    //    public DevExpress.Xpo.DB.IDataStore CreateUpdatingStore(bool allowUpdateSchema, out IDisposable[] disposableObjects)
    //    {
    //        disposableObjects = null;
    //        return DataStore;
    //    }
    //    public DevExpress.Xpo.DB.IDataStore CreateWorkingStore(out IDisposable[] disposableObjects)
    //    {
    //        disposableObjects = null;
    //        return DataStore;
    //    }
    //    public DevExpress.Xpo.DB.IDataStore CreateSchemaCheckingStore(out IDisposable[] disposableObjects)
    //    {
    //        disposableObjects = null;
    //        return DataStore;
    //    }
    //    public XPDictionary XPDictionary
    //    {
    //        get { return null; }
    //    }
    //    public string ConnectionString
    //    {
    //        get { return null; }
    //    }
    //    public bool IsInitialized
    //    {
    //        get;
    //        private set;
    //    }
    //    public void Initialize(XPDictionary dictionary, string LocalDatabase, string LogDatabase)
    //    {

    //        //proxy.Initialize(new Assembly[] { this.GetType().Assembly }, legacyConnectionString, tempConnectionString);
    //        DataStore.Initialize(LocalDatabase, LogDatabase, false, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema, _Assemblies);
    //        IsInitialized = true;
    //    }
    //}
}
