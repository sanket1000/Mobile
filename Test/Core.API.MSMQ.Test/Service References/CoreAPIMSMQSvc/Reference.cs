﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.2045
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.API.MSMQ.Test.CoreAPIMSMQSvc {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CoreAPIMSMQSvc.ICoreAPIMsgHandler")]
    public interface ICoreAPIMsgHandler {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICoreAPIMsgHandler/QueueMessage")]
        void QueueMessage(string Label, string QueueMsg);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICoreAPIMsgHandlerChannel : Core.API.MSMQ.Test.CoreAPIMSMQSvc.ICoreAPIMsgHandler, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CoreAPIMsgHandlerClient : System.ServiceModel.ClientBase<Core.API.MSMQ.Test.CoreAPIMSMQSvc.ICoreAPIMsgHandler>, Core.API.MSMQ.Test.CoreAPIMSMQSvc.ICoreAPIMsgHandler {
        
        public CoreAPIMsgHandlerClient() {
        }
        
        public CoreAPIMsgHandlerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CoreAPIMsgHandlerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CoreAPIMsgHandlerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CoreAPIMsgHandlerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void QueueMessage(string Label, string QueueMsg) {
            base.Channel.QueueMessage(Label, QueueMsg);
        }
    }
}
