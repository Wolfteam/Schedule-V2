import axios from 'axios';
import {
    IApiResponseDto,
    IEmptyResponseDto,
    IApiListResponseDto,
    IGetAllSemestersResponseDto,
    ISaveSemesterRequestDto
} from '../models';
import { handleErrorResponse } from '../utils/network';

const semesterPath = '/Semester';

export const getAllSemesters = async (): Promise<IApiListResponseDto<IGetAllSemestersResponseDto>> => {
    try {
        const response = await axios.get<IApiListResponseDto<IGetAllSemestersResponseDto>>(semesterPath);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiListResponseDto<IGetAllSemestersResponseDto>>(error);
    }
};

export const createSemester = async (dto: ISaveSemesterRequestDto): Promise<IApiResponseDto<IGetAllSemestersResponseDto>> => {
    try {
        const response = await axios.post<IApiResponseDto<IGetAllSemestersResponseDto>>(semesterPath, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllSemestersResponseDto>>(error);
    }
};

export const updateSemester = async (id: number, dto: ISaveSemesterRequestDto): Promise<IApiResponseDto<IGetAllSemestersResponseDto>> => {
    try {
        const response = await axios.put<IApiResponseDto<IGetAllSemestersResponseDto>>(`${semesterPath}/${id}`, dto);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllSemestersResponseDto>>(error);
    }
};

export const deleteSemester = async (id: number): Promise<IEmptyResponseDto> => {
    try {
        const response = await axios.delete<IEmptyResponseDto>(`${semesterPath}/${id}`);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IEmptyResponseDto>(error);
    }
};