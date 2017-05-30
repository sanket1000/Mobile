using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using Core.API.Utilities;
using Core.API.Utilities.Helpers;

namespace Core.API.MSMQ
{
    public class CoreAPIMessageHandler
    {
        public static void QueueMessage (string MachineName, string QueueName, string Label, string Message)
        {
           
            try
            {
                // log incoming message
                string msg = "MachineName= " + (string.IsNullOrEmpty(MachineName) ? "" : MachineName)
                + " QueueName= " + (string.IsNullOrEmpty(QueueName) ? "" : QueueName)
                + " Label= " + (string.IsNullOrEmpty(Label) ? "" : Label)
                + " Message= " + (string.IsNullOrEmpty(Message) ? "" : Message);

                APIMessageLogger.LogAPIMessage(0, "REQUEST", msg, Label, "Core.API.MSMQ.CoreAPIMessageHandler.QueueMessage", 0);

                if (string.IsNullOrEmpty(MachineName) || string.IsNullOrEmpty(QueueName))
                {
                    //|| string.IsNullOrEmpty(Label) || string.IsNullOrEmpty(Message)

                    APIMessageLogger.LogAPIMessage(0, "REQUEST", "Error - MSMQ machine name or queue name is empty", Label, "Core.API.MSMQ.CoreAPIMessageHandler.QueueMessage", 0);
                    return;
                }

               
                using (Message queueMsg = new System.Messaging.Message())
                {
                    queueMsg.Label = Label;
                    queueMsg.Body = Message;
                    queueMsg.UseDeadLetterQueue = true;
                    string connection = string.Format("{0}{1}{2}{3}", "FormatName:Direct=OS:", MachineName, @"\private$\", QueueName);
                    MessageQueue msgQ = new MessageQueue(connection);
                    if (!string.IsNullOrEmpty(msgQ.MachineName) && !string.IsNullOrEmpty(msgQ.QueueName))
                    {
                        msgQ.Send(queueMsg);
                        //MessageQueue msgQ = new MessageQueue(@"FormatName:Direct=OS:TESTServer12\private$\corehh2api");
                        //msgQ.MachineName = "TESTServer12";
                    }
                    else
                    {
                        APIMessageLogger.LogAPIMessage(0, "REQUEST", "Error - Invalid MSMQ machine name or queue name", Label, "Core.API.MSMQ.CoreAPIMessageHandler.QueueMessage", 0);
                    }
                    //return;
                }
            }
            catch (Exception ex)
            {
                TSErrorLogger.LogError("Core.API.MSMQ.CoreAPIMessageHandler", "QueueMessage", (string.IsNullOrEmpty(ex.Message) ? "" : ex.Message) + (string.IsNullOrEmpty(ex.StackTrace) ? "" : "  " + ex.StackTrace), (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message)) ? ex.InnerException.Message : "", "Error - Invalid MSMQ machine name or queue name", "");
            }
        }
    }
}
