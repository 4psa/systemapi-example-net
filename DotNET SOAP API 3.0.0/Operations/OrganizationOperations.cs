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
    static class OrganizationOperations
    {
        #region AddOrganizationAccount operation

        public static string AddOrganizationAccount(string accessToken, string parentServiceProviderID)
        {
            if (string.IsNullOrEmpty(parentServiceProviderID))
            {
                Console.WriteLine("The parentServiceProviderID parameter cannot be null or empty");

                return null;
            }

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

            AddOrganization request = CreateOrganizationRequest(new Random(), parentServiceProviderID);
            AddOrganizationResponse response = new AddOrganizationResponse();

            OrganizationClient organizationClient = new OrganizationClient("OrganizationPort");

            Console.WriteLine("Adding an Organization");

            try
            {
                organizationClient.AddOrganization(credentials, request, out response);
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

            return response.ID;
        }

        private static AddOrganization CreateOrganizationRequest(Random rand, string parentServiceProviderId)
        {
            AddOrganization request = new AddOrganization();

            // Required fields
            request.name = "OrganizationC#_" + rand.Next().ToString() + rand.Next().ToString();
            request.login = "Organization_" + rand.Next().ToString();
            request.password = "testpassword";
            request.country = "ro";
            request.Item = parentServiceProviderId;
            request.ItemElementName = ItemChoiceType7.parentID;

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

        #endregion AddOrganizationAccount operation

        #region SetOrganizationAccountPermissionsAndLimits operation

        public static void SetOrganizationAccountPermissionsAndLimits(string accessToken, string organizationID)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("The access token cannot be null or empty!");

                return;
            }

            if (string.IsNullOrEmpty(organizationID))
            {
                Console.WriteLine("The organizationID parameter cannot be null or empty!");

                return;
            }

            ServicePointManager.ServerCertificateValidationCallback += delegate(object sender,
                X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            ServicePointManager.Expect100Continue = false;

            userCredentials credentials = new userCredentials() { accessToken = accessToken };

            SetOrganizationPL request = CreateSetOrganizationPLRequest(organizationID);
            updateObject response = new updateObject();

            OrganizationClient organizationClient = new OrganizationClient("OrganizationPort");

            Console.WriteLine("Setting the Permissions and Limits for the Organization with the ID = {0}.", organizationID);
            try
            {
                organizationClient.SetOrganizationPL(credentials, request, out response);
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

        private static SetOrganizationPL CreateSetOrganizationPLRequest(string organizationID)
        {
            SetOrganizationPL request = new SetOrganizationPL();

            request.Item1 = organizationID;
            request.Item1ElementName = Item1ChoiceType7.ID;

            // set permissions
            request.permsManag = true;            
            request.extensionManag = true;
            request.extensionManagSpecified = true;
            request.extFeatureManag = true;
            request.extFeatureManagSpecified = true;

            // set limits
            request.userMax = new unlimitedUInt() { unlimited = false, Value = 5 };
            request.phoneExtMax = new unlimitedUInt() { unlimited = false, Value = 5 };


            return request;
        }

        #endregion SetOrganizationAccountPermissionsAndLimits operation
    }
}
