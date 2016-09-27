using System.Collections.Generic;
using CustomerProjectOrder.DataLayer;
using CustomerProjectOrder.DataLayer.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using DenodoAdapter;
using System.Web.Http;

namespace CustomerProjectOrder.UnitTest
{
    [TestClass]
    public class DataLayerContextUnitTest
    {
        #region "Members"
        private const string LIKE_OPERATOR = "like";
        private const string PROJECTNUMBER_FIELD = "pr01001";
        private const string PROJECTNAME_FIELD = "pr01009";
        private const string PROJECTSTART_FIELD = "pr01067";
        private const string PROJECTEND_FIELD = "pr01069";
        private const string CUSTOMER_PO_NUMBER = "pr01106";
        private const string CUSTOMER_ACCOUNT_NUMBER = "pr01003";
        private readonly string _companyCode = "j4";
        private readonly List<CustomerProjectOrderHeader> _customerProjectOrderHeaders = new List<CustomerProjectOrderHeader>();
        private ConfigReader _configReader;
        #endregion

        #region "Initialization"
        /// <summary>
        /// Initialize all the required resources
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            var spec = MockRepository.GenerateMock<IDenodoContext>();
            _configReader = new ConfigReader();
        }
        #endregion

        #region "Unit Test Methods"
        /// <summary>
        /// Unit testing Get all project orders for Company code
        /// </summary>
        [TestMethod]
        public void GetProjectOrdersByCompanyCodeTest()
        {
            SetMockDataForCustomerProjectOrderHeaders();
            var mocks = MockRepository.GenerateMock<IDenodoContext>();

            mocks.Stub(x => x.GetData<CustomerProjectOrderHeader>(_configReader.GetDenodoViewUri(_companyCode)))
                .IgnoreArguments()
                .Return(_customerProjectOrderHeaders);

            var datalayer = new DataLayerContext(_configReader, mocks);
            var result = datalayer.GetProjectByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get Project Order by Project Order Number
        /// </summary>
        [TestMethod]
        public void GetProjectOrderByProjectNumberTest()
        {
            string projectNumber = "M100030010";
            SetMockDataForCustomerProjectOrderHeaders();
            var mocks = MockRepository.GenerateMock<IDenodoContext>();
            string filter = $"{PROJECTNUMBER_FIELD}='{projectNumber}'";
            mocks.Stub(x => x.SearchData<CustomerProjectOrderHeader>(_configReader.GetDenodoViewUri(_companyCode),filter))
                .IgnoreArguments()
                .Return(_customerProjectOrderHeaders);

            var datalayer = new DataLayerContext(_configReader, mocks);
            var result = datalayer.GetProjectByNumber(_companyCode, projectNumber);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get Project Order by Project Order Name
        /// </summary>
        [TestMethod]
        public void GetProjectOrderByProjectNameTest()
        {
            string projectName = "INTE";
            SetMockDataForCustomerProjectOrderHeaders();
            var mocks = MockRepository.GenerateMock<IDenodoContext>();

            mocks.Stub(x => x.GetData<CustomerProjectOrderHeader>(_configReader.GetDenodoViewUri(_companyCode)))
                .IgnoreArguments()
                .Return(_customerProjectOrderHeaders);

            var datalayer = new DataLayerContext(_configReader, mocks);
            var result = datalayer.GetProjectByName(_companyCode, projectName);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get Project Order by Account
        /// </summary>
        [TestMethod]
        public void GetProjectOrderByAccountTest()
        {
            string account = "PL001";
            SetMockDataForCustomerProjectOrderHeaders();
            var mocks = MockRepository.GenerateMock<IDenodoContext>();

            mocks.Stub(x => x.GetData<CustomerProjectOrderHeader>(_configReader.GetDenodoViewUri(_companyCode)))
                .IgnoreArguments()
                .Return(_customerProjectOrderHeaders);

            var datalayer = new DataLayerContext(_configReader, mocks);
            var result = datalayer.GetProjectByAccount(_companyCode, account);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get Project Order by Customer PO No
        /// </summary>
        [TestMethod]
        public void GetProjectOrderByCustomerPONoTest()
        {
            string customerPONo = "TH618000199004";
            SetMockDataForCustomerProjectOrderHeaders();
            var mocks = MockRepository.GenerateMock<IDenodoContext>();

            mocks.Stub(x => x.GetData<CustomerProjectOrderHeader>(_configReader.GetDenodoViewUri(_companyCode)))
                .IgnoreArguments()
                .Return(_customerProjectOrderHeaders);

            var datalayer = new DataLayerContext(_configReader, mocks);
            var result = datalayer.GetProjectByCustomerPONo(_companyCode, customerPONo);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get Project Orders by Duration
        /// </summary>
        [TestMethod]
        public void GetProjectOrdersByDurationTest()
        {
            string startDate = "2010-12-14";
            string endDate = "2011-02-28";
            SetMockDataForCustomerProjectOrderHeaders();
            var mocks = MockRepository.GenerateMock<IDenodoContext>();

            mocks.Stub(x => x.GetData<CustomerProjectOrderHeader>(_configReader.GetDenodoViewUri(_companyCode)))
                .IgnoreArguments()
                .Return(_customerProjectOrderHeaders);

            var datalayer = new DataLayerContext(_configReader, mocks);
            var result = datalayer.GetProjectByDuration(_companyCode, startDate,endDate);
            Assert.IsNotNull(result);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// To create a mock data of customer project order header for unit test
        /// </summary>
        private void SetMockDataForCustomerProjectOrderHeaders()
        {
            #region SampleCustomerProjectOrderHeaders
            _customerProjectOrderHeaders.Add(new CustomerProjectOrderHeader()
            {
                pr01001 = "M100030010",
                pr01003 = "PL001",
                pr01106 = "7000128779",
                pr01009 = "INTEGATION CARRIER",
                pr01010 = "MDR - Residual Value",
                pr01011 = "JCC-11-1120-SIY-01",
                pr01067 = "2010-12-14 00:00:00.0",
                pr01069 = "2011-02-28 00:00:00.0",
            });
            _customerProjectOrderHeaders.Add(new CustomerProjectOrderHeader()
            {
                pr01001 = "P000030020",
                pr01003 = "NX001",
                pr01106 = "TH618000199004",
                pr01009 = "NXP-Install Tripod&Access",
                pr01010 = "SYS PRIME RETROFIT",
                pr01011= "TH92-4240-Q534-R1",
                pr01067 = "2009-09-29 00:00:00.0",
                pr01069 = "2009-12-30 00:00:00.0"
            });
            _customerProjectOrderHeaders.Add(new CustomerProjectOrderHeader()
            {
                pr01001 = "P200050060",
                pr01003 = "SE003",
                pr01106 = "SK1260221",
                pr01009 = "Fire alarm control panel",
                pr01010 = "SYS PRIME RETROFIT",
                pr01011 = "JCC-11-0509-SIR-01",
                pr01067 = "2011-08-09 00:00:00.0",
                pr01069 = "2011-08-10 00:00:00.0"
            });
            #endregion
        }

        #endregion
    }
}
