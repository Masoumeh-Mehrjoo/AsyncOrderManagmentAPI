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
    public class OrderItemService : IOrderItemService

    {
        private readonly IMapper _mapper;
        IOrderItemRepository _orderItemRepository;
        public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
        }
        public async Task DeleteOrderItemAsync(int Id)
        {
            await _orderItemRepository.DeleteAsync(Id);
        }
        public async Task EditOrderItemAsync(int Id, JsonPatchDocument<OrderItemForUpdate> patchDocument)
        {
            //try
            //{
            var OrderItem = await _orderItemRepository.findbyIdAsync(Id);

            var OrderItemForUpdateDto = _mapper.Map<OrderItemForUpdate>(OrderItem);
            patchDocument.ApplyTo(OrderItemForUpdateDto);

            _mapper.Map(OrderItemForUpdateDto, OrderItem);

            await _orderItemRepository.EditAsync(OrderItem);
            await _orderItemRepository.Save();
            //}
            //catch (Exception)
            //{
            //    throw new NotFoundException();
            //}
        }
        public async Task<OrderItemDto> FindByIdAsync(int Id)
        {
            try
            {
                var orderItem = await _orderItemRepository.findbyIdAsync(Id);
                var OrderItemDto = _mapper.Map<OrderItemDto>(orderItem);
                return (OrderItemDto);
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }
        }
        public async Task<OrderItemDto> InsertOrderItemAsync(int orderId, OrderItemForCreation OrderItem)
        {
            var orderItem = _mapper.Map<OrderItem>(OrderItem);
            await _orderItemRepository.InsertByOrderIdAsync(orderId, orderItem);

            var OrderItemDto = _mapper.Map<OrderItemDto>(orderItem);
            return OrderItemDto;

        }
        public async Task<IEnumerable<OrderItemDto>> OrderItemsOfOrderAsync(int OrderId)
        {
            var orderItems = await _orderItemRepository.FindOrderItemsofOrderIdAsync(OrderId);
            var OrderItemsDto = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);

            return (OrderItemsDto);

        }

    }
}
