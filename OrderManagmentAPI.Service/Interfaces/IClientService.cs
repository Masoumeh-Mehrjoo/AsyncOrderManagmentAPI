using Microsoft.AspNetCore.JsonPatch;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Service.Interfaces
{
    public interface IClientService
    {
        public ClientDto InsertClient(ClientForCreationDto Client);

        public IEnumerable<ClientDto> AllRows();

        public ClientDto FindById(int Id);

        public IEnumerable<ClientDto> SearchedRows(ClientResourceParameter ClientResourceParameter);

        public void DeleteClient(int Id);

        public void EditClient( int ClientId,JsonPatchDocument <ClientForUpdateDto>  patchDocument);
        public IEnumerable<OrderDto> OrdersofClient(int ClientId);
    }
}
 