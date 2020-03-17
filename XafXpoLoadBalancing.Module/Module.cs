using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Xpo;

namespace XafXpoLoadBalancing.Module {
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
    public sealed partial class XafXpoLoadBalancingModule : ModuleBase {
        public XafXpoLoadBalancingModule() {
            InitializeComponent();
			BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;

        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        private static XpoLoadBalancingProvider provider;
        void application_CreateCustomObjectSpaceProvider(object sender, CreateCustomObjectSpaceProviderEventArgs e)
        {
            if (provider == null)
            {
             
                provider = new XpoLoadBalancingProvider(
                    GetConnectionString("XpoMainReadWrite"), 
                    GetConnectionString("Replica1"), 
                    GetConnectionString("Replica2"));
            }
            e.ObjectSpaceProvider = new XPObjectSpaceProvider(provider);
        }

        private static string GetConnectionString(string xpoMainReadWrite)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[xpoMainReadWrite].ConnectionString;
        }

        public override void Setup(XafApplication application) {
            base.Setup(application);
            //application.CustomCheckCompatibility += new EventHandler<CustomCheckCompatibilityEventArgs>(application_CustomCheckCompatibility);
            application.CreateCustomObjectSpaceProvider += new EventHandler<CreateCustomObjectSpaceProviderEventArgs>(application_CreateCustomObjectSpaceProvider);
            // Manage various aspects of the application UI and behavior at the module level.
        }
        public override void CustomizeTypesInfo(ITypesInfo typesInfo) {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
        }
    }
}
