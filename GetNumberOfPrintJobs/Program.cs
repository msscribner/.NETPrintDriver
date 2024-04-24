using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Printing;
using System.IO;


namespace GetNumberOfPrintJobs
{
    internal class Program
    {
        //https://www.codeproject.com/Tips/1169564/Csharp-Print-Reports-Document-on-Server-local-usin

        static void Main(string[] args)
        {
            CheckPrinterConfiguration("", "");
            GetPrintTicketFromPrinter();
        }


            public static string CheckPrinterConfiguration(string printerIP, string printerName)
            {
                var server = new PrintServer();
                var queues = server.GetPrintQueues(new[]
                    { 
                        EnumeratedPrintQueueTypes.Local, 
                        EnumeratedPrintQueueTypes.Connections 
                    });

                foreach (var queue in queues) 
                {

                }

                string fulllName = queues.Where(q => q.Name == printerName && q.QueuePort.Name == printerIP).Select(q => q.FullName).FirstOrDefault();

                return fulllName;
            }


        // ---------------------- GetPrintTicketFromPrinter -----------------------
        /// <summary>
        ///   Returns a PrintTicket based on the current default printer.</summary>
        /// <returns>
        ///   A PrintTicket for the current local default printer.</returns>
        private static PrintTicket GetPrintTicketFromPrinter()
        {
            PrintQueue printQueue = null;

            LocalPrintServer localPrintServer = new LocalPrintServer();

            // Retrieving collection of local printer on user machine
            PrintQueueCollection localPrinterCollection =
                localPrintServer.GetPrintQueues();

            System.Collections.IEnumerator localPrinterEnumerator =
                localPrinterCollection.GetEnumerator();

            if (localPrinterEnumerator.MoveNext())
            {
                // Get PrintQueue from first available printer
                printQueue = (PrintQueue)localPrinterEnumerator.Current;
            }
            else
            {
                // No printer exist, return null PrintTicket
                return null;
            }

            // Get default PrintTicket from printer
            PrintTicket printTicket = printQueue.DefaultPrintTicket;

            PrintCapabilities printCapabilities = printQueue.GetPrintCapabilities();

            // Modify PrintTicket
            if (printCapabilities.CollationCapability.Contains(Collation.Collated))
            {
                printTicket.Collation = Collation.Collated;
            }

            if (printCapabilities.DuplexingCapability.Contains(
                    Duplexing.TwoSidedLongEdge))
            {
                printTicket.Duplexing = Duplexing.TwoSidedLongEdge;
            }

            if (printCapabilities.StaplingCapability.Contains(Stapling.StapleDualLeft))
            {
                printTicket.Stapling = Stapling.StapleDualLeft;
            }

            return printTicket;
        }// end:GetPrintTicketFromPrinter()
    }
}
