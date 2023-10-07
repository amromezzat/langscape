import { ReactNode } from 'react';
import { Store, StoreContext } from './store';

interface Props {
    children: ReactNode,
    store: Store
}

export const StoreProvider = ({ children, store }: Props) => {

    return (
      <StoreContext.Provider value={store}>
        {children}
      </StoreContext.Provider>
    );
};