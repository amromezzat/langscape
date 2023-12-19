import { createContext } from "react";
import { useContext } from "react";
import FlashCardStore from "../features/flashCardStore";
import ModalStore from "../common/modalStore";
import AccountStore from "../features/accountStore";
import PromptStore from "../common/promptStore";

export interface Store {
    modalStore: ModalStore;
    promptStore: PromptStore;
    accountStore: AccountStore;
    flashCardStore: FlashCardStore;
}

export const StoreContext = createContext<Store | undefined>(undefined)

export const useStore = () => {
  const store = useContext(StoreContext);
  if (store === undefined) {
    throw new Error('store must be used within a StoreProvider');
  }
  
  return store;
}