import { IEmptyResponseDto } from './empty-response.model';

export interface IApiListResponseDto<T> extends IEmptyResponseDto {
    result: T[];
}