using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Amazon.SimpleEmail.Internal;

namespace BlazorComms.Services
{
    public class Email
    {

        public static async void SendAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    string source = "ryan@campuspoint.com";
                    using (var client = new AmazonSimpleEmailServiceClient(
                            CampusPoint.AWS.EmailServices.AwsAccessKey,
                            CampusPoint.AWS.EmailServices.AwsSecretKey,
                            CampusPoint.AWS.EmailServices.Region)
                        )
                    {
                        // Create a send email message request

                        var dest = new Destination(new List<string> { source });
                        var subject = new Content("Testing Amazon SES through the API");
                        var textBody = new Content("This is a test message sent by Amazon SES from the AWS SDK for .NET.");
                        var body = new Body(textBody);
                        var message = new Message(subject, body);
                        var request = new SendEmailRequest(source, dest, message);

                        // Send the email to recipients via Amazon SES
                        //Console.WriteLine($"Sending message to {source}");
                        var response = client.SendEmailAsync(request);
                        //var response = await client.SendEmailAsync(request);
                        //Console.WriteLine($"Response - HttpStatusCode: {response.HttpStatusCode}, MessageId: {response.MessageId}");
                    }
                }
                catch (Exception ex) { 
                    // Log error to SignalR console then throw

                    throw; 
                }

            });
        }
    }
}
