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
        public IEnumerable<OrderDto> AllRows()
        {
            try
            {
                var Rep_Orders = _OrderRepository.AllRows();
             
                var OrdersToReturn = _mapper.Map<IEnumerable<OrderDto>>(Rep_Orders);
                return (OrdersToReturn);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteOrder(int Id)
        {
            throw new NotImplementedException();
        }

        public void EditOrder(int OrderId, JsonPatchDocument<OrderForUpdateDto> patchDocument)
        {
            //  try
            // {
            var Order = _OrderRepository.findbyId(OrderId);

            var OrderTopatch = _mapper.Map<OrderForUpdateDto>(Order);
            patchDocument.ApplyTo(OrderTopatch);

            _mapper.Map(OrderTopatch, Order);
            _OrderRepository.Edit(Order);

            _OrderRepository.Save();
            // }
            // catch (Exception)
            // {
            //   throw new NotFoundException();
            // }

        }

        public OrderDto FindById(int Id)
        {
            var RepOrder = _OrderRepository.findbyId(Id);
            var OrderToReturn = _mapper.Map<OrderDto>(RepOrder);
            return (OrderToReturn);


        }

        public OrderDto InsertOrder(OrderForCreationDto orderForCreationDto)
        {
            try
            {
                var Order = _mapper.Map<Order>(orderForCreationDto);
                _OrderRepository.Insert(Order);

                var OrderToReturn = _mapper.Map<OrderDto>(Order);
                return OrderToReturn;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<OrderDto> SearchedRows(OrderResourceParameter OrderResourceParameter)
        {
            throw new NotImplementedException();
        }
    }
}
