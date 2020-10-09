export interface IPaginatedRequestDto {
    take: number,
    page: number,
    searchTerm: string,
    orderBy: string,
    orderByAsc: boolean,
    draw: number
}