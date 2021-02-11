using AutoMapper;
using OrderManagmentAPI.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using OrderManagmentAPI.Repository;
using OrderManagmentAPI.Model.Repository;
using OrderManagmentAPI.Model;

namespace OrderManagmentAPI.Service.Profiles
{
    public class OrderProfile : Profile
    {
        readonly IClientRepository _clientRepository;

        public OrderProfile(IClientRepository clientRepository)
        {
            /********** Client Profile**********/
            CreateMap<Client, ClientDto>();

            CreateMap<ClientDto, Client>();
            CreateMap<ClientForCreationDto, Client>();

            CreateMap<Model.Client, ClientForUpdateDto>();
            CreateMap<ClientForUpdateDto, Client>();

            /*******Product Profile**********/
            CreateMap<Model.Product, ProductDto>();
            CreateMap<ProductDtoForCreation, Product>();

            /**************OrderItem******/
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();

            CreateMap<OrderItemForCreation, OrderItem>();

            CreateMap<OrderItem, OrderItemForUpdate>();
            CreateMap<OrderItemForUpdate,OrderItem >();




            _clientRepository = clientRepository;
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDto>();

            CreateMap<OrderForCreationDto, Order>().ForMember(dest => dest.client, act =>
            act.MapFrom(src => _clientRepository.findbyId(src.clientId)));

            CreateMap<Order, OrderForUpdateDto>();

            CreateMap<OrderForUpdateDto, Order>().ForMember(dest => dest.client, act =>
            act.MapFrom(src => _clientRepository.findbyId(src.ClientId)));



        }
    }
}
