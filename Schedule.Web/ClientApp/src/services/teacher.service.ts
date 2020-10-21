import axios from 'axios'
import {
    IApiListResponseDto,
    IApiResponseDto,
    IGetAllTeacherResponseDto,
    ISaveTeacherAvailabilityRequestDto,
    ITeacherAvailabilityDetailResponseDto,
    ITeacherAvailabilityResponseDto,
} from '../models'
import { handleErrorResponse } from '../utils/network';

export const getAllTeachers = async (): Promise<IApiListResponseDto<IGetAllTeacherResponseDto>> => {
    try {
        const response = await axios.get<IApiListResponseDto<IGetAllTeacherResponseDto>>("/Teacher");
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiListResponseDto<IGetAllTeacherResponseDto>>(error);
    }
};

export const getTeacherAvailability = async (id: number): Promise<IApiResponseDto<ITeacherAvailabilityDetailResponseDto>> => {
    try {
        const response = await axios.get<IApiResponseDto<ITeacherAvailabilityDetailResponseDto>>(`/Teacher/${id}/Availability`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<ITeacherAvailabilityDetailResponseDto>>(error);
    }
};

export const saveTeacherAvailability = async (id: number, dto: ISaveTeacherAvailabilityRequestDto): Promise<IApiListResponseDto<ITeacherAvailabilityResponseDto>> => {
    try {
        const response = await axios.post<IApiListResponseDto<ITeacherAvailabilityResponseDto>>(`/Teacher/${id}/Availability`, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiListResponseDto<ITeacherAvailabilityResponseDto>>(error);
    }
};