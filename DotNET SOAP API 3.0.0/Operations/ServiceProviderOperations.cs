using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using voipnowsoap_net;
using System.ComponentModel;
using System.Net;

namespace DotNET_SOAP_API_3._0._0
{
    static class ServiceProviderOperations
    {
        #region AddServiceProviderAccount operation

        public static string AddServiceProviderAccount(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("The access token cannot be null or empty!");

                return null;
            }

            ServicePointManager.ServerCertificateValidationCallback += delegate(object sender,
                X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            ServicePointManager.Expect100Continue = false;

            userCredentials credentials = new userCredentials() { accessToken = accessToken };

            AddServiceProvider request = CreateAddServiceProviderRequest(new Random());
            AddServiceProviderResponse response = new AddServiceProviderResponse();

            ServiceProviderClient serviceProviderClient = new ServiceProviderClient("ServiceProviderPort");

            Console.WriteLine("Adding a Service Provider...");

            try
            {
                serviceProviderClient.AddServiceProvider(credentials, request, out response);                
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
                Console.WriteLine("The response for adding a Service Provider:");

                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(response))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(response);
                    Console.WriteLine("\t{0}={1}", name, value);
                }

                Console.WriteLine();
            }            

            return response.ID;
        }

        private static AddServiceProvider CreateAddServiceProviderRequest(Random rand)
        {
            AddServiceProvider request = new AddServiceProvider();

            // Required fields
            request.name = "ServiceProviderC#_" + rand.Next().ToString() + rand.Next().ToString();
            request.login = "Admin_" + rand.Next().ToString();
            request.password = "testpassword";
            request.country = "ro";

            // Optionally fields
            request.company = "Company_" + rand.Next().ToString();
            request.email = CreateEmail(rand);

            return request;
        }

        private static string CreateEmail(Random rand)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("testEmail");
            builder.Append(rand.Next().ToString());
            builder.Append("@test");
            builder.Append(rand.Next().ToString());
            builder.Append(".com");

            return builder.ToString();
        }

        #endregion AddServiceProviderAccount operation

        #region SetServiceProviderAccountPermissionsAndLimits operation

        public static void SetServiceProviderAccountPermissionAndLimits(string accessToken, string serviceProviderID)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("The access token cannot be null or empty!");

                return;
            }

            if (string.IsNullOrEmpty(serviceProviderID))
            {
                Console.WriteLine("The serviceProviderID parameter cannot be null or empty!");

                return;
            }

            ServicePointManager.ServerCertificateValidationCallback += delegate(object sender,
                X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            ServicePointManager.Expect100Continue = false;

            userCredentials credentials = new userCredentials() { accessToken = accessToken };

            SetServiceProviderPL request = CreateSetServiceProviderPLRequest(serviceProviderID);
            updateObject response = new updateObject();

            ServiceProviderClient serviceProviderClient = new ServiceProviderClient("ServiceProviderPort");

            Console.WriteLine("Setting the PL for the Service Provider with the ID = {0}.", serviceProviderID);

            try
            {
                serviceProviderClient.SetServiceProviderPL(credentials, request, out response);
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

        private static SetServiceProviderPL CreateSetServiceProviderPLRequest(string serviceProviderID)
        {
            SetServiceProviderPL request = new SetServiceProviderPL();

            request.Item1 = serviceProviderID;
            request.Item1ElementName = Item1ChoiceType3.ID;

            // set permissions
            request.permsManag = true;
            request.organizationManag = true;
            request.organizationManagSpecified = true;
            request.extensionManag = true;
            request.extensionManagSpecified = true;
            request.extFeatureManag = true;
            request.extFeatureManagSpecified = true;

            // set limits
            
            request.organizationMax = new unlimitedUInt() { unlimited = false, Value = 5 };
            request.userMax = new unlimitedUInt() { unlimited = false, Value = 5 };
            request.phoneExtMax = new unlimitedUInt() { unlimited = false, Value = 5 };

            return request;
        }

        #endregion SetServiceProviderAccountPermissionsAndLimits operation
    }
}
