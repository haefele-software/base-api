using AutoMapper;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.Domain.Enums;
using ProjectName.Application.Models.Mappings;
using System;

namespace ProjectName.Application.Models.Dtos
{
    public class TodoItemDto : IHaveCustomMapping
    {
        public long Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public PriorityLevel Priority { get; set; }

        public DateTimeOffset Reminder { get; set; }

        public bool Done { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TodoItem, TodoItemDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.ReferenceId, opt => opt.MapFrom(c => c.ReferenceId.Value))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(c => c.Title.Value))
                .ForMember(dto => dto.Note, opt => opt.MapFrom(c => c.Note.Value))
                .ForMember(dto => dto.Priority, opt => opt.MapFrom(c => c.Priority))
                .ForMember(dto => dto.Reminder, opt => opt.MapFrom(c => c.ReminderDate.Value))
                .ForMember(dto => dto.Done, opt => opt.MapFrom(c => c.Done));
        }
    }
}
