export type { IApiListResponseDto } from './api-list-response.model';
export type { IApiResponseDto } from './api-response.model';
export type { IEmptyResponseDto } from './empty-response.model';

export type { IPaginatedRequestDto } from './paginated-request.model';
export { buildPaginatedRequest } from './paginated-request.model';
export type { IPaginatedResponseDto } from './paginated-response.model';

export type {
    ITeacherAvailabilityDetailResponseDto,
    ITeacherAvailabilityResponseDto,
} from './responses/teacher/teacher-availability.model';

export type { IGetAllTeacherResponseDto } from './responses/teacher/get-all-teacher.model';

export type { ISubjectResponseDto } from './responses/subjects.model';

export type { ILoginRequestDto } from './requests/account/login-request.model';

export type { IGetAllPeriodsResponseDto } from './responses/period/get-all-periods.model';

export type { ICurrentLoggedUserResponseDto } from './responses/account/current-logged-user.model';

export type { ISaveTeacherAvailabilityRequestDto, ITeacherAvailabilityRequestDto } from './requests/teacher/save-teacher-availability.model';

export type { IGetAllSubjectResponseDto } from './responses/subject/get-all-subject.model';

export type { ISaveSubjectRequestDto } from './requests/subject/save-subject.model';

export type { IGetAllSemestersResponseDto } from './responses/semester/get-all-semester.model';

export type { IGetAllClassroomsResponseDto } from './responses/classroom/get-all-classroom.model';

export type { IGetAllClassroomTypesResponseDto } from './responses/classroom/get-all-classroom-type.model';

export type { ISaveClassroomTypeRequestDto } from './requests/classroom/save-classroom-type.model';

export type { ISaveClassroomRequestDto } from './requests/classroom/save-classroom.model';

export type { IGetAllCareersResponseDto } from './responses/career/get-all-career.model';

export type { ISaveSemesterRequestDto } from './requests/semester/save-semester.model';

export type { ISaveCareerRequestDto } from './requests/career/save-career.model';

export type { ISaveTeacherRequestDto } from './requests/teacher/save-teacher.model';

export type { IGetAllPrioritiesResponseDto } from './responses/priorities/get-all-priorities.model';

export type { ISavePriorityRequestDto } from './requests/priorities/save-priority.model';