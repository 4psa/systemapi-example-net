using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNET_SOAP_API_3._0._0
{
    class Program
    {
        static void Main(string[] args)
        {
            // Change this with the oauth token of the account you want to use.
            string accessToken = "CHANGEME";

            Tests.AddRelatedAccounts(accessToken);

            // Uncomment the following line if you want to run this test
            //Tests.AddServiceProviderAccount(accessToken);

            // Uncomment the following line if you want to run this test
            //Tests.AddOrganizationAccount(accessToken);

            // Uncomment the following line if you want to run this test
            //Tests.AddUserAccount(accessToken);

            // Uncomment the following line if you want to run this test
            //Tests.AddExtensionAccount(accessToken);
        }
    }
}
