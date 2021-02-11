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
    public class OrderItemService : IOrderItemService

    {
        private readonly IMapper _mapper;
        IOrderItemRepository _orderItemRepository;
        public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
        }
        public void DeleteOrderItem(int Id)
        {
            _orderItemRepository.Delete(Id);
        }
        public void EditOrderItem(int Id, JsonPatchDocument<OrderItemForUpdate> patchDocument)
        {
            try
            {
                var OrderItem = _orderItemRepository.findbyId(Id);

                var OrderItemForUpdateDto = _mapper.Map<OrderItemForUpdate>(OrderItem);
                patchDocument.ApplyTo(OrderItemForUpdateDto);

                _mapper.Map(OrderItemForUpdateDto, OrderItem);
                _orderItemRepository.Edit(OrderItem);

                _orderItemRepository.Save();
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }
        }
        public OrderItemDto FindById(int Id)
        {
            try
            {
                var orderItem = _orderItemRepository.findbyId(Id);
                var OrderItemDto = _mapper.Map<OrderItemDto>(orderItem);
                return (OrderItemDto);
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }
        }
        public OrderItemDto InsertOrderItem(int orderId, OrderItemForCreation OrderItem)
        {
            var orderItem = _mapper.Map<OrderItem>(OrderItem);
            _orderItemRepository.InsertByOrderId(orderId, orderItem);

            var OrderItemDto = _mapper.Map<OrderItemDto>(orderItem);
            return OrderItemDto;

        }
        public IEnumerable<OrderItemDto> OrderItemsOfOrder(int OrderId)
        {
            var orderItems = _orderItemRepository.FindOrderItemsofOrderId(OrderId);
            var OrderItemsDto = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);

            return (OrderItemsDto);

        }

    }
}
