using System;
using System.Collections.Generic;
using System.Web.Http;
using CustomerProjectOrder.Common.Logger;
using CustomerProjectOrder.DataLayer.Entities;
using DenodoAdapter;
using CustomerProjectOrder.DataLayer.Interfaces;
using System.Linq;

namespace CustomerProjectOrder.DataLayer
{

    public interface ITest
    { string DomainUri { get; set; } }
    public class DataLayerContext : IDataLayerContext, ITest
    {

        private const string LIKE_OPERATOR = "like";
        private const string PROJECTNUMBER_FIELD = "pr01001";
        private const string PROJECTNAME_FIELD = "pr01009";
        private const string PROJECTSTART_FIELD = "pr01067";
        private const string PROJECTEND_FIELD = "pr01069";
        private const string CUSTOMER_PO_NUMBER = "pr01106";
        private const string CUSTOMER_ACCOUNT_NUMBER = "pr01003";
        private const string COMPANYCODE_PLACEHOLDER = "{CompanyCode}";
        private readonly string _denodoUrl;
        private readonly IDenodoContext _denodoContext;
        private readonly ConfigReader _configReader;

        public DataLayerContext()
        {
            try
            {
                _configReader = new ConfigReader();
                _denodoContext = new DenodoContext(_configReader.BaseUri, _configReader.DenodoUsername,
                    _configReader.DenodoPassword);
                _denodoUrl = _configReader.BaseUri;
            }
            catch (Exception exception)
            {
                ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                    exception.InnerException);
                throw;
            }
        }

        public DataLayerContext(ConfigReader configReader, IDenodoContext denodoContext)
        {
            _configReader = configReader;
            _denodoContext = denodoContext;
            _denodoUrl = _configReader.BaseUri;
        }

        public string DomainUri { get; set; }

        public CustomerProjectOrderHeader GetProjectByAccount(string companyCode, string account)
        {
            try
            {
                string companyViewUri = _denodoUrl + _configReader.GetDenodoViewUri(companyCode);
                string filter = $"{CUSTOMER_ACCOUNT_NUMBER}='{account}'";
                ApplicationLogger.InfoLogger($"DataLayer :: GetOrderByName :: Getting Order by name [{CUSTOMER_ACCOUNT_NUMBER}] with company code [{companyCode}] from denodo url: {companyViewUri} with filter: {filter}");
                var lstOfCustomerProjectOrderHeader = _denodoContext.SearchData<CustomerProjectOrderHeader>(companyViewUri, filter);
                ApplicationLogger.InfoLogger($"Orders count: {lstOfCustomerProjectOrderHeader.Count}");
                return lstOfCustomerProjectOrderHeader.FirstOrDefault();
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public CustomerProjectOrderHeader GetProjectByCustomerPONo(string companyCode, string customerPONo)
        {
            try
            {
                string companyViewUri = _denodoUrl+ _configReader.GetDenodoViewUri(companyCode);
                string filter = $"{CUSTOMER_PO_NUMBER}='{customerPONo}'";
                ApplicationLogger.InfoLogger($"DataLayer :: GetOrderByName :: Getting Order by name [{customerPONo}] with company code [{companyCode}] from denodo url: {companyViewUri} with filter: {filter}");
                var lstOfCustomerProjectOrderHeader = _denodoContext.SearchData<CustomerProjectOrderHeader>(companyViewUri, filter);
                ApplicationLogger.InfoLogger($"Orders count: {lstOfCustomerProjectOrderHeader.Count}");
                return lstOfCustomerProjectOrderHeader.FirstOrDefault();
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public List<CustomerProjectOrderHeader> GetProjectByDuration(string companyCode, string startDate, string endDate)
        {
            try
            {
                string companyViewUri = _denodoUrl+ _configReader.GetDenodoViewUri(companyCode);
                string filter = $"{PROJECTSTART_FIELD}>='{startDate}' AND {PROJECTEND_FIELD}<='{endDate}'";
                ApplicationLogger.InfoLogger($"DataLayer :: GetOrderByName :: Getting Order by start date [{startDate}] and end date[{endDate}] with company code [{companyCode}] from denodo url: {companyViewUri} with filter: {filter}");
                var lstOfCustomerProjectOrderHeader = _denodoContext.SearchData<CustomerProjectOrderHeader>(companyViewUri, filter);
                ApplicationLogger.InfoLogger($"Orders count: {lstOfCustomerProjectOrderHeader.Count}");
                return lstOfCustomerProjectOrderHeader;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public List<CustomerProjectOrderHeader> GetProjectByName(string companyCode, string projectName)
        {
            try
            {
                string companyViewUri = _denodoUrl+ _configReader.GetDenodoViewUri(companyCode);
                string filter = $"{PROJECTNAME_FIELD} {LIKE_OPERATOR} '%{projectName}%'";
                ApplicationLogger.InfoLogger($"DataLayer :: GetOrderByName :: Getting Order by name [{projectName}] with company code [{companyCode}] from denodo url: {companyViewUri} with filter: {filter}");
                var lstOfCustomerProjectOrderHeader = _denodoContext.SearchData<CustomerProjectOrderHeader>(companyViewUri, filter);
                ApplicationLogger.InfoLogger($"Orders count: {lstOfCustomerProjectOrderHeader.Count}");
                return lstOfCustomerProjectOrderHeader;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public CustomerProjectOrderHeader GetProjectByNumber(string companyCode, string projectNumber)
        {
            try
            {
                string companyViewUri = _denodoUrl+ _configReader.GetDenodoViewUri(companyCode);
                string filter = $"{PROJECTNUMBER_FIELD}='{projectNumber}'";
                ApplicationLogger.InfoLogger($"DataLayer :: GetOrderByName :: Getting Order by name [{projectNumber}] with company code [{companyCode}] from denodo url: {companyViewUri} with filter: {filter}");
                var customerProjectOrderHeader = _denodoContext.SearchData<CustomerProjectOrderHeader>(companyViewUri, filter);
                return customerProjectOrderHeader.FirstOrDefault();
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public List<CustomerProjectOrderHeader> GetProjectByCompanyCode(string companyCode)
        {
            try
            {
                string companyViewUri = _denodoUrl + _configReader.GetDenodoViewUri(companyCode);
                ApplicationLogger.InfoLogger($"DataLayer :: GetOrderByName :: Getting Order by  company code [{companyCode}] from denodo url: {companyViewUri}");
                var lstOfCustomerProjectOrderHeader = _denodoContext.GetData<CustomerProjectOrderHeader>(companyViewUri);
                ApplicationLogger.InfoLogger($"Orders count: {lstOfCustomerProjectOrderHeader.Count}");
                return lstOfCustomerProjectOrderHeader;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        private static void LogException(Exception exception)
        {
            if (exception.GetType() == typeof(HttpResponseException))
            {
                HttpResponseException responseException = (HttpResponseException)exception;
                ApplicationLogger.Errorlog(responseException.Response.ReasonPhrase, Category.Database,
                    responseException.Response.Content.ReadAsStringAsync().Result, responseException.InnerException);
                ApplicationLogger.InfoLogger(
                    $"Denodo Adapter exception :: {responseException.Response.ReasonPhrase}");
                throw responseException;
            }
            ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                exception.InnerException);
        }
    }
}
