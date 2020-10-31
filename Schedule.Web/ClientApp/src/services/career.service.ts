import axios from 'axios';
import {
    IApiResponseDto,
    IEmptyResponseDto,
    IApiListResponseDto,
    IGetAllCareersResponseDto,
    ISaveCareerRequestDto
} from '../models';
import { handleErrorResponse } from '../utils/network';

const careerPath = '/Career';

export const getAllCareers = async (): Promise<IApiListResponseDto<IGetAllCareersResponseDto>> => {
    try {
        const response = await axios.get<IApiListResponseDto<IGetAllCareersResponseDto>>(careerPath);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiListResponseDto<IGetAllCareersResponseDto>>(error);
    }
};


export const createCareer = async (dto: ISaveCareerRequestDto): Promise<IApiResponseDto<IGetAllCareersResponseDto>> => {
    try {
        const response = await axios.post<IApiResponseDto<IGetAllCareersResponseDto>>(careerPath, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllCareersResponseDto>>(error);
    }
};

export const updateCareer = async (id: number, dto: ISaveCareerRequestDto): Promise<IApiResponseDto<IGetAllCareersResponseDto>> => {
    try {
        const response = await axios.put<IApiResponseDto<IGetAllCareersResponseDto>>(`${careerPath}/${id}`, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllCareersResponseDto>>(error);
    }
};

export const deleteCareer = async (id: number): Promise<IEmptyResponseDto> => {
    try {
        const response = await axios.delete<IEmptyResponseDto>(`${careerPath}/${id}`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IEmptyResponseDto>(error);
    }
};