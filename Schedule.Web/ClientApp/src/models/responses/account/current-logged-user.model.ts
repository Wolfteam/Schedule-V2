import { Language } from '../../../enums';

export interface ICurrentLoggedUserResponseDto {
    username: string;
    fullName: string;
    language: Language;
}