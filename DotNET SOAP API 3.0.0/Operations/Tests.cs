using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNET_SOAP_API_3._0._0
{
    public static class Tests
    {
        public static void AddRelatedAccounts(string accessToken)
        {
            // Step 1: add a service provider
            var serviceProviderId = ServiceProviderOperations.AddServiceProviderAccount(accessToken);
            //
            // Step 2: set the service provider permissions and limits
            ServiceProviderOperations.SetServiceProviderAccountPermissionAndLimits(accessToken, serviceProviderId);
            //
            // Step 3: add an organization to the service provider created at the Step 1.
            var organizationID = OrganizationOperations.AddOrganizationAccount(accessToken, serviceProviderId);
            //
            // Step 4: set the organization permissions and limits
            OrganizationOperations.SetOrganizationAccountPermissionsAndLimits(accessToken, organizationID);
            //
            // Step 5: add an user to the organization created at the Step 3.
            var userID = UserOperations.AddUserAccount(accessToken, organizationID);
            //
            // Step 6: set the user permissions and limits
            UserOperations.SetUserAccountPermissionsAndLimits(accessToken, userID);
            //
            // Step 7: add an extension to the user created at the Step 5.
            ExtensionOperations.AddExtensionAccount(accessToken, userID);            

            Console.Read();
        }

        public static void AddServiceProviderAccount(string accessToken)
        {
            ServiceProviderOperations.AddServiceProviderAccount(accessToken);

            Console.Read();
        }

        public static void SetServiceProviderAccountPermissionsAndLimits(string accessToken)
        {
            string serviceProviderId = "CHANGEME";

            ServiceProviderOperations.SetServiceProviderAccountPermissionAndLimits(accessToken, serviceProviderId);

            Console.Read();
        }

        public static void AddOrganizationAccount(string accessToken)
        {
            string serviceProviderId = "CHANGEME";

            OrganizationOperations.AddOrganizationAccount(accessToken, serviceProviderId);

            Console.Read();
        }

        public static void SetOrganizationAccountPermissionsAndLimits(string accessToken)
        {
            string organizationId = "CHANGEME";

            OrganizationOperations.SetOrganizationAccountPermissionsAndLimits(accessToken, organizationId);

            Console.Read();
        }

        public static void AddUserAccount(string accessToken)
        {
            string organizationID = "CHANGEME";

            UserOperations.AddUserAccount(accessToken, organizationID);

            Console.Read();
        }

        public static void SetUserAccountPermissionsAndLimits(string accessToken)
        {
            string userID = "CHANGEME";

            UserOperations.SetUserAccountPermissionsAndLimits(accessToken, userID);

            Console.Read();
        }

        public static void AddExtensionAccount(string accessToken)
        {
            string userID = "CHANGEME";

            ExtensionOperations.AddExtensionAccount(accessToken, userID);

            Console.Read();
        }

        public static void GetCallCosts(string accessToken)
        {
            string userID = "CHANGEME";

            CallCostsOperations.GetCallCosts(accessToken, userID);

            Console.Read();
        }
    }
}
