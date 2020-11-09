import axios from 'axios';
import {
    IApiResponseDto,
    IEmptyResponseDto,
    IGetAllClassroomsResponseDto,
    IGetAllClassroomTypesResponseDto,
    IPaginatedRequestDto,
    IPaginatedResponseDto,
    ISaveClassroomRequestDto,
    ISaveClassroomTypeRequestDto
} from '../models';
import { handleErrorResponse } from '../utils/network';

const classroomBasePath = '/Classroom';
const classroomTypeBasePath = '/Classroom/Types';

export const getAllClassrooms = async (dto: IPaginatedRequestDto): Promise<IPaginatedResponseDto<IGetAllClassroomsResponseDto>> => {
    try {
        const response = await axios.get<IPaginatedResponseDto<IGetAllClassroomsResponseDto>>(classroomBasePath, { params: dto });
        return response.data;
    } catch (error) {
        return handleErrorResponse<IPaginatedResponseDto<IGetAllClassroomsResponseDto>>(error);
    }
};

export const getClassroom = async (id: number): Promise<IApiResponseDto<IGetAllClassroomsResponseDto>> => {
    try {
        const response = await axios.get<IApiResponseDto<IGetAllClassroomsResponseDto>>(`${classroomBasePath}/${id}`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllClassroomsResponseDto>>(error);
    }
};

export const createClassroom = async (dto: ISaveClassroomRequestDto): Promise<IApiResponseDto<IGetAllClassroomsResponseDto>> => {
    try {
        const response = await axios.post<IApiResponseDto<IGetAllClassroomsResponseDto>>(classroomBasePath, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllClassroomsResponseDto>>(error);
    }
};

export const updateClassroom = async (id: number, dto: ISaveClassroomRequestDto): Promise<IApiResponseDto<IGetAllClassroomsResponseDto>> => {
    try {
        const response = await axios.put<IApiResponseDto<IGetAllClassroomsResponseDto>>(`${classroomBasePath}/${id}`, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllClassroomsResponseDto>>(error);
    }
};

export const deleteClassroom = async (id: number): Promise<IEmptyResponseDto> => {
    try {
        const response = await axios.delete<IEmptyResponseDto>(`${classroomBasePath}/${id}`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IEmptyResponseDto>(error);
    }
}

export const getAllClassroomTypes = async (dto: IPaginatedRequestDto): Promise<IPaginatedResponseDto<IGetAllClassroomTypesResponseDto>> => {
    try {
        const response = await axios.get<IPaginatedResponseDto<IGetAllClassroomTypesResponseDto>>(classroomTypeBasePath, { params: dto });
        return response.data;
    } catch (error) {
        return handleErrorResponse<IPaginatedResponseDto<IGetAllClassroomTypesResponseDto>>(error);
    }
};

export const getClassroomType = async (id: number): Promise<IApiResponseDto<IGetAllClassroomTypesResponseDto>> => {
    try {
        const response = await axios.get<IApiResponseDto<IGetAllClassroomTypesResponseDto>>(`${classroomTypeBasePath}/${id}`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllClassroomTypesResponseDto>>(error);
    }
};

export const createClassroomType = async (dto: ISaveClassroomTypeRequestDto): Promise<IApiResponseDto<IGetAllClassroomTypesResponseDto>> => {
    try {
        const response = await axios.post<IApiResponseDto<IGetAllClassroomTypesResponseDto>>(classroomTypeBasePath, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllClassroomTypesResponseDto>>(error);
    }
};

export const updateClassroomType = async (id: number, dto: ISaveClassroomTypeRequestDto): Promise<IApiResponseDto<IGetAllClassroomTypesResponseDto>> => {
    try {
        const response = await axios.put<IApiResponseDto<IGetAllClassroomTypesResponseDto>>(`${classroomTypeBasePath}/${id}`, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllClassroomTypesResponseDto>>(error);
    }
};

export const deleteClassroomType = async (id: number): Promise<IEmptyResponseDto> => {
    try {
        const response = await axios.delete<IEmptyResponseDto>(`${classroomTypeBasePath}/${id}`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IEmptyResponseDto>(error);
    }
}