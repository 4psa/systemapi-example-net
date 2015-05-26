using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using voipnowsoap_net;
using System.ComponentModel;

namespace DotNET_SOAP_API_3._0._0
{
    static class ExtensionOperations
    {
        #region AddExtensionAccount operation

        public static void AddExtensionAccount(string accessToken, string parentUserID)
        {
            if (string.IsNullOrEmpty(parentUserID))
            {
                Console.WriteLine("The parentUserID parameter cannot be null or empty");

                return;
            }

            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("The access token cannot be null or empty!");

                return;
            }

            ServicePointManager.ServerCertificateValidationCallback += delegate(object sender,
                X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            ServicePointManager.Expect100Continue = false;

            userCredentials credentials = new userCredentials() { accessToken = accessToken };

            AddExtension request = CreateExtensionRequest(new Random(), parentUserID);
            AddExtensionResponse response = new AddExtensionResponse();

            ExtensionClient extension = new ExtensionClient("ExtensionPort");

            Console.WriteLine("Adding the Extension with the label = {0}.", request.label);
            try
            {
                extension.AddExtension(credentials, request, out response);
            }
            catch (Exception e)
            {
                //exception found, so we check the stack trc
                String trace = e.StackTrace;

                //write the stack trace to the console                
                Console.WriteLine("{0} Exception caught.", e);                

                //wait for the user to press a key before closing the console
                Console.Read();
            }
            finally
            {
                Console.WriteLine("The operation response:");

                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(response))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(response);
                    Console.WriteLine("\t{0}={1}", name, value);
                }

                Console.WriteLine();
            }
        }

        private static AddExtension CreateExtensionRequest(Random rand, string parentUserID)
        {
            AddExtension request = new AddExtension();

            request.extensionNo = rand.Next(0, 1000).ToString();
            request.extensionType = extensionType1.term; // any extension type can be assigned.
            request.extensionTypeSpecified = true;
            request.label = rand.Next().ToString();
            request.Item = parentUserID;
            request.ItemElementName = ItemChoiceType55.parentID;            

            return request;
        }

        #endregion AddExtensionAccount operation
    }
}
