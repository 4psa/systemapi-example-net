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
    static class CallCostsOperations
    {
        #region CallCosts operation

        public static void GetCallCosts(string accessToken, string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                Console.WriteLine("The userID parameter cannot be null or empty");

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

            CallCosts request = CreateCallCostsRequest(userID);
            CallCostsResponse response = new CallCostsResponse();

            ReportClient reportClient = new ReportClient("ReportPort");

            Console.WriteLine("Getting the call costs for the user with the id = {0}", userID);
            try
            {
                reportClient.CallCosts(credentials, request, out response);
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

        private static CallCosts CreateCallCostsRequest(string userID)
        {
            CallCosts request = new CallCosts();

            // Required fields
            request.ItemElementName = ItemChoiceType35.userID;
            request.Item = userID;

            interval callCostsInterval = new interval();
            callCostsInterval.startDate = System.DateTime.Parse("2011-03-01");
            callCostsInterval.endDate = System.DateTime.Parse("2011-04-01");
            request.Items = new object[] { callCostsInterval };
            request.ItemsElementName = new ItemsChoiceType8[] { ItemsChoiceType8.interval };
         
            return request;
        }

        #endregion CallCosts operation
    }
}
