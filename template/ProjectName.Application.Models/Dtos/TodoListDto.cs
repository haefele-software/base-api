using AutoMapper;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.Models.Mappings;
using System;
using System.Collections.Generic;

namespace ProjectName.Application.Models.Dtos
{
    public class TodoListDto : IHaveCustomMapping
    {
        public long Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string Title { get; set; }

        public TodoItemDto[] Items { get; private set; } = new List<TodoItemDto>().ToArray();

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TodoList, TodoListDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.ReferenceId, opt => opt.MapFrom(c => c.ReferenceId.Value))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(c => c.Title.Value))
                .ForMember(dto => dto.Items, opt => opt.MapFrom(c => c.Items));
        }
    }
}
