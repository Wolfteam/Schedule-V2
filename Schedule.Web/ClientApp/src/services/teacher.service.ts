import axios from 'axios';
import {
    IApiListResponseDto,
    IApiResponseDto,
    IEmptyResponseDto,
    IGetAllPrioritiesResponseDto,
    IGetAllTeacherResponseDto,
    ISavePriorityRequestDto,
    ISaveTeacherAvailabilityRequestDto,
    ISaveTeacherRequestDto,
    ITeacherAvailabilityDetailResponseDto,
    ITeacherAvailabilityResponseDto
} from '../models';
import { handleErrorResponse } from '../utils/network';

const teachersPath = "/Teacher";
const prioritiesPath = `${teachersPath}/Priorities`;

export const getAllTeachers = async (): Promise<IApiListResponseDto<IGetAllTeacherResponseDto>> => {
    try {
        const response = await axios.get<IApiListResponseDto<IGetAllTeacherResponseDto>>(teachersPath);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiListResponseDto<IGetAllTeacherResponseDto>>(error);
    }
};

export const getTeacher = async (id: number): Promise<IApiResponseDto<IGetAllTeacherResponseDto>> => {
    try {
        const response = await axios.get<IApiResponseDto<IGetAllTeacherResponseDto>>(`${teachersPath}/${id}`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllTeacherResponseDto>>(error);
    }
};

export const createTeacher = async (dto: ISaveTeacherRequestDto): Promise<IApiResponseDto<IGetAllTeacherResponseDto>> => {
    try {
        const response = await axios.post<IApiResponseDto<IGetAllTeacherResponseDto>>(teachersPath, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllTeacherResponseDto>>(error);
    }
};

export const updateTeacher = async (id: number, dto: ISaveTeacherRequestDto): Promise<IApiResponseDto<IGetAllTeacherResponseDto>> => {
    try {
        const response = await axios.put<IApiResponseDto<IGetAllTeacherResponseDto>>(`${teachersPath}/${id}`, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllTeacherResponseDto>>(error);
    }
};

export const deleteTeacher = async (id: number): Promise<IEmptyResponseDto> => {
    try {
        const response = await axios.delete<IEmptyResponseDto>(`${teachersPath}/${id}`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IEmptyResponseDto>(error);
    }
}

export const getTeacherAvailability = async (id: number): Promise<IApiResponseDto<ITeacherAvailabilityDetailResponseDto>> => {
    try {
        const response = await axios.get<IApiResponseDto<ITeacherAvailabilityDetailResponseDto>>(`${teachersPath}/${id}/Availability`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<ITeacherAvailabilityDetailResponseDto>>(error);
    }
};

export const saveTeacherAvailability = async (id: number, dto: ISaveTeacherAvailabilityRequestDto): Promise<IApiListResponseDto<ITeacherAvailabilityResponseDto>> => {
    try {
        const response = await axios.post<IApiListResponseDto<ITeacherAvailabilityResponseDto>>(`${teachersPath}/${id}/Availability`, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiListResponseDto<ITeacherAvailabilityResponseDto>>(error);
    }
};

export const getAllPriorities = async (): Promise<IApiListResponseDto<IGetAllPrioritiesResponseDto>> => {
    try {
        const response = await axios.get<IApiListResponseDto<IGetAllPrioritiesResponseDto>>(prioritiesPath);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiListResponseDto<IGetAllPrioritiesResponseDto>>(error);
    }
};

export const getPriority = async (id: number): Promise<IApiResponseDto<IGetAllPrioritiesResponseDto>> => {
    try {
        const response = await axios.get<IApiResponseDto<IGetAllPrioritiesResponseDto>>(`${prioritiesPath}/${id}`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllPrioritiesResponseDto>>(error);
    }
};

export const createPriority = async (dto: ISavePriorityRequestDto): Promise<IApiResponseDto<IGetAllPrioritiesResponseDto>> => {
    try {
        const response = await axios.post<IApiResponseDto<IGetAllPrioritiesResponseDto>>(prioritiesPath, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllPrioritiesResponseDto>>(error);
    }
};

export const updatePriority = async (id: number, dto: ISavePriorityRequestDto): Promise<IApiResponseDto<IGetAllPrioritiesResponseDto>> => {
    try {
        const response = await axios.put<IApiResponseDto<IGetAllPrioritiesResponseDto>>(`${prioritiesPath}/${id}`, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllPrioritiesResponseDto>>(error);
    }
};

export const deletePriority = async (id: number): Promise<IEmptyResponseDto> => {
    try {
        const response = await axios.delete<IEmptyResponseDto>(`${prioritiesPath}/${id}`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IEmptyResponseDto>(error);
    }
}