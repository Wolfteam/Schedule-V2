export interface IPaginatedRequestDto {
    take: number;
    page: number;
    searchTerm: string;
    orderBy: string;
    orderByAsc: boolean;
}

export const buildPaginatedRequest = (page: number, take: number, search: string, orderBy: string, orderByAsc: boolean) => {
    const request: IPaginatedRequestDto = {
        page: page,
        take: take,
        searchTerm: search,
        orderBy: orderBy,
        orderByAsc: orderByAsc
    };
    return request;
};