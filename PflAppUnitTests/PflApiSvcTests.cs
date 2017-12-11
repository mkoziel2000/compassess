using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PFLApp.Configuration;
using PFLApp.Models;
using PFLApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PflAppUnitTests
{
    public class PflApiSvcTests
    {
        /// <summary>
        /// Integration Test
        /// </summary>
        [Fact]
        public async void GetProducts_Success()
        {
            var svc = CreateService();
            var products = await svc.GetProductsAsync();
            Assert.NotNull(products);
            Assert.True(products.Count > 0);
        }

        /// <summary>
        /// Integration Test
        /// </summary>
        [Fact]
        public async void PlaceOrder_Failure()
        {
            var svc = CreateService();
            PflOrderModel order = new PflOrderModel()
            {
                OrderCustomer = new PflCustomerModel()
                {
                    FirstName = "Joe",
                    LastName = "Schmoe",
                    Address1 = "123 Main St",
                    City = "Bozeman",
                    State = "MT",
                    PostalCode = "59715",
                    CountryCode = "US",
                    Phone = "1234567890",
                    Email = "test@test.com"
                },
                Items = new List<PflOrderItemModel>()
                {
                    new PflOrderItemModel()
                    {
                        ItemSequenceNumber = 1,
                        PartnerItemReference = "SomeRefID",
                        ProductId = 1234,
                        Quantity = 10,
                    }
                },
                Shipments = new List<PflShipmentModel>()
                {
                    new PflShipmentModel()
                    {
                        ShipmentSequenceNumber = 1,
                        FirstName = "Joe",
                        LastName = "Schmoe",
                        Address1 = "123 Main St",
                        City = "Bozeman",
                        State = "MT",
                        PostalCode = "59715",
                        CountryCode = "US",
                        Phone = "1234567890",
                        ShippingMethod = "FDXG",
                        Email = "test@test.com"
                    }
                },
                ItemShipments = new List<PflItemShipmentModel>()
                {
                    new PflItemShipmentModel()
                    {
                        ItemSequenceNumber = 1,
                        ShipmentSequenceNumber = 1
                    }
                },
                PartnerOrderReference = "SomePartnerOrderID"
            };
            var orderNumber = await svc.PlaceOrderAsync(order);
            Assert.Null(orderNumber);
        }

        /// <summary>
        /// Integration Test
        /// </summary>
        [Fact]
        public async void PlaceOrder_Success()
        {
            var svc = CreateService();
            PflOrderModel order = new PflOrderModel()
            {
                OrderCustomer = new PflCustomerModel()
                {
                    FirstName = "Joe",
                    LastName = "Schmoe",
                    Address1 = "123 Main St",
                    City = "Bozeman",
                    State = "MT",
                    PostalCode = "59715",
                    CountryCode = "US",
                    Phone = "1234567890",
                    Email = "test@test.com"
                },
                Items = new List<PflOrderItemModel>()
                {
                    new PflOrderItemModel()
                    {
                        ItemSequenceNumber = 1,
                        PartnerItemReference = "SomeRefID",
                        ProductId = 9876,
                        Quantity = 1
                    }
                },
                Shipments = new List<PflShipmentModel>()
                {
                    new PflShipmentModel()
                    {
                        ShipmentSequenceNumber = 1,
                        FirstName = "Joe",
                        LastName = "Schmoe",
                        Address1 = "123 Main St",
                        City = "Bozeman",
                        State = "MT",
                        PostalCode = "59715",
                        CountryCode = "US",
                        Phone = "1234567890",
                        ShippingMethod = "FDXG",
                        Email = "test@test.com"
                    }
                },
                ItemShipments = new List<PflItemShipmentModel>()
                {
                    new PflItemShipmentModel()
                    {
                        ItemSequenceNumber = 1,
                        ShipmentSequenceNumber = 1
                    }
                },
                PartnerOrderReference = "SomePartnerOrderID"
            };
            var orderNumber = await svc.PlaceOrderAsync(order);
            Assert.NotNull(orderNumber);
            Assert.True(orderNumber.Length > 0);
        }

        private PflApiSvc CreateService()
        {
            Mock<ILogger<PflApiSvc>> mockLogger = new Mock<ILogger<PflApiSvc>>();
            Mock<IOptionsSnapshot<PflLinkConfig>> mockPflLinkConfig = new Mock<IOptionsSnapshot<PflLinkConfig>>();
            mockPflLinkConfig.Setup(a => a.Value).Returns(new PflLinkConfig()
            {
                Url = "https://testapi.pfl.com",
                ApiKey = "XvVY1MKALOGZFhK0qe3PZ30YRVIDUn1HPtpiPm5bp8so7EYLGMqndqrhYJB7sYkYpfzYbGujnsrub23FrTtWdT506hbe/iRlrLV4+8w4/vXMXb4+8F/OBSZAYGYn/GtxYQpij87CWVGj/qvSESeGYTkUOAC/Nd9+yNn1F+9xmD70MYsOhVTZp42ByBjIuzaK4SDrf+efHQ04u28yAfJ5tnLCCAyXo277Uq1fkiYqDzqEWvrVrEDWdLD3czEGYhQDjYAFJF3rfS/O0F+ujOeMdmOMf3KpEGXE54VuwO0rKZ4dFw00oYnVQmbqdQPtAoFr6uJh9H+kHQbyT8PhXYmqdg==",
                Username = "miniproject",
                Password = "AB2qffrKTVq6e+xWyp3g49wIxCig+ueH/+YvAP4XV17tEY4m2yVA0yjeeqGfeO8HvJOtslF/eLUcRpZ3aVlGJ2Br1LYc2GCRCX2naW2l8abaI8zhpmbPyXYnXcH6P+OAdQlpd72IW04aPB+f4JnwttSwvUAjZrSxUNf+IYxq1mF7lob1I2hFToaVL7Z5leae5WeSOETLGEO53YJf/Q5V2o86J2F+ult3IWXk6Flg3iQBQLeDv5V7iqOqxW2YEpLsNcN3UD3gg4YOofPnBIVMzLSt2ZJPgQjYPRB2ldPifoH9uxIrhswmr9K/4tyWZ9dx+ZwW5sxFlRkuKA9hdqwjfA=="
            });

            Mock<IOptionsSnapshot<X509CertConfig>> mockX509CertConfig = new Mock<IOptionsSnapshot<X509CertConfig>>();
            mockX509CertConfig.Setup(a => a.Value).Returns(new X509CertConfig()
            {
                CertFile = "PFL.pfx"
            });
            return new PflApiSvc(mockLogger.Object, mockPflLinkConfig.Object, mockX509CertConfig.Object);
        }
    }
}
