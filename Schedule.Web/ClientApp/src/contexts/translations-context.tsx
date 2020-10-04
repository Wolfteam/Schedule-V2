import React, { useState, createContext, Dispatch, SetStateAction } from 'react'
import translations from '../services/translations';

export const SupportedLangs: string[] = ['es', 'en'];

interface TranslationContext {
    currentLanguage: string;
}

const defaultValue: TranslationContext = {
    currentLanguage: SupportedLangs[1]
};

export const TranslationContext = createContext<[TranslationContext | null, Dispatch<SetStateAction<TranslationContext>> | null]>([null, null]);

export const TranslationContextProvider = (children: any) => {
    const [trans, setTrans] = useState<TranslationContext>(defaultValue);

    if (trans.currentLanguage !== translations.getLanguage())
        translations.setLanguage(trans.currentLanguage);

    return <TranslationContext.Provider value={[trans, setTrans]}>
        {children.children}
    </TranslationContext.Provider>;
};