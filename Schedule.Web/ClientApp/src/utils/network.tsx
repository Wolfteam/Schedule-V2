import translations from '../services/translations';
import { IEmptyResponseDto } from '../models';

export const handleErrorResponse = <T extends IEmptyResponseDto>(error: any): T => {
    const errorResponse = error.response?.data as T;
    if (errorResponse)
        return errorResponse;
    console.log(error);
    const genericResponse = {
        errorMessageCode: 999,
        errorMessageId: 'NA',
        errorMessage: translations.unknownError,
        succeed: false
    };

    return genericResponse as T;
}