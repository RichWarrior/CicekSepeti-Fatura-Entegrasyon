using System;
using System.Threading;
using System.Threading.Tasks;

namespace EArchive.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            DraftInvoiceConsumer draftInvoiceConsumer = new();
            DownloadInvoiceConsumer downloadInvoiceConsumer = new();
            for (; ; )
            {
                Thread.Sleep(1000);
            }
        }
    }
}
