import axios from 'axios';
import { IApiResponseDto, IGetAllPeriodsResponseDto } from '../models';
import { handleErrorResponse } from '../utils/network';

export const getCurrentPeriod = async (): Promise<IApiResponseDto<IGetAllPeriodsResponseDto>> => {
    try {
        const response = await axios.get<IApiResponseDto<IGetAllPeriodsResponseDto>>('/Period/Current');
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<IGetAllPeriodsResponseDto>>(error);
    }
};