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
    public class OrderService : IOrderService
    {
        IOrderRepository _OrderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _OrderRepository = orderRepository ??
                throw new ArgumentNullException(nameof(orderRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<OrderDto>> AllRowsAsync()
        {
            try
            {
                var Rep_Orders = await _OrderRepository.AllRowsAsync();

                var OrdersToReturn = _mapper.Map<IEnumerable<OrderDto>>(Rep_Orders);
                return (OrdersToReturn);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task DeleteOrderAsync(int Id)
        {
            throw new NotImplementedException();
        }
        public async Task EditOrderAsync(int OrderId, JsonPatchDocument<OrderForUpdateDto> patchDocument)
        {
            try
            {

                var Order = await _OrderRepository.findbyIdAsync(OrderId);

                var OrderTopatch = _mapper.Map<OrderForUpdateDto>(Order);
                patchDocument.ApplyTo(OrderTopatch);

                _mapper.Map(OrderTopatch, Order);
                _OrderRepository.Edit(Order);
                await _OrderRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<OrderDto> FindByIdAsync(int Id)
        {
            var RepOrder = await _OrderRepository.findbyIdAsync(Id);
            var OrderToReturn = _mapper.Map<OrderDto>(RepOrder);
            return (OrderToReturn);


        }
        public async Task<OrderDto> InsertOrderAsync(OrderForCreationDto orderForCreationDto)
        {
            try
            {
                var Order = _mapper.Map<Order>(orderForCreationDto);
                await _OrderRepository.InsertAsync(Order);

                var OrderToReturn = _mapper.Map<OrderDto>(Order);
                return OrderToReturn;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<IEnumerable<OrderDto>> SearchedRowsAsync(OrderResourceParameter OrderResourceParameter)
        {
            throw new NotImplementedException();
        }
    }
}
