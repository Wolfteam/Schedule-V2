import { IApiListResponseDto } from './api-list-response.model';

export interface IPaginatedResponseDto<T> extends IApiListResponseDto<T> {
    take: number;
    totalRecords: number;
    currentPage: number;
    records: number;
    totalPages: number;
}