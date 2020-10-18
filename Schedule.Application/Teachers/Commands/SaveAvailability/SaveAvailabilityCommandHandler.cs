﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Teachers.Responses;
using Schedule.Domain.Entities;
using Schedule.Shared.Exceptions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Teachers.Commands.SaveAvailability
{
    public class SaveAvailabilityCommandHandler : BaseApiListRequestHandler<SaveAvailabilityCommand, TeacherAvailabilityResponseDto>
    {
        private readonly IMapper _mapper;
        public SaveAvailabilityCommandHandler(
            ILogger<SaveAvailabilityCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiListResponseDto<TeacherAvailabilityResponseDto>> Handle(SaveAvailabilityCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await AppDataService.Teachers.ExistsAsync(t => t.Id == request.TeacherId && t.SchoolId == AppUserManager.SchoolId);
            if (!teacherExists)
            {
                var msg = $"TeacherId = {request.TeacherId} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            bool periodExists = await AppDataService.Periods.ExistsAsync(p => p.Id == request.Dto.PeriodId && p.SchoolId == AppUserManager.SchoolId);
            if (!periodExists)
            {
                var msg = $"PeriodId = {request.Dto.PeriodId} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            var currentAvailabilities = await AppDataService.TeacherAvailabilities.GetAllAsync(t =>
                t.TeacherId == request.TeacherId &&
                t.Teacher.SchoolId == AppUserManager.SchoolId &&
                t.PeriodId == request.Dto.PeriodId);

            AppDataService.TeacherAvailabilities.RemoveRange(currentAvailabilities);

            var newAvailabilities = _mapper.Map<List<TeacherAvailability>>(request.Dto.Availability);
            foreach (var item in newAvailabilities)
            {
                item.TeacherId = request.TeacherId;
                item.PeriodId = request.Dto.PeriodId;
            }

            AppDataService.TeacherAvailabilities.AddRange(newAvailabilities);

            await AppDataService.SaveChangesAsync();

            return new ApiListResponseDto<TeacherAvailabilityResponseDto>(_mapper.Map<List<TeacherAvailabilityResponseDto>>(newAvailabilities));
        }
    }
}
