import axios from 'axios';
import {
    IApiResponseDto,
    IEmptyResponseDto,
    IGetAllSubjectResponseDto,
    IPaginatedRequestDto,
    IPaginatedResponseDto,
    ISaveSubjectRequestDto
} from '../models';
import { handleErrorResponse } from '../utils/network';

const subjectPath = '/Subject';

export const getAllSubjects = async (dto: IPaginatedRequestDto): Promise<IPaginatedResponseDto<IGetAllSubjectResponseDto>> => {
    try {
        const response = await axios.get<IPaginatedResponseDto<IGetAllSubjectResponseDto>>(subjectPath, { params: dto });
        return response.data;
    } catch (error) {
        return handleErrorResponse<IPaginatedResponseDto<IGetAllSubjectResponseDto>>(error);
    }
};

export const getSubject = async (id: number): Promise<IApiResponseDto<IGetAllSubjectResponseDto>> => {
    try {
        const response = await axios.get<IApiResponseDto<IGetAllSubjectResponseDto>>(`${subjectPath}/${id}`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllSubjectResponseDto>>(error);
    }
};

export const createSubject = async (dto: ISaveSubjectRequestDto): Promise<IApiResponseDto<IGetAllSubjectResponseDto>> => {
    try {
        const response = await axios.post<IApiResponseDto<IGetAllSubjectResponseDto>>(subjectPath, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllSubjectResponseDto>>(error);
    }
};

export const updateSubject = async (id: number, dto: ISaveSubjectRequestDto): Promise<IApiResponseDto<IGetAllSubjectResponseDto>> => {
    try {
        const response = await axios.put<IApiResponseDto<IGetAllSubjectResponseDto>>(`${subjectPath}/${id}`, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllSubjectResponseDto>>(error);
    }
};

export const deleteSubject = async (id: number): Promise<IEmptyResponseDto> => {
    try {
        const response = await axios.delete<IEmptyResponseDto>(`${subjectPath}/${id}`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IEmptyResponseDto>(error);
    }
}