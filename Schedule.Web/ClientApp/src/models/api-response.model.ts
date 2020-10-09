import { IEmptyResponseDto } from './empty-response.model';

export interface IApiResponseDto<T> extends IEmptyResponseDto {
    result: T;
}