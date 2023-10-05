import React, { ReactNode, useContext } from 'react';
import FlashCardStore from './flashCardStore';


export interface Store {
    flashCardStore: FlashCardStore;
}

const storeContext = React.createContext<Store | undefined>(undefined)

interface Props {
    children: ReactNode,
    store: Store
}

export const StoreProvider = ({ children, store }: Props) => {

    return (
      <storeContext.Provider value={store}>
        {children}
      </storeContext.Provider>
    );
};

export const useStore = () => {
  const store = useContext(storeContext);
  if (store === undefined) {
    throw new Error('store must be used within a StoreProvider');
  }
  
  return store;
}