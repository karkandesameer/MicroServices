using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerProjectOrder.API.Controllers;
using CustomerProjectOrder.DataLayer.Entities;
using CustomerProjectOrder.BusinessLayer.Interface;
using CustomerProjectOrder.Model;
using CustomerProjectOrder.Common.Error;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System;

namespace CustomerProjectOrder.UnitTest
{
    [TestClass]
    public class CustomerProjectOrderControllerTest
    {
        #region Declarations
        private ICustomerProjectOrderManager _iCustomerProjectOrderManager;
        private CustomerProjectOrderController _controller;
        private const string COMPANY_CODE = "bh";
        private string startDate = DateTime.Now.ToString() ;
        private string endDate = DateTime.Now.ToString();

        readonly CustomerProjectOrderModel _customerProjectOrderModel = new CustomerProjectOrderModel();
        readonly List<CustomerProjectOrderModel> _customerProjectOrderList = new List<CustomerProjectOrderModel>();
        readonly List<ErrorInfo> _errorsList = new List<ErrorInfo>();

        #endregion


        [TestInitialize]
        public void Initialize()
        {
            _controller = new CustomerProjectOrderController(_iCustomerProjectOrderManager) { Request = new HttpRequestMessage() };
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:51083/?param1=someValue&param2=anotherValue");
        }


        #region Unit Test Methods

        /// <summary>
        /// Unit Test Method for Get all project of Company
        /// </summary>
        [TestMethod]
        public void GetProjectByCompanyCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerProjectOrderManager>();
            SetMockDataForCustomerProjectModelModels();
            var data = new Model.Response.CustomerProjectOrdersResponse { CustomerProjectOrders = _customerProjectOrderList };
            mockRepository.Stub(x => x.GetProjectByCompanyCode(COMPANY_CODE))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerProjectOrderController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };


            var result = _controller.GetProjectByCompanyCode(COMPANY_CODE);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test Method for Get all project of Company
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetProjectByCompanyCodeTestException()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerProjectOrderManager>();
            SetMockDataForCustomerProjectModelModels();
            var data = new Model.Response.CustomerProjectOrdersResponse { CustomerProjectOrders = _customerProjectOrderList };

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockRepository.Stub(x => x.GetProjectByCompanyCode(COMPANY_CODE))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerProjectOrderController(mockRepository);
            MockControllerRequest();

