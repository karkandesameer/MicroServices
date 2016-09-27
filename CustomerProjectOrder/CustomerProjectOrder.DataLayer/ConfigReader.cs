﻿using System.Configuration;
using Configuration = CustomerProjectOrder.Common.Configuration;

namespace CustomerProjectOrder.DataLayer
{
    public class ConfigReader
    {
        private readonly bool _readFromDatabase;
        private const string BASEURI_KEY = "BaseUri";
        private const string DENODO_USERNAME_KEY = "DenodoUsername";
        private const string DENODO_PASSWORD_KEY = "DenodoPassword";
        private const string CustomerProjectOrder_VIEWURI_KEY = "CustomerProjectOrderViewUri";
        private string ServiceName { get; }
        private string Environment { get; }
        public string ConfigurationDbConnectionString { get; set; }
        public string BaseUri { get; set; }
        public string DenodoUsername { get; private set; }
        public string DenodoPassword { get; private set; }
        public ConfigReader()
        {
            _readFromDatabase = bool.Parse(ReadConfig("ReadConfigFromDatabase"));
            ServiceName = ReadConfig("ServiceName");
            Environment = ReadConfig("Environment");

            if (_readFromDatabase)
                InitializeFromDatabase();
            else
                InitializeFromConfig();
        }
        private string ReadConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        private void InitializeFromConfig()
        {
            BaseUri = ReadConfig(BASEURI_KEY);
            DenodoUsername = ReadConfig(DENODO_USERNAME_KEY);
            DenodoPassword = ReadConfig(DENODO_PASSWORD_KEY);
        }
        private void InitializeFromDatabase()
        {
            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            var configurationDictionary = configuration.GetConfiguration(ServiceName, Environment);
            BaseUri = configurationDictionary[BASEURI_KEY];
            DenodoUsername = configurationDictionary[DENODO_USERNAME_KEY];
            DenodoPassword = configurationDictionary[DENODO_PASSWORD_KEY];
        }

        public string GetDenodoViewUri(string companyCode)
        {
            if (!_readFromDatabase)
                return ReadConfig($"{CustomerProjectOrder_VIEWURI_KEY}_{companyCode.ToLower()}");

            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            return configuration.GetDenodoViewUri(ServiceName, Environment, companyCode, CustomerProjectOrder_VIEWURI_KEY);
        }
    }
}
