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
    static class UserOperations
    {
        #region AddUserAccount operation

        public static string AddUserAccount(string accessToken, string parentOrganizationID)
        {
            if (string.IsNullOrEmpty(parentOrganizationID))
            {
                Console.WriteLine("The parentOrganizationID parameter cannot be null or empty");

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

            AddUser request = CreateUserRequest(new Random(), parentOrganizationID);
            AddUserResponse response = new AddUserResponse();

            UserClient userClient = new UserClient("UserPort");

            Console.WriteLine("Adding an user");
            try
            {
                userClient.AddUser(credentials, request, out response);                
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

        private static AddUser CreateUserRequest(Random rand, string parentOrganizationID)
        {
            AddUser request = new AddUser();

            // Required fields
            request.name = "UserC#_" + rand.Next().ToString() + rand.Next().ToString();
            request.login = "Admin_" + rand.Next().ToString();
            request.password = "testpassword";
            request.country = "ro";
            request.Item = parentOrganizationID;
            request.ItemElementName = ItemChoiceType50.parentID;

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

        #endregion AddUserAccount operation

        #region SetUserAccountPermissionsAndLimits operation

        public static void SetUserAccountPermissionsAndLimits(string accessToken, string userID)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("The access token cannot be null or empty!");

                return;
            }

            if (string.IsNullOrEmpty(userID))
            {
                Console.WriteLine("The userID parameter cannot be null or empty!");

                return;
            }            

            ServicePointManager.ServerCertificateValidationCallback += delegate(object sender,
                X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            ServicePointManager.Expect100Continue = false;

            userCredentials credentials = new userCredentials() { accessToken = accessToken };

            SetUserPL request = CreateSetUserPLRequest(userID);
            updateObject response = new updateObject();

            UserClient userClient = new UserClient("UserPort");

            Console.WriteLine("Set the Permissions and Limits for the User with the ID = {0}.", userID);
            try
            {
                userClient.SetUserPL(credentials, request, out response);                
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

        private static SetUserPL CreateSetUserPLRequest(string userID)
        {
            SetUserPL request = new SetUserPL();            

            request.Item1 = userID;
            request.Item1ElementName = Item1ChoiceType12.ID;

            // set permissions            
            request.extensionManag = true;
            request.extensionManagSpecified = true;
            request.extFeatureManag = true;
            request.extFeatureManagSpecified = true;

            // set limits            
            request.phoneExtMax = new unlimitedUInt() { unlimited = false, Value = 5 };

            return request;
        }

        #endregion SetUserAccountPermissionsAndLimits operation
    }
}
