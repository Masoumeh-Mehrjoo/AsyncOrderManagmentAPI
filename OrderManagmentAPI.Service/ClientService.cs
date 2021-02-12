using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Model.Repository;
using OrderManagmentAPI.Repository;
using OrderManagmentAPI.Service.Dto;
using OrderManagmentAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Service
{
    public  class ClientService : IClientService
    {
        IClientRepository _iclientRepository;
        private readonly IMapper _mapper;
        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            this._iclientRepository = clientRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<ClientDto>> AllRowsAsync()
        {
            try
            {
                var clients = await _iclientRepository.AllRowsAsync();

                var clientDto = _mapper.Map<IEnumerable<ClientDto>>(clients);

                return (clientDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ClientDto> FindByIdAsync(int Id)
        {
            var client = await _iclientRepository.findbyIdAsync(Id);
            var clientDto = _mapper.Map<ClientDto>(client);
            return (clientDto);
        }

        public async Task<ClientDto> InsertClientAsync(ClientForCreationDto Client)
        {
            try
            {
                var client = _mapper.Map<Client>(Client);
               await _iclientRepository.InsertAsync(client);

                var clientDto = _mapper.Map<ClientDto>(client);
                return clientDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ClientDto>> SearchedRowsAsync(ClientResourceParameter ClientResourceParameter)
        {
            var clients = await _iclientRepository.SearchedRowsAsync(ClientResourceParameter);
            var clientDtos = _mapper.Map<IEnumerable<ClientDto>>(clients);

            return  (clientDtos);
        }
        public async Task DeleteClientAsync(int Id)
        {
            await _iclientRepository.DeleteAsync(Id);

        }

        public async Task EditClientAsync(int Id, JsonPatchDocument<ClientForUpdateDto> patchDocument)
        {
            try
            {
                var client = await _iclientRepository.findbyIdAsync(Id);

                var clientForUpdateDto = _mapper.Map<ClientForUpdateDto>(client);
                patchDocument.ApplyTo(clientForUpdateDto);

                _mapper.Map(clientForUpdateDto, client);
                _iclientRepository.Edit(client);

                _iclientRepository.Save();
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }
        }

        public async Task<IEnumerable<OrderDto>> OrdersofClientAsync(int clientId)
        {
            var Orders = await _iclientRepository.OrdersOfClientAsync(clientId);

            var ListOfOrders =_mapper.Map<IEnumerable<OrderDto>>(Orders);
            return (ListOfOrders);
        }
    }
}
