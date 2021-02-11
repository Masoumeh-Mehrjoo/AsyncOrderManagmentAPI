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

namespace OrderManagmentAPI.Service
{
    public class ClientService : IClientService
    {
        IClientRepository _iclientRepository;
        private readonly IMapper _mapper;
        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            this._iclientRepository = clientRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public IEnumerable<ClientDto> AllRows()
        {
            try
            {
                var clients = _iclientRepository.AllRows();

                var clientDto = _mapper.Map<IEnumerable<ClientDto>>(clients);

                return (clientDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ClientDto FindById(int Id)
        {
            var client = _iclientRepository.findbyId(Id);
            var clientDto = _mapper.Map<ClientDto>(client);
            return (clientDto);
        }

        public ClientDto InsertClient(ClientForCreationDto Client)
        {
            try
            {
                var client = _mapper.Map<Client>(Client);
                _iclientRepository.Insert(client);

                var clientDto = _mapper.Map<ClientDto>(client);
                return clientDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ClientDto> SearchedRows(ClientResourceParameter ClientResourceParameter)
        {
            var clients = _iclientRepository.SearchedRows(ClientResourceParameter);
            var clientDtos = _mapper.Map<IEnumerable<ClientDto>>(clients);

            return (clientDtos);
        }
        public void DeleteClient(int Id)
        {
            _iclientRepository.Delete(Id);

        }

        public void EditClient(int Id, JsonPatchDocument<ClientForUpdateDto> patchDocument)
        {
            try
            {
                var client = _iclientRepository.findbyId(Id);

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

        public IEnumerable<OrderDto> OrdersofClient(int clientId)
        {
            var Orders = _iclientRepository.OrdersOfClient(clientId);

            var ListOfOrders =_mapper.Map<IEnumerable<OrderDto>>(Orders);
            return (ListOfOrders);
        }
    }
}
