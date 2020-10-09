import React, { Dispatch, SetStateAction, useState } from 'react'

export interface State {
    email: string,
    isAuthenticated: boolean
}

const defaultValue: State = {
    email: '',
    isAuthenticated: false
};

export const AuthContext = React.createContext<[State, Dispatch<SetStateAction<State>>]>([undefined!, undefined!]);

export const AuthContextProvider = (children: any) => {
    const [auth, setAuth] = useState<State>(defaultValue);

    return (<AuthContext.Provider value={[auth, setAuth]}>
        {children.children}
    </AuthContext.Provider>);
};
