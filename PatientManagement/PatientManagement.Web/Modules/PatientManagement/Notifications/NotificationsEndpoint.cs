﻿
namespace PatientManagement.PatientManagement.Endpoints
{
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System.Data;
    using Microsoft.AspNetCore.Mvc;
    using MyRepository = Repositories.NotificationsRepository;
    using MyRow = Entities.NotificationsRow;

    [Route("Services/PatientManagement/Notifications/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class NotificationsController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MyRepository().Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MyRepository().Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            return new MyRepository().Delete(uow, request);
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRepository().Retrieve(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository().List(connection, request);
        }
        public ListResponse<MyRow> ListForDropdown(IDbConnection connection, ListRequest request)
        {
            return new MyRepository().ListForDropdown(connection, request);
        }
        public CountNotificationsResponse CountNotifications(IDbConnection connection, ListRequest request)
        {
           return new CountNotificationsResponse
           {
               Count = connection.Count<MyRow>()
           };
        }
    }

    public class CountNotificationsRequest : ServiceRequest
    {
        public decimal Count { get; set; }
    }
    public class CountNotificationsResponse : ServiceResponse
    {
        public int Count { get; set; }
    }

}
