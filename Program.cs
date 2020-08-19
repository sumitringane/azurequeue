using System; // Namespace for Console output
using System.Configuration; // Namespace for ConfigurationManager
using System.Threading.Tasks; // Namespace for Task
using Azure.Storage.Queues; // Namespace for Queue storage types
using Azure.Storage.Queues.Models; // Namespace for PeekedMessage
namespace console_storage_sent_msg
{
    class Program
    {
        private static string queue_connection_string = "Please past connection string here from application token";
        private static string queue_name = "appqueue";
        
        static void Main(string[] args)
        {        
            //Console.WriteLine(InsertMsgQueue());
            //Console.WriteLine(PeekMsgQueue());
            //Console.WriteLine(UpdateMsgByIdQueue());
            //Console.WriteLine(DeleteMsgByIdQueue());
            //Console.WriteLine(DeleteQueue());
            Console.WriteLine(CreateQueue());
            Console.ReadLine();
        
        }
        /// <summary>
        /// For Insert msg in Azure queue storage.
        /// </summary>
        /// <returns></returns>
        public static string InsertMsgQueue()
        {           
        QueueClient queueClient = new QueueClient(queue_connection_string, queue_name);    
        if (queueClient.Exists())
        {
             for(int i=0; i<10 ; i++)
             {
                 
                order msg = new order();    

                 queueClient.SendMessage(msg.ToString());
             }            
        }
           return "All messages sent";
        }
        /// <summary>
        /// Peek up massage from Azure Queue storage.
        /// </summary>
        /// <returns></returns>
        public static string PeekMsgQueue()
        {           
        QueueClient queueClient = new QueueClient(queue_connection_string, queue_name); 
        string msg = string.Empty;   
        string MsgId = string.Empty;
        if (queueClient.Exists())
        {
            // Peek at the next message
             PeekedMessage[] peekedMessage = queueClient.PeekMessages();
            msg =peekedMessage[0].MessageId+' '+ peekedMessage[0].MessageText;
        }
           return msg;
        }
       /// <summary>
       /// Update message in Azure Queue Storage
       /// </summary>
       /// <returns></returns>
       public static string DeleteMsgByIdQueue()
       {
           QueueClient queueClient = new QueueClient(queue_connection_string, queue_name); 
            if (queueClient.Exists())
            {                
                // Get the next message
                QueueMessage[] retrievedMessage = queueClient.ReceiveMessages();
                
                if(retrievedMessage.Length >0)
                        queueClient.DeleteMessage(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);               
                
            }
            return "Message delete Successfully";
       }
        /// <summary>
        /// delete Queue from Azure Storage account.
        /// </summary>
        /// <returns></returns>
       public static string DeleteQueue()
       {
           QueueClient queueClient = new QueueClient(queue_connection_string, queue_name); 
            string msg = string.Empty;   
            if (queueClient.Exists())
            {
                queueClient.Delete();
            }
            return "Queue delete successfully";
       }
        /// <summary>
        /// Massage update in AZure Queue Storage.
        /// </summary>
        /// <returns></returns>
        public static string UpdateMsgByIdQueue()
        {
             QueueClient queueClient = new QueueClient(queue_connection_string, queue_name); 
            if (queueClient.Exists())
            {                
                // Get the next message
                QueueMessage[] retrievedMessage = queueClient.ReceiveMessages();
                
                if(retrievedMessage.Length >0)
                        queueClient.UpdateMessage(retrievedMessage[0].MessageId,retrievedMessage[0].PopReceipt, "Record update successfully.");               
                
            }
            return "Message update Successfully";
        }
        /// <summary>
        /// Create Queue dynamically.
        /// </summary>
        /// <returns></returns>
        public static string CreateQueue()
        {
            try{
                
                // Instantiate a QueueClient which will be used to create and manipulate the queue
                QueueClient queueClient = new QueueClient(queue_connection_string, "myqueue");

                // Create the queue
                queueClient.CreateIfNotExists();
            
            }catch(Exception ex)
            {
                Console.WriteLine("Exception message="+ex.Message);
            }
            return "Queue create in AZure storage account";

        }
    }
}
