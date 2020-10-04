import React, { Dispatch, SetStateAction, useState } from 'react'

interface AuthContext {
    email: string,
    isAuthenticated: boolean
}

const defaultValue: AuthContext = {
    email: '',
    isAuthenticated: false
};

export const AuthContext = React.createContext<[AuthContext | null, Dispatch<SetStateAction<AuthContext>> | null]>([null, null]);

export const AuthContextProvider = (children: any) => {
    const [auth, setAuth] = useState<AuthContext>(defaultValue);

    return (<AuthContext.Provider value={[auth, setAuth]}>
        {children.children}
    </AuthContext.Provider >);
};
