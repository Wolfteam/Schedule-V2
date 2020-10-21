import axios from 'axios';
import { handleErrorResponse } from '../utils/network';
import {
    IApiResponseDto,
    ICurrentLoggedUserResponseDto,
    IEmptyResponseDto,
    ILoginRequestDto
} from '../models/index';

export const login = async (username: string, password: string, rememberMe: boolean): Promise<IEmptyResponseDto> => {
    const request: ILoginRequestDto = {
        username: username,
        password: password,
        rememberMe: rememberMe
    };
    try {
        const response = await axios.post<IEmptyResponseDto>('/Account/Login', request);
        return response.data;
    } catch (error) {
        return handleErrorResponse<IEmptyResponseDto>(error);
    }
};

export const logout = async () => {
    try {
        await axios.post<IEmptyResponseDto>('/Account/Logout');
    } catch (error) {
        console.log(error);
    }
};

export const isUserLogged = async (): Promise<IApiResponseDto<ICurrentLoggedUserResponseDto>> => {
    try {
        const response = await axios.get<IApiResponseDto<ICurrentLoggedUserResponseDto>>('/Account/CurrentUser');
        return response.data;
    } catch (error) {
        return handleErrorResponse<IApiResponseDto<ICurrentLoggedUserResponseDto>>(error);
    }
};