using Microsoft.AspNetCore.JsonPatch;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Service.Interfaces
{
    public interface IClientService
    {
        public Task<ClientDto> InsertClientAsync(ClientForCreationDto Client);

        public Task<IEnumerable<ClientDto>> AllRowsAsync();

        public Task<ClientDto> FindByIdAsync(int Id);

        public Task<IEnumerable<ClientDto>> SearchedRowsAsync(ClientResourceParameter ClientResourceParameter);

        public Task DeleteClientAsync(int Id);

        public Task EditClientAsync( int ClientId,JsonPatchDocument <ClientForUpdateDto>  patchDocument);
        public Task<IEnumerable<OrderDto>> OrdersofClientAsync(int ClientId);
    }
}
 