            var result = _controller.GetProjectByCompanyCode(COMPANY_CODE);
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);

            _controller = new CustomerProjectOrderController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };


            result = _controller.GetProjectByCompanyCode(COMPANY_CODE);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test Method
        /// </summary>
        [TestMethod]
        public void GetProjectByNumberTest()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerProjectOrderManager>();
            SetMockDataForCustomerProjectModelModels();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };
            mockRepository.Stub(x => x.GetProjectByNumber(COMPANY_CODE, "000"))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerProjectOrderController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };


            var result = _controller.GetProjectByNumber(COMPANY_CODE, "123");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test Method
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetProjectByNumberTestException()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerProjectOrderManager>();
            SetMockDataForCustomerProjectModelModels();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockRepository.Stub(x => x.GetProjectByNumber(COMPANY_CODE, "000"))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerProjectOrderController(mockRepository);
            MockControllerRequest();

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);

            _controller = new CustomerProjectOrderController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };


            var result = _controller.GetProjectByNumber(COMPANY_CODE, "12345");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test Method
        /// </summary>
        [TestMethod]
        public void GetProjectByNameTest()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerProjectOrderManager>();
            SetMockDataForCustomerProjectModelModels();
            var data = new Model.Response.CustomerProjectOrdersResponse { CustomerProjectOrders = _customerProjectOrderList };
            mockRepository.Stub(x => x.GetProjectByName(COMPANY_CODE,"ABC"))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerProjectOrderController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };


            var result = _controller.GetProjectByName(COMPANY_CODE, "ABC");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test Method
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetProjectByNameTestException()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerProjectOrderManager>();
            SetMockDataForCustomerProjectModelModels();
            var data = new Model.Response.CustomerProjectOrdersResponse { CustomerProjectOrders = _customerProjectOrderList };

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockRepository.Stub(x => x.GetProjectByName(COMPANY_CODE, "ABC"))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerProjectOrderController(mockRepository);
            MockControllerRequest();

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);

            _controller = new CustomerProjectOrderController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };


            var result = _controller.GetProjectByName(COMPANY_CODE, "ABC");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test Method
        /// </summary>
        [TestMethod]
        public void GetProjectByDurationTest()
        {
            
        var mockRepository = MockRepository.GenerateMock<ICustomerProjectOrderManager>();
            SetMockDataForCustomerProjectModelModels();
            var data = new Model.Response.CustomerProjectOrdersResponse { CustomerProjectOrders = _customerProjectOrderList };
            
            mockRepository.Stub(x => x.GetProjectByDuration(COMPANY_CODE, startDate, endDate))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerProjectOrderController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };


            var result = _controller.GetProjectByDuration(COMPANY_CODE, startDate, endDate);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test Method
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetProjectByDurationTestException()
        {

            var mockRepository = MockRepository.GenerateMock<ICustomerProjectOrderManager>();
            SetMockDataForCustomerProjectModelModels();
            var data = new Model.Response.CustomerProjectOrdersResponse { CustomerProjectOrders = _customerProjectOrderList };

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockRepository.Stub(x => x.GetProjectByDuration(COMPANY_CODE, startDate, endDate))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerProjectOrderController(mockRepository);
            MockControllerRequest();

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);

            _controller = new CustomerProjectOrderController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };


            var result = _controller.GetProjectByDuration(COMPANY_CODE, startDate, endDate);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test Method
        /// </summary>
        [TestMethod]
        public void GetProjectByCustomerPONoTest()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerProjectOrderManager>();
            SetMockDataForCustomerProjectModelModels();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };
            mockRepository.Stub(x => x.GetProjectByCustomerPONo(COMPANY_CODE, "000"))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerProjectOrderController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };


            var result = _controller.GetProjectByCustomerPONo(COMPANY_CODE, "123");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test Method
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetProjectByCustomerPONoTestException()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerProjectOrderManager>();
            SetMockDataForCustomerProjectModelModels();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockRepository.Stub(x => x.GetProjectByCustomerPONo(COMPANY_CODE, "000"))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerProjectOrderController(mockRepository);
            MockControllerRequest();

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);

            _controller = new CustomerProjectOrderController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };


            var result = _controller.GetProjectByCustomerPONo(COMPANY_CODE, "12345");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test Method
        /// </summary>
        [TestMethod]
        public void GetProjectByAccountTest()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerProjectOrderManager>();
            SetMockDataForCustomerProjectModelModels();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };
            mockRepository.Stub(x => x.GetProjectByAccount(COMPANY_CODE, "000"))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerProjectOrderController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };


            var result = _controller.GetProjectByAccount(COMPANY_CODE, "123");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test Method
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetProjectByAccountTestException()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerProjectOrderManager>();
            SetMockDataForCustomerProjectModelModels();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockRepository.Stub(x => x.GetProjectByAccount(COMPANY_CODE, "000"))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerProjectOrderController(mockRepository);
            MockControllerRequest();

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);

            _controller = new CustomerProjectOrderController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };


            var result = _controller.GetProjectByAccount(COMPANY_CODE, "12345");
            Assert.IsNotNull(result);
        }

        #endregion

        #region MockData Methods

        public void MockControllerRequest()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/customprojectorder");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "customerprojectorder" } });

            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        /// <summary>
        /// To create a mock data of customer for unit test
        /// </summary>
        public void SetMockDataForCustomerProjectModelModels()
        {
            #region SampleCustomerProjectOrderModelList
            _customerProjectOrderList.Add(new CustomerProjectOrderModel()
            {
                ERP_Project_Number__c = "M100030010",
                Account = "PL001",
                ERP_Customer_PO_Number__c = "7000128779",
                Subject = "INTEGATION CARRIER",
                Description = "MDR - Residual Value" + "JCC-11-1120-SIY-01",
                ERP_Project_Start_Date__c = "2010-12-14 00:00:00.0",
                ERP_Project_End_Date__c = "2011-02-28 00:00:00.0",
                Status = "Working",
                Origin = "Integrated",
                RecordType = "Projects",
                ERP_Project_Key__c="I"+ COMPANY_CODE+ "M100030010"

            });
            _customerProjectOrderList.Add(new CustomerProjectOrderModel()
            {
                ERP_Project_Number__c = "P000030020",
                Account = "NX001",
                ERP_Customer_PO_Number__c = "TH618000199004",
                Subject = "NXP-Install Tripod&Access",
                Description = "SYS PRIME RETROFIT" + "TH92-4240-Q534-R1",
                ERP_Project_Start_Date__c = "2009-09-29 00:00:00.0",
                ERP_Project_End_Date__c = "2009-12-30 00:00:00.0",
                Status = "Working",
                Origin = "Integrated",
                RecordType = "Projects",
                ERP_Project_Key__c = "I" + COMPANY_CODE + "M100030010"
            });
            _customerProjectOrderList.Add(new CustomerProjectOrderModel()
            {
                ERP_Project_Number__c = "P200050060",
                Account = "SE003",
                ERP_Customer_PO_Number__c = "SK1260221",
                Subject = "Fire alarm control panel",
                Description = "SYS PRIME RETROFIT" + "JCC-11-0509-SIR-01",
                ERP_Project_Start_Date__c = "2011-08-09 00:00:00.0",
                ERP_Project_End_Date__c = "2011-08-10 00:00:00.0",
                Status = "Working",
                Origin = "Integrated",
                RecordType = "Projects",
                ERP_Project_Key__c = "I" + COMPANY_CODE + "M100030010"
            });
            #endregion
        }
        #endregion
    }
}
