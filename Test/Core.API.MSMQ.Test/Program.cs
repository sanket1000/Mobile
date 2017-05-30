using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.MSMQ;
using Core.API.MSMQMessageBuilder;
using System.Messaging;

namespace Core.API.MSMQ.Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Core.API.MSMQ.CoreAPIMessageHandler msmq = new CoreAPIMessageHandler();
            //msmq.QueueMessage(SampleXMLMsg());
            //CoreAPIMessageHandler hndlr = new CoreAPIMessageHandler();
            //hndlr.QueueMessage(SampleXMLMsg());
            //CoreAPIMsgHandler.CoreAPIMsgHandlerClient client = new CoreAPIMsgHandler.CoreAPIMsgHandlerClient();
            try
            {
                SendMessageToCoreAPIMSMQ();

                //CoreAPIMSMQHandler.CoreAPIMsgHandlerClient client = new CoreAPIMSMQHandler.CoreAPIMsgHandlerClient();
             
                //client.QueueMessage("MSMQTest", SampleXMLMsg());
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            
            int i = 0;
            i++;
        }

        private static void SendMessageToCoreAPIMSMQ()
        {
            int userid = 100;
            long longRwVersion = 2676634;
            //string xmlMsg = CoreAPIMSMQMessage.GetUserSvcMessage(userid, longRwVersion, CoreAPIMSMQEnums.EnumUserSvc.UserUpdate);
            string xmlMsg =  SampleXMLMsg();
            try
            {
                // log incoming message
                //APIMessageLogger.LogAPIMessage(0, "REQUEST", msg, Label, "Core.API.MSMQ.CoreAPIMessageHandler.QueueMessage", 0);

                using (Message queueMsg = new System.Messaging.Message())
                {
                    queueMsg.Label = "TestLabel";
                    queueMsg.Body = xmlMsg;
                    queueMsg.UseDeadLetterQueue = true;
                    string connection = string.Format("{0}{1}{2}{3}", "FormatName:Direct=OS:", "testserver-ts03", @"\private$\", "corehh2api");
                    MessageQueue msgQ = new MessageQueue(connection);
                    if (!string.IsNullOrEmpty(msgQ.MachineName) && !string.IsNullOrEmpty(msgQ.QueueName))
                    {
                        msgQ.Send(queueMsg);
                        //MessageQueue msgQ = new MessageQueue(@"FormatName:Direct=OS:TESTServer12\private$\corehh2api");
                        //msgQ.MachineName = "TESTServer12";
                    }

                    //return;
                }
            }
            catch (Exception ex)
            {
                string msg = "";
                msg = ex.Message;
                //TSErrorLogger.LogError("MSMQ.CoreAPIMessageHandler", "QueueMessage", string.IsNullOrEmpty(ex.Message) ? "" : ex.Message, (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message)) ? ex.InnerException.Message : "", string.IsNullOrEmpty(ex.StackTrace) ? "" : ex.StackTrace, "");
            }

            //if (!string.IsNullOrEmpty(xmlMsg))
            //{
            //    try
            //    {
            //        CoreAPIMSMQSvc.CoreAPIMsgHandlerClient client = new CoreAPIMSMQSvc.CoreAPIMsgHandlerClient();
            //        client.QueueMessage(CoreAPIMSMQEnums.EnumUserSvc.UserUpdate.ToString(), xmlMsg);
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}
        }
        private static string SampleXMLMsg()
        {
            string msg = @"<xml> 
                                <Name>Test Test</Name> 
                           </xml>";

            return msg;
        }
    }
}
