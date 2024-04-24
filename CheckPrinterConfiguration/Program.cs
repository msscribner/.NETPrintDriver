using System;
using System.Net.Http;
using System.Threading.Tasks;

class IPPPrinterQuery
{
    static async Task Main(string[] args)
    {
        //      string printerURI = "http://printer.example.com/ipp"; // Replace with the actual IPP endpoint of your printer
        //      string printerURI = "http://[fe80::c665:16ff:fe3e:dba3%5]:3911/";
        //string printerURI = "http://192.168.1.8:3911/printers/HP Color LaserJet M452dn UPD PCL 6";
        string printerURI = "http://192.168.1.8";



        // Create HttpClient instance
        using (HttpClient client = new HttpClient())
        {
            // Define the attributes you want to query (e.g., printer name, supported features, etc.)
            string requestBody = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                  "<ipp:operation xmlns:ipp=\"http://www.pwg.org/ipp\">" +
                                  "    <ipp:operation-attributes>" +
                                  "        <ipp:attributes-charset charset=\"utf-8\"/>" +
                                  "        <ipp:attributes-natural-language natural-language=\"en\"/>" +
                                  "        <ipp:requested-attributes>printer-name printer-info printer-make-and-model printer-state printer-state-reasons printer-uri-supported</ipp:requested-attributes>" +
                                  "    </ipp:operation-attributes>" +
                                  "</ipp:operation>";

            // Create the HTTP request
            var request = new HttpRequestMessage(HttpMethod.Post, printerURI);
            request.Content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/ipp");

            // Send the request and get the response
            var response = await client.SendAsync(request);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Printer attributes:");
                Console.WriteLine(responseContent); // Output the response content (XML or JSON)
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}